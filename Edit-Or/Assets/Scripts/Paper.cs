using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paper : MonoBehaviour
{
    public GameObject paperText;
    public GameObject paperPanel;
    public AudioSource paperSound;

    public bool isInRange;
    private bool isInEdit;
    public bool panelIsOpen;
    public bool isBeingLookedAt;
    private void Start()
    {
        paperText.SetActive(false);
        isInEdit = false;
        panelIsOpen = false;
    }

    private void OnMouseOver()
    {
        if (isInRange) 
        {
            isBeingLookedAt = true;
            if(PauseMenu.instance.isPaused == false)
            {
                paperText.SetActive(true); //Use animator here.
            }
        }
    }

    private void OnMouseExit()
    {
        if (isInRange)
        {
            isBeingLookedAt = false;
            if (PauseMenu.instance.isPaused == false)
            {
                paperText.SetActive(false); //Use animator here.
            }
        }
    }

    private void Update()
    {

        if (!isInEdit)
        {
            if (isBeingLookedAt)
            {
                if (!panelIsOpen)
                {
                    if (Input.GetKeyDown(KeyCode.E) && PauseMenu.instance.isPaused == false)
                    {
                        PauseMenu.instance.canOpen = false;
                        paperSound.PlayOneShot(paperSound.clip);
                        paperPanel.SetActive(true);
                        panelIsOpen = true;
                        CameraManager.instance.DisableComponents();
                    }
                }
                else
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        panelIsOpen = false;
                        paperSound.PlayOneShot(paperSound.clip);
                        paperPanel.SetActive(false);
                        CameraManager.instance.ReEnableComponents();
                        PauseMenu.instance.canOpen = true;
                    }
                }
                
            }

        }

    }
}
