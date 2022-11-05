using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuDragItem : MonoBehaviour
{

    public float dragSpeed = 1;


    [SerializeField]
    private Camera camera;


    //Physics private variables
    private Vector3 mOffset;
    private float mZcoord;
    private Vector3 direction;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    private void OnMouseDown()
    {
        mZcoord = camera.WorldToScreenPoint(transform.position).z;
        mOffset = transform.position - GetMouseWorldPos();
    }

    private void OnMouseUp()
    {
        direction = Vector3.zero;
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;

        mousePoint.z = mZcoord;

        return camera.ScreenToWorldPoint(mousePoint);
    }
    private void OnMouseDrag()
    {

         direction = GetMouseWorldPos() - transform.position + mOffset * dragSpeed;
         rb.velocity = direction;

    }


}
