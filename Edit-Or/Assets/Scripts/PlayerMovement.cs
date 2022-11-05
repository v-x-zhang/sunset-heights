using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private AudioSource footStepsSound;

    public bool isInEditor;

    private Rigidbody rb;
    private Vector3 direction;
    private bool isWalking;
    private Coroutine lastCoroutine;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        isWalking = false;
        isInEditor = false;
        
    }

    private void Update()
    {
        if (!isInEditor)
        {
            float _xMov = Input.GetAxis("Horizontal");
            float _yMov = Input.GetAxis("Vertical");

            direction = _xMov * transform.right + _yMov * transform.forward;

            if (lastCoroutine == null)
            {
                if (direction != Vector3.zero)
                {
                    isWalking = true;
                    lastCoroutine = StartCoroutine(WalkSound());

                }
            }
            else
            {
                if (direction != Vector3.zero)
                {
                    isWalking = true;
                }
                else
                {
                    isWalking = false;
                }
            }
        }
        else
        {
            direction = Vector3.zero;
            if (lastCoroutine == null)
            {
                if (direction != Vector3.zero)
                {
                    isWalking = true;
                    lastCoroutine = StartCoroutine(WalkSound());

                }
            }
            else
            {
                if (direction != Vector3.zero)
                {
                    isWalking = true;
                }
                else
                {
                    isWalking = false;
                }
            }
        }
        
    }

    private void FixedUpdate()
    {
        Vector3 yVelFix = new Vector3(0, rb.velocity.y, 0);

        rb.velocity = direction * movementSpeed * Time.deltaTime;

        rb.velocity += yVelFix;
    }


    IEnumerator WalkSound()
    {
        while (isWalking)
        {
            if (!footStepsSound.isPlaying)
            {
                footStepsSound.Play();
            }
            yield return null;
        }
        footStepsSound.Stop();
        lastCoroutine = null;
    }

    
}
