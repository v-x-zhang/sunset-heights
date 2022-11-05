using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instructions : MonoBehaviour
{

    [SerializeField]
    private Animator animator;
    [SerializeField]
    private AudioSource sound;

    private bool isOpen;

    private void Start()
    {
        isOpen = false;
    }
    public void PaperClick()
    {
        if (!isOpen)
        {
            Open();
        }
        else
        {
            Close();
        }
    }
    public void Open()
    {
        isOpen = true;
        animator.SetTrigger("Open");
        sound.PlayOneShot(sound.clip);
    }

    public void Close()
    {
        isOpen = false;
        animator.SetTrigger("Close");
        sound.PlayOneShot(sound.clip);
    }
}
