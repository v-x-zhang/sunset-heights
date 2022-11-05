using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{

    [SerializeField]
    private Transform objectToFollow;
    
    [Header("Linear Interpolation")]
    [SerializeField]
    private bool useLinearInterpolation = false;
    [SerializeField]
    private float lerpTime = 0f;

    void Update()
    {
        if (useLinearInterpolation)
        {
            transform.position = Vector3.Lerp(transform.position, objectToFollow.position, lerpTime);
        }
        else
        {
            transform.position = objectToFollow.position;

        }
    }
}
