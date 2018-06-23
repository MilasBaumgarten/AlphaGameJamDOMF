using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player_Look : NetworkBehaviour
{

    Camera cam;
    public float screenBounds;
    public float lookBounds;
    public float rotaSpeed;
    public GameObject cursorPrefab;
	private GameObject cursor;

    void Start()
    {
        cam = Camera.main;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;

		if (isLocalPlayer) {
			CmdSpawnCursor();
			RpcSyncBlockOnce(cursor.transform.localPosition, cursor.transform.localRotation, cursor, GameObject.Find("Canvas"));
		}
    }

    void Update()
    {
		if (isLocalPlayer) {
			Vector2 pos = cam.ScreenToViewportPoint(Input.mousePosition);

			if (pos.x <= screenBounds) {
				cam.transform.rotation = new Quaternion(cam.transform.rotation.x, Mathf.Clamp(cam.transform.rotation.y - rotaSpeed * Time.deltaTime, -lookBounds, lookBounds), cam.transform.rotation.z, cam.transform.rotation.w);
			}
			if (pos.x >= 1.0f - screenBounds) {
				cam.transform.rotation = new Quaternion(cam.transform.rotation.x, Mathf.Clamp(cam.transform.rotation.y + rotaSpeed * Time.deltaTime, -lookBounds, lookBounds), cam.transform.rotation.z, cam.transform.rotation.w);
			}

			cursor.transform.position = new Vector3(pos.x * Screen.width, pos.y * Screen.height, 0.0f);
		}
    }

	[Command]
	void CmdSpawnCursor() {
		cursor = Instantiate(cursorPrefab);
		NetworkServer.Spawn(cursor);
	}

	[ClientRpc]
	public void RpcSyncBlockOnce(Vector3 localPos, Quaternion localRot, GameObject gameObject, GameObject parent) {
		gameObject.transform.parent = parent.transform;
		gameObject.transform.localPosition = localPos;
		gameObject.transform.localRotation = localRot;
	}
}
