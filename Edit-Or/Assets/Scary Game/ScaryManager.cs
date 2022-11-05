using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaryManager : MonoBehaviour
{
    #region Singleton
    public static ScaryManager instance;
    private void Awake()
    {
        instance = this;

    }
    #endregion

    [SerializeField]
    private Elevator elevatorScript;

    public bool beenToElevator;
    public bool beenBackToStart;
    public bool doorsHaveOpened;
    public bool hasPlayedSound;

    [SerializeField]
    private AudioSource scarySound;
    [SerializeField]
    private AudioSource jumpScareSound;
    [SerializeField]
    private AudioSource doorSound;

    [SerializeField]
    private GameObject jumpScareObject;
    [SerializeField]
    private GameObject jumpScareObject2;
    [SerializeField]
    private GameObject light1;
    [SerializeField]
    private GameObject light2;
    [SerializeField]
    private GameObject light3;
    private void Update()
    {
        if (beenToElevator && beenBackToStart && !doorsHaveOpened)
        {
            elevatorScript.OpenDoors();
            doorsHaveOpened = true;
        }
    }

    public void PlaySound()
    {
        scarySound.PlayOneShot(scarySound.clip);
        StartCoroutine(LightingFlash());
    }

    public void JumpScare()
    {
        jumpScareObject2.SetActive(true);
        jumpScareSound.PlayOneShot(jumpScareSound.clip);
        StartCoroutine(QuitFunc());
    }

    public void PlayDoorSound()
    {
        doorSound.PlayOneShot(doorSound.clip);
    }

    IEnumerator LightingFlash()
    {
        light1.SetActive(false);
        light2.SetActive(false);
        light3.SetActive(false);
        jumpScareObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        light1.SetActive(true);
        light2.SetActive(true);
        light3.SetActive(true);
        jumpScareObject.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        light1.SetActive(false);
        light2.SetActive(false);
        light3.SetActive(false);
        jumpScareObject.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        light1.SetActive(true);
        light2.SetActive(true);
        light3.SetActive(true);
        jumpScareObject.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        light1.SetActive(false);
        light2.SetActive(false);
        light3.SetActive(false);
        jumpScareObject.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        light1.SetActive(true);
        light2.SetActive(true);
        light3.SetActive(true);
        jumpScareObject.SetActive(false);
    }

    IEnumerator QuitFunc()
    {
        yield return new WaitForSeconds(2f);
        Application.Quit();
    }
}
