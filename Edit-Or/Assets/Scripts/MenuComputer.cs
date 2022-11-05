using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MenuComputer : MonoBehaviour
{
    [SerializeField]
    private Animator mainCamera;
    [SerializeField]
    private GameObject Text;
    [SerializeField]
    private AudioSource computerSound;

    [SerializeField]
    private GameObject canvasBlocker;

    private bool isMoving;
    private bool isBeingLookedAt;

    private void Start()
    {
        isMoving = false;
        isBeingLookedAt = false;
        Text.gameObject.SetActive(false);
        canvasBlocker.SetActive(true);
 
    }
    private void OnMouseEnter()
    {
        isBeingLookedAt = true;
        Text.gameObject.SetActive(true);

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
            mainCamera.SetTrigger("None-Play");
            canvasBlocker.SetActive(false);
            computerSound.PlayOneShot(computerSound.clip);
        }
    }

    public void Back()
    {
        isMoving = false;
        mainCamera.SetTrigger("Play-None");
        canvasBlocker.SetActive(true);
        computerSound.PlayOneShot(computerSound.clip);
    }
}
