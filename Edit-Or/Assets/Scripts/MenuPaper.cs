using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPaper : MonoBehaviour
{
    [SerializeField]
    private GameObject Text;
    [SerializeField]
    private Animator mainCamera;
    [SerializeField]
    private AudioSource paperSound;


    [SerializeField]
    private GameObject canvasBlocker;
    private bool isBeingLookedAt;
    private bool isInSettings;
    private bool isMoving;
    private void Start()
    {
        isMoving = false;
        isBeingLookedAt = false;
        Text.gameObject.SetActive(false);
        canvasBlocker.SetActive(true);
        isInSettings = false;
    }
    private void OnMouseEnter()
    {
        if (!isInSettings)
        {
            isBeingLookedAt = true;
            Text.gameObject.SetActive(true);
        }
       
    }

    private void OnMouseExit()
    {
        isBeingLookedAt = false;
        Text.gameObject.SetActive(false);

    }

    private void OnMouseDown()
    {
        if (isBeingLookedAt && !isMoving)
        {
            isMoving = true;
            mainCamera.SetTrigger("None-Options");
            canvasBlocker.SetActive(false);
            Text.gameObject.SetActive(false);
            isInSettings = true;
            paperSound.PlayOneShot(paperSound.clip);

        }
    }

    public void Back()
    {
        isMoving = false;
        mainCamera.SetTrigger("Options-None");
        canvasBlocker.SetActive(true);
        isInSettings = false;
        paperSound.PlayOneShot(paperSound.clip);

    }

}
