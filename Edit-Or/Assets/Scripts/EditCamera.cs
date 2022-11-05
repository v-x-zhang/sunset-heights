using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditCamera : MonoBehaviour
{
    [SerializeField]
    private Transform roomCenter;
    [SerializeField]
    private float rotateSpeed;
    [SerializeField]
    private Transform editCam;
    [SerializeField]
    private Transform projectContainer;
    [SerializeField]
    private float maxZoomIn;
    [SerializeField]
    private float maxZoomOut;
    [SerializeField]
    private float zoomSpeed;

    // Update is called once per frame
    void Update()
    {
        float _xMov = Input.GetAxis("Horizontal");
        float ScrollWheelChange = Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;

        transform.RotateAround(roomCenter.position, transform.up, -rotateSpeed * _xMov * Time.fixedDeltaTime);

        if (ScrollWheelChange != 0)
        {
            Vector3 amountToChange = editCam.forward * ScrollWheelChange;
            
            Vector3 NewPos = editCam.position + amountToChange;

            if(Vector3.Distance(NewPos, roomCenter.position) > maxZoomIn && Vector3.Distance(NewPos, roomCenter.position) < maxZoomOut)
            {
                editCam.position = NewPos;
                projectContainer.transform.position = NewPos;
            }
        }
    }
}
