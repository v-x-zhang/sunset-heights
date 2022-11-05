using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApartmentDoor : MonoBehaviour
{
    public GameObject apartmentText;
    [SerializeField]
    private AudioSource lockedSound;

    public bool isBeingLookedAt;

    public bool isInRange;
    private bool isInEdit;

    [Header("Animation")]
    public bool canOpen;
    public Animation animator;

    private bool isOpen;
    private void Start()
    {
        apartmentText.SetActive(false);
        isInEdit = false;
    }

    private void OnMouseOver()
    {
        if (isInRange)
        {
            isBeingLookedAt = true;
            apartmentText.SetActive(true); //Use animator here.
        }
    }

    private void OnMouseExit()
    {
        if (isInRange)
        {
            isBeingLookedAt = false;
            apartmentText.SetActive(false); //Use animator here.
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
                    if (canOpen)
                    {
                        if (!isOpen)
                        {
                            animator.Play("Door_Open");
                            lockedSound.PlayOneShot(lockedSound.clip);
                            isOpen = true;
                        }
                        else
                        {
                            animator.Play("Door_Close");
                            lockedSound.PlayOneShot(lockedSound.clip);
                            isOpen = false;
                        }
                    }
                    else
                    {
                        lockedSound.PlayOneShot(lockedSound.clip);
                    }
                }

            }

        }

    }
}
