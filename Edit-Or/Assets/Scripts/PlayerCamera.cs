using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Camera-Control/Player Camera")]
public class PlayerCamera : MonoBehaviour
{
    public Transform playerBody;
    [SerializeField]
    private float mouseSensitivity = 2f;
    [SerializeField]
    private float maxAngleX = 80f;
    [SerializeField]
    private float minAngleX = -80f;

    private float xRot = 0f;
    private float yRot = 0f;

    // Update is called once per frame
    void Update()
    {
        xRot += mouseSensitivity * Input.GetAxis("Mouse X");
        yRot -= mouseSensitivity * Input.GetAxis("Mouse Y");

        yRot = Mathf.Clamp(yRot, minAngleX, maxAngleX);

        transform.eulerAngles = new Vector3(yRot, xRot, 0);
        playerBody.transform.eulerAngles = new Vector3(0, xRot, 0);
    }
}
