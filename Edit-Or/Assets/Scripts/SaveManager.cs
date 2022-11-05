using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{

    #region Singleton

    public static SaveManager instance;

    private void Awake()
    {
        instance = this;
        ES3AutoSaveMgr.Current.Load();
        ES3.Save("levelReached", currentLevel);
    }

    #endregion

    [SerializeField]
    private string currentLevel;
    [Header("If Player Saved In Elevator")]
    public PlayerMovement playerMovement;
    public Transform cameraContainer;
    public Transform playerCameraSpot;

    public void SavePlayer()
    {
        ES3AutoSaveMgr.Current.Save();
        ES3.Save("levelReached", currentLevel);
    }

    private void OnApplicationQuit()
    {
        ES3AutoSaveMgr.Current.Save();
        ES3.Save("levelReached", currentLevel);
    }
}
