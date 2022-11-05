using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    private bool doorsAreOpen;
    private bool isClosingGame;
    private bool isMoving;

    [SerializeField]
    private Animation doorAnimation;
    [SerializeField]
    private Animator fadeCanvas;

    [Header("Floors")]
    [SerializeField]
    private Text floorText;
    [SerializeField]
    private GameObject playFloor;
    [SerializeField]
    private GameObject settingsFloor;

    [Header("Audio")]
    [SerializeField]
    private AudioSource closeDoorSound;
    [SerializeField]
    private AudioSource openDoorSound;
    [SerializeField]
    private AudioSource rideSound;
    

    private void Start()
    {
        doorsAreOpen = true;
        isClosingGame = false;
        isMoving = false;
    }
    #region Settings

    [SerializeField]
    private WindowScript windowScript;

    public void SetFullscreen(bool isFullscreen)
    {
        if (isFullscreen)
        {
            windowScript.OnNoBorderBtnClick();
            windowScript.OnMaximizeBtnClick();
        }
        else
        {
            windowScript.OnBorderBtnClick();
        }
    }

    #endregion

    #region buttonMethods
    public void PlayButtonPressed()
    {
        if (!isMoving)
        {
            doorAnimation.Play("CloseDoors");
            doorsAreOpen = false;
            
            StartCoroutine(MoveFloor(true));
            //If doors are closed, just move to the next floor, and then open doors.
        }

    }

    public void SettingsButtonPressed()
    {
        if (!isMoving)
        {
            doorAnimation.Play("CloseDoors");
            doorsAreOpen = false;

            StartCoroutine(MoveFloor(false));
            //If doors are closed, just move to the next floor, and then open doors.
        }
    }

    public void QuitButtonPressed()
    {
        if (!isClosingGame)
        {
            doorAnimation.Play("CloseDoors");
            doorsAreOpen = false;
            isClosingGame = true;
            fadeCanvas.SetTrigger("QuitFadeOut");
            StartCoroutine(QuitGameTimer());
        }
        if (!isClosingGame)
        {
            isClosingGame = true;
            fadeCanvas.SetTrigger("QuitFadeOut");
            StartCoroutine(QuitGameTimer());
        }


    }

    #region Coroutines
    IEnumerator MoveFloor(bool isPlayFloor)
    {
        isMoving = true;
        closeDoorSound.PlayOneShot(closeDoorSound.clip);
        yield return new WaitForSecondsRealtime(3.25f);
        //Play elevator moving sound
        if (isPlayFloor) //If play button was pressed
        {
            settingsFloor.SetActive(false);
            playFloor.SetActive(true);
        }
        else //Settings button was pressed
        {
            settingsFloor.SetActive(true);
            playFloor.SetActive(false);
        }
        rideSound.PlayOneShot(rideSound.clip);
        yield return new WaitForSecondsRealtime(2f);
        if (isPlayFloor)
        {
            floorText.text = "2";
        }
        else
        {
            floorText.text = "3";
        }
        doorAnimation.Play("OpenDoors");
        openDoorSound.PlayOneShot(openDoorSound.clip);
        yield return new WaitForSecondsRealtime(3.25f);
        isMoving = false;
    }
    IEnumerator QuitGameTimer()
    {
        yield return new WaitForSecondsRealtime(3.25f);
        Application.Quit();
    }

    #endregion

    #endregion
}
