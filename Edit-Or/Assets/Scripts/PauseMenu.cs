using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    #region Singleton

    public static PauseMenu instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    [SerializeField]
    private PlayerMovement playerMovement;
    [SerializeField]
    private PlayerCamera playerCamera;

    [SerializeField]
    private GameObject pausePanel;

    public bool isPaused;
    public bool canOpen;

    void Start()
    {
        canOpen = true;
        isPaused = false;
        pausePanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && playerMovement.isInEditor == false && LevelManager.instance.isInElevator == false && canOpen)
        {
            isPaused = !isPaused;
            pausePanel.SetActive(isPaused);
            playerMovement.isInEditor = isPaused;
            playerCamera.enabled = !isPaused;
            Cursor.visible = isPaused;

            if (isPaused)
            {
                Cursor.lockState = CursorLockMode.Confined;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    public void Resume()
    {
        isPaused = false;
        pausePanel.SetActive(false);
        playerMovement.isInEditor = false;
        playerCamera.enabled = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ReturnToMenu() 
    {
        SaveManager.instance.SavePlayer();
        LevelManager.instance.LoadScene("MenuScene");
    }

}
