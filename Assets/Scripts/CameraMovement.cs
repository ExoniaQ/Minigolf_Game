
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Camera cam;
    public Transform ballTransform;
    public float camSpeed = 1.0f;
    public float scrollSpeed = 1.0f;

    private Vector3 mousePos;
   
    public void Update()
    {
        float distance = Vector3.Distance(cam.transform.position, ballTransform.position);
        Vector3 camDirection = cam.transform.position - ballTransform.position;

        float scrollAmount = Input.mouseScrollDelta.y;

        if (scrollAmount != 0)
        {
            distance = distance - scrollAmount * scrollSpeed;

            

            camDirection.Normalize();
            camDirection *= distance;

            cam.transform.position = ballTransform.position + camDirection;
        }


        if (Input.GetMouseButtonDown(1))
        {
            mousePos = cam.ScreenToViewportPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(1))
        {
            Vector3 rotateDirection = mousePos - cam.ScreenToViewportPoint(Input.mousePosition);

            cam.transform.position = ballTransform.position;


            cam.transform.Rotate(new Vector3(camSpeed, 0, 0), rotateDirection.y * 180);
            cam.transform.Rotate(new Vector3(0, camSpeed, 0), -rotateDirection.x * 180, Space.World);
            cam.transform.Translate(new Vector3(0, 0, -distance));

            mousePos = cam.ScreenToViewportPoint(Input.mousePosition);
        }

        cam.transform.position = ballTransform.position;
        cam.transform.Translate(new Vector3(0, 0, -distance));

    }
}

