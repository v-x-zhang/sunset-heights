using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : MonoBehaviour
{
    public GameObject computerText;


    public bool isInRange;
    private bool isInEdit;

    public bool isBeingLookedAt;
    private void Start()
    {
        computerText.SetActive(false);
        isInRange = false;
        isInEdit = false;
    }

    private void OnMouseOver()
    {
        if (isInRange)
        {
            isBeingLookedAt = true;
            computerText.SetActive(true); //Use animator here.
        }
    }

    private void OnMouseExit()
    {
        if (isInRange)
        {
            isBeingLookedAt = false;
            computerText.SetActive(false); //Use animator here.
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            isInRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            isInRange = false;
            isBeingLookedAt = false;
            computerText.SetActive(false); //Use animator here, which would check if it's being looked at to turn it off.
            
        }
    }
    private void Update()
    {

        if (!isInEdit)
        {
            if (isBeingLookedAt)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    CameraManager.instance.SwitchCameras();
                    isInEdit = true;
                }
            }

        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                CameraManager.instance.SwitchCameras();
                isInEdit = false;
            }
        }
        
    }

}

