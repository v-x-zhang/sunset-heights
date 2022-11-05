using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField]
    private AudioSource openDoorSound;
    [SerializeField]
    private AudioSource closeDoorSound;
    [SerializeField]
    private AudioSource callSound;
    [SerializeField]
    private float timeToWait;

    [Header("Animators")]
    [SerializeField]
    private Animation elevatorAnimation;



    public GameObject elevatorText;
    

    public bool isInRange;
    private bool isInEdit;
    public bool doorsAreOpen;
    public bool isBeingLookedAt;
    [SerializeField]
    private bool isWorkingButton = true;
    
    private void Start()
    {
        elevatorText.SetActive(false);
        doorsAreOpen = false;
        isInEdit = false;
    }

    private void OnMouseOver()
    {
        if (isInRange)
        {
            isBeingLookedAt = true;
            elevatorText.SetActive(true); //Use animator here.
        }
    }

    private void OnMouseExit()
    {
        if (isInRange)
        {
            isBeingLookedAt = false;
            elevatorText.SetActive(false); //Use animator here.
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
                    if (!doorsAreOpen && isWorkingButton)
                    {
                        callSound.PlayOneShot(callSound.clip);
                        OpenDoors();
                        StartCoroutine(CloseDoorsTimer());
                    }
                    else
                    {
                        callSound.PlayOneShot(callSound.clip);
                    }
                    
                }
            }

        }

    }

    public void OpenDoors()
    {
        LevelManager.instance.doorsHaveOpened = true;
        openDoorSound.PlayOneShot(openDoorSound.clip);
        elevatorAnimation.Play("OpenDoors");
        doorsAreOpen = true;
        if (isWorkingButton)
        {
            LevelManager.instance.doorsAreClosed = false;
        }
    }

    public void CloseDoors()
    {
        closeDoorSound.PlayOneShot(closeDoorSound.clip);
        elevatorAnimation.Play("CloseDoors");
    }

    

    IEnumerator CloseDoorsTimer()
    {
        yield return new WaitForSecondsRealtime(timeToWait);
        CloseDoors();
        yield return new WaitForSecondsRealtime(3.25f);
        doorsAreOpen = false;
        LevelManager.instance.doorsAreClosed = true;

    }
}
