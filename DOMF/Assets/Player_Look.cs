using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Look : MonoBehaviour
{

    Camera cam;
    public float screenBounds;
    public float lookBounds;
    public float rotaSpeed;
    public GameObject cursor;

    void Start()
    {
        cam = Camera.main;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    void Update()
    {
        Vector2 pos = cam.ScreenToViewportPoint(Input.mousePosition);

        if (pos.x <= screenBounds)
        {
            cam.transform.rotation = new Quaternion(cam.transform.rotation.x, Mathf.Clamp(cam.transform.rotation.y - rotaSpeed * Time.deltaTime, -lookBounds, lookBounds), cam.transform.rotation.z, cam.transform.rotation.w);
        }
        if (pos.x >= 1.0f - screenBounds)
        {
            cam.transform.rotation = new Quaternion(cam.transform.rotation.x, Mathf.Clamp(cam.transform.rotation.y + rotaSpeed * Time.deltaTime, -lookBounds, lookBounds), cam.transform.rotation.z, cam.transform.rotation.w);
        }

        cursor.transform.position = new Vector3(pos.x * Screen.width, pos.y * Screen.height, 0.0f);
    }
}
