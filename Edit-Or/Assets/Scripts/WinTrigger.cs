using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinTrigger : MonoBehaviour
{

    [SerializeField]
    private string nextSceneName;

    [SerializeField]
    private Elevator elevatorScript;

    private bool isInElevator;

    private void Start()
    {
        isInElevator = false;
    }
    private void Update()
    {
        if (isInElevator)
        {
            if (!elevatorScript.doorsAreOpen)
            {
                StartLoading();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        isInElevator = true;
    }

    private void OnTriggerExit(Collider other)
    {
        isInElevator = false;
    }

    void StartLoading()
    {
        //Fade to loading screen, start loading async when screen is completely black.
    }
}
