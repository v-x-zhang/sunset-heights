using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuGlobe : MonoBehaviour
{
    [SerializeField]
    private GameObject Text;

    private bool isBeingLookedAt;

    private void Start()
    {
        isBeingLookedAt = false;
        Text.gameObject.SetActive(false);
    }
    private void OnMouseEnter()
    {
        isBeingLookedAt = true;
        Text.gameObject.SetActive(true);

    }

    private void OnMouseExit()
    {
        isBeingLookedAt = false;
        Text.gameObject.SetActive(false);

    }

    private void OnMouseDown()
    {
        Application.Quit();
    }


}
