using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    #region Singleton

    public static CameraManager instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    [Header("Object Exceptions")]
    [SerializeField]
    private Canvas editCanvas;
    [SerializeField]
    private PlayerMovement playerMovement;
    [SerializeField]
    private AudioSource keyboardClick; //Remove, since we will migrate to camera animations.

    [Header("Cameras")]
    public Camera playerCam;
    public Camera editCamera;

    [Header("ObjectsToDisable/Enable")]
    [SerializeField]
    private Behaviour[] componentsToDisable;
    [SerializeField]
    private GameObject[] gameObjectsToDisable;
    [SerializeField]
    private Behaviour[] componentsToEnable;
    [SerializeField]
    private Outline[] outlinesToEnable;
    [SerializeField]
    private Outline[] outlinesToDisable;

    public bool isEditing;

    
    public void SwitchCameras()
    {
        editCamera.enabled = !editCamera.enabled;
        playerCam.enabled = !playerCam.enabled;
        isEditing = !isEditing;
        editCanvas.gameObject.SetActive(isEditing);
        playerMovement.isInEditor = isEditing;

        //Disable and Enable Components
        foreach (Behaviour component in componentsToDisable)
        {
            component.enabled = !isEditing;
        }
        foreach (GameObject gameObject in gameObjectsToDisable)
        {
            gameObject.SetActive(!isEditing);
        }
        foreach(Behaviour component in componentsToEnable)
        {
            component.enabled = isEditing;
        }
        foreach(Outline outline in outlinesToEnable)
        {
            outline.enabled = isEditing;
        }
        foreach(Outline outline in outlinesToDisable)
        {
            outline.enabled = !isEditing;
        }

        //Switch Cursor Mode
        Cursor.visible = !Cursor.visible;
        if (isEditing)
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        EditManager.instance.DeselectAllWalls();
        keyboardClick.PlayOneShot(keyboardClick.clip);
    }

    public void SetDefaults()
    {
        playerCam.enabled = true;
        if(editCamera != null)
        {
            editCamera.enabled = false;
        }
        isEditing = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //Disable and Enable Components
        foreach (Behaviour component in componentsToDisable)
        {
            component.enabled = !isEditing;
        }
        foreach (GameObject gameObject in gameObjectsToDisable)
        {
            gameObject.SetActive(!isEditing);
        }
        foreach (Behaviour component in componentsToEnable)
        {
            component.enabled = isEditing;
        }
        foreach (Outline outline in outlinesToEnable)
        {
            outline.enabled = isEditing;
        }
        foreach (Outline outline in outlinesToDisable)
        {
            outline.enabled = !isEditing;
        }

    }

    public void DisableComponents()
    {
        playerMovement.isInEditor = true;
        foreach(Behaviour component in componentsToDisable)
        {
            component.enabled = false;
        }
    }

    public void ReEnableComponents()
    {
        playerMovement.isInEditor = false;
        foreach (Behaviour component in componentsToDisable)
        {
            component.enabled = true;
        }
    }

    private void Start()
    {
        SetDefaults();
    }
}
