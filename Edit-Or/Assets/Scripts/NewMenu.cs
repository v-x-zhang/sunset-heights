using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class NewMenu : MonoBehaviour
{

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

    #region Saving & Loading
    [Header("Saving & Loading")]
    [SerializeField]
    private string firstLevelName;
    [SerializeField]
    private Button continueButton;
    [SerializeField]
    private GameObject newGameNoPopButton;
    [SerializeField]
    private GameObject newGamePopButton;
    [SerializeField]
    private GameObject confirmPopUp;

    private bool isLoadingLevel;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        
        isLoadingLevel = false;
        confirmPopUp.SetActive(false);
        string path = Application.persistentDataPath + "/SaveFile.es3";
        if (File.Exists(path))
        {
            continueButton.interactable = true;
            newGameNoPopButton.SetActive(false);
            newGamePopButton.SetActive(true);
        }
        else
        {
            continueButton.interactable = false;
            newGameNoPopButton.SetActive(true);
            newGamePopButton.SetActive(false);
        }
    }

    public void ContinueButtonPressed()
    {
        if (!isLoadingLevel)
        {
            isLoadingLevel = true;
            StartCoroutine(FadeTo(ES3.Load("levelReached") as string));
        }
        
    }

    public void NewGameButtonPressed()
    {
        ES3.DeleteFile(Application.persistentDataPath + "/SaveFile.es3");
        if (!isLoadingLevel)
        {
            isLoadingLevel = true;
            StartCoroutine(FadeTo(firstLevelName));
        }
    }
    #endregion

    #region Scene Fading

    [SerializeField]
    private Animator fadeCanvas;
    [SerializeField]
    private Animator loadingInfo;

    IEnumerator FadeTo(string sceneName)
    {
        fadeCanvas.SetTrigger("FadeOut");
        yield return new WaitForSecondsRealtime(2f);
        AsyncOperation loadAsync = SceneManager.LoadSceneAsync(sceneName);
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
    }
    #endregion
}
