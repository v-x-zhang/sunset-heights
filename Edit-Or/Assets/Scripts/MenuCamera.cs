using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCamera : MonoBehaviour
{
    [SerializeField]
    private float sensitivity = 0.05f;

    [SerializeField]
    private float maxYPosition;
    [SerializeField]
    private float maxXPosition;


    private Vector2 screenBounds;
    private Camera mycam;
    private void Start()
    {
        mycam = GetComponent<Camera>();
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

    }
    void Update()
    {
        Vector3 vp = mycam.ScreenToViewportPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, mycam.nearClipPlane));
        vp.x -= 0.5f;
        vp.y -= 0.5f;
        vp.x *= sensitivity;
        vp.y *= sensitivity;
        vp.x += 0.5f;
        vp.y += 0.5f;
        Vector3 sp = mycam.ViewportToScreenPoint(vp);

        Vector3 v = mycam.ScreenToWorldPoint(sp);

        v.x = Mathf.Clamp(v.x, -screenBounds.x, screenBounds.x);
        v.y = Mathf.Clamp(v.y, -screenBounds.y, screenBounds.y);

        transform.LookAt(v, Vector3.up);
    }
}
