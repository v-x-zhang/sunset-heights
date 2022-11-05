using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditableObject : MonoBehaviour
{
    private Color startcolor;

    [SerializeField]
    private Outline outline;

    [SerializeField]
    private Color hoverColor;
    [SerializeField]
    private Color selectedColor;

    public bool isDeselectingAll;
    public bool isSelected;
    private bool isWallToSelect;

    [SerializeField]
    private GameObject[] objectButtons;

    public AudioSource pushingSound;

    public float currentSpeed;

    Vector3 oldPos;
    private void Start()
    {
        oldPos = transform.position;
        startcolor = outline.OutlineColor;
        isSelected = false;
        isDeselectingAll = true;
        foreach (GameObject button in objectButtons)
        {
            button.SetActive(false);
        }
    }
    void OnMouseEnter()
    {
        if (CameraManager.instance.isEditing)
        {
            outline.OutlineColor = hoverColor;
        }
        
    }
    void OnMouseExit()
    {
        if (CameraManager.instance.isEditing)
        {
            if (isSelected)
            {
                outline.OutlineColor = selectedColor;
            }
            else
            {
                outline.OutlineColor = startcolor;
            }
        }
    }

    private void OnMouseDown()
    {
        if (CameraManager.instance.isEditing)
        {
            isWallToSelect = true;
            EditManager.instance.SelectNewWall();
            isWallToSelect = false;
            isSelected = !isSelected;
            foreach (GameObject button in objectButtons)
            {
                button.SetActive(isSelected);
            }
            if (isSelected)
            {
                outline.OutlineColor = selectedColor;
            }
            else
            {
                outline.OutlineColor = startcolor;
            }
        }
        
    }

    public void Deselect()
    {
        pushingSound.Stop();
        GetComponent<Rigidbody>().isKinematic = true;
        foreach (GameObject button in objectButtons)
        {
            button.SetActive(false);
            button.GetComponent<DragButton>().lastCoroutine = null;
        }
        if (isWallToSelect == false)
        {
            isSelected = false;
            outline.OutlineColor = startcolor;
        }
        if(isDeselectingAll == true)
        {
            isSelected = false;
            outline.OutlineColor = startcolor;
            isDeselectingAll = false;
        }
    }

    private void FixedUpdate()
    {
        currentSpeed = Vector3.Distance(oldPos, transform.position);
        oldPos = transform.position;
    }
}
