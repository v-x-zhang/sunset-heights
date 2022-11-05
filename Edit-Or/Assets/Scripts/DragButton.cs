using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragButton : MonoBehaviour
{
    public enum DirectionAxis { xAxis = 0, yAxis = 1, zAxis = 2 };
    public DirectionAxis axis = DirectionAxis.xAxis;
    public float dragSpeed = 1;
    [SerializeField]
    private GameObject transformObject;
    public Coroutine lastCoroutine;


    [Header("Visuals")]
    [SerializeField]
    private Renderer renderer;
    [SerializeField]
    private Color hoverColor;
    [SerializeField]
    private Color selectedColor;
    [SerializeField]
    private Color defaultColor;

    //Physics private variables
    private Vector3 mOffset;
    private float mZcoord;
    private Vector3 direction;
    private Rigidbody rb;

    //Audio private variables
    private AudioSource dragSound;
    private bool isDragging;
    private void Start()
    {
        renderer.material.color = defaultColor;
        rb = transformObject.GetComponent<Rigidbody>();
        dragSound = transformObject.GetComponent<EditableObject>().pushingSound;
    }
    private void OnMouseEnter()
    {
        renderer.material.color = hoverColor;
    }

    private void OnMouseExit()
    {
        renderer.material.color = defaultColor;

    }

    private void OnMouseDown()
    {
        renderer.material.color = selectedColor;
        mZcoord = CameraManager.instance.editCamera.WorldToScreenPoint(transformObject.transform.position).z;
        mOffset = transformObject.transform.position - GetMouseWorldPos();
    }

    private void OnMouseUp()
    {
        rb.isKinematic = true;
        renderer.material.color = hoverColor;
        direction = Vector3.zero;
        isDragging = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isDragging = false;
            renderer.material.color = hoverColor;
            direction = Vector3.zero;
            rb.isKinematic = false;
        }
    }
    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;

        mousePoint.z = mZcoord;

        return CameraManager.instance.editCamera.ScreenToWorldPoint(mousePoint);
    }
    private void OnMouseDrag()
    {
        rb.isKinematic = false;
        if (axis == DirectionAxis.xAxis)
        {
            direction = new Vector3(GetMouseWorldPos().x - transformObject.transform.position.x + mOffset.x, 0, 0) * dragSpeed;
            rb.velocity = direction;


            //Audio
            if (lastCoroutine == null)
            {
                lastCoroutine = StartCoroutine(DragSound());
            }
        }
        else if (axis == DirectionAxis.yAxis)
        {
            direction = new Vector3(0, GetMouseWorldPos().y - transformObject.transform.position.y + mOffset.y, 0) * dragSpeed;
            rb.velocity = direction;

            //Audio
            if (lastCoroutine == null)
            {
                lastCoroutine = StartCoroutine(DragSound());
            }

        }
        else
        {
            direction = new Vector3(0, 0, GetMouseWorldPos().z - transformObject.transform.position.z + mOffset.z) * dragSpeed;
            rb.velocity = direction;
            

            //Audio
            if(lastCoroutine == null)
            {
                lastCoroutine = StartCoroutine(DragSound());
            }
        }
    }



    IEnumerator DragSound()
    {
        for(; ; )
        {
            if (!dragSound.isPlaying)
            {
                dragSound.Play();

            }
            float velocity = transformObject.GetComponent<EditableObject>().currentSpeed;
            dragSound.volume = Mathf.Lerp(dragSound.volume, Mathf.Clamp01(velocity / 5), .1f);
            yield return null;
        }
        
    }
}
