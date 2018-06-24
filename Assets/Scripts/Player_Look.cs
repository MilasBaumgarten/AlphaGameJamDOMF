using UnityEngine;

public class Player_Look : MonoBehaviour
{

    Camera cam;
    private Vector3 defaultCamPos;
    private bool zoomState = false;
    public Transform zoomedInPosition;
    public float screenBounds;
    public float lookBounds;
    public float rotaSpeed;
    public GameObject cursor;
    public bool cursorVisibility = false;

    void Start()
    {
		cam = transform.GetChild(0).GetComponent<Camera>();//Camera.main;
        defaultCamPos = cam.transform.position;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = cursorVisibility;
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

    public void ToggleCameraZoom()
    {
        zoomState = !zoomState;
        if (zoomState)
        {
            cam.transform.position = zoomedInPosition.position;
        }
        else
        {
            cam.transform.position = defaultCamPos;
        }
    }
}
