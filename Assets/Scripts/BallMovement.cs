using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    Quaternion initialRotation;
    // Start is called before the first frame update
    void Start()
    {
        initialRotation = ballTransform.rotation;
    }

    public Transform ballTransform;
    public float shootForce = 10f;
    public LineRenderer lineRenderer;

    private bool isDragging = false;
    private float dragDistance = 0f;

    private RaycastHit CastRay()
    {
        Vector3 screenMousePosFar = new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            Camera.main.farClipPlane);
        Vector3 screenMousePosNear = new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            Camera.main.nearClipPlane);
        Vector3 worldMousePosFar = Camera.main.ScreenToWorldPoint(screenMousePosFar);
        Vector3 worldMousePosNear = Camera.main.ScreenToWorldPoint(screenMousePosNear);
        RaycastHit hit;
        Physics.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear, out hit);

        return hit;
    }
    // Update is called once per frame
    void Update()
    {
        RaycastHit hit = CastRay();

        if (hit.collider != null && hit.collider.gameObject == ballTransform.gameObject)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isDragging = true;
                dragDistance = Vector3.Distance(ballTransform.position, hit.point);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;

            Vector3 shootDirection = (ballTransform.position - hit.point).normalized;
            float shootMagnitude = dragDistance * shootForce;
            ballTransform.rotation = initialRotation;


            Rigidbody rb = ballTransform.GetComponent<Rigidbody>();
            rb.AddForce(shootDirection * shootMagnitude, ForceMode.Impulse);
        }
    }


}
