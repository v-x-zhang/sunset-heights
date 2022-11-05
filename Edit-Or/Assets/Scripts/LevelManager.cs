using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    #region Singleton and DontDestroyOnLoad

    public static LevelManager instance;
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);
    }

    #endregion

    [SerializeField]
    private string nextLevelScene;

    public bool doorsAreClosed;
    public bool doorsHaveOpened;
    public bool isInElevator;
    private bool isLoadingLevel;

    [SerializeField]
    private Animator loadingCanvas;
    [SerializeField]
    private Animator loadingInfo;

    public Transform outsideElevator;

    void Start()
    {
        doorsHaveOpened = false;
        doorsAreClosed = false;
        isInElevator = false;
        isLoadingLevel = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isInElevator = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            isInElevator = false;
        }
    }
    void Update()
    {
        if (doorsAreClosed)
        {
            if (isInElevator)
            {
                if (!isLoadingLevel)
                {
                    //Fade to loading screen
                    isLoadingLevel = true;
                    loadingCanvas.SetTrigger("FadeOut");
                    StartCoroutine(LoadLevel(nextLevelScene));
                }
                
            }
        }
        if(isInElevator && !doorsHaveOpened) // If the player saved in the elevator.
        {
            Vector3 pos = new Vector3(outsideElevator.position.x, transform.position.y, LevelManager.instance.outsideElevator.position.z);
            SaveManager.instance.playerMovement.transform.position = instance.outsideElevator.position;
            SaveManager.instance.cameraContainer.transform.position = SaveManager.instance.playerCameraSpot.position;
        }
    }
    
    public void LoadScene(string sceneToLoad)
    {
        if (!isLoadingLevel) 
        {
            
            StartCoroutine(LoadLevel(sceneToLoad));
            isLoadingLevel = true;
            loadingCanvas.SetTrigger("FadeOut");
        }
        else
        {
            Debug.Log("Is already Loading level!");
            //Show this in game to avoid confusion, or disable the pause menu all together when the elevator doors are closing.
        }
        
    }
    IEnumerator LoadLevel(string sceneToLoad)
    {
        yield return new WaitForSecondsRealtime(2f);
        CameraManager.instance.DisableComponents();
        AsyncOperation loadAsync = SceneManager.LoadSceneAsync(sceneToLoad);
        loadAsync.allowSceneActivation = false;
        loadingInfo.SetTrigger("EnableInfo");
        while (loadAsync.progress < .9f)
        {
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        loadingInfo.SetTrigger("DisableInfo");
        yield return new WaitForSeconds(1f);
        loadAsync.allowSceneActivation = true;
        yield return new WaitForSecondsRealtime(.25f);
        Destroy(gameObject);
    }
}
