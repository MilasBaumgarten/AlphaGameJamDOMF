using UnityEngine;
using UnityEngine.Networking;

public class Player_Look : NetworkBehaviour {
    Camera cam;
    public float screenBounds;
    public float lookBounds;
    public float rotaSpeed;
    public GameObject cursorPrefab;
	private GameObject cursor;

	private void Start() {
		cam = transform.GetChild(0).GetComponent<Camera>();
		CmdSpawnCursor();
	}

	[ClientCallback]
	void Update() {
		if (isLocalPlayer) {
			Vector2 pos = cam.ScreenToViewportPoint(Input.mousePosition);

			if (pos.x <= screenBounds) {
				cam.transform.rotation = new Quaternion(cam.transform.rotation.x, Mathf.Clamp(cam.transform.rotation.y - rotaSpeed * Time.deltaTime, -lookBounds, lookBounds), cam.transform.rotation.z, cam.transform.rotation.w);
			}
			if (pos.x >= 1.0f - screenBounds) {
				cam.transform.rotation = new Quaternion(cam.transform.rotation.x, Mathf.Clamp(cam.transform.rotation.y + rotaSpeed * Time.deltaTime, -lookBounds, lookBounds), cam.transform.rotation.z, cam.transform.rotation.w);
			}

			cursor.transform.position = new Vector3(pos.x * Screen.width, pos.y * Screen.height, 0.0f);
		} else {
			gameObject.SetActive(false);
		}
    }

	[Command]
	void CmdSpawnCursor() {
		cursor = Instantiate(cursorPrefab);
		cursor.GetComponent<PlayerCursor>().parentNetId = GameObject.Find("Canvas").GetComponent<NetworkIdentity>().netId; // Set the parent network ID
		cursor.transform.parent = GameObject.Find("Canvas").transform; // Set the parent transform on the server

		NetworkServer.Spawn(cursor); // Spawn the object

		//cursor = Instantiate(cursorPrefab, GameObject.Find("Canvas").transform);
		//NetworkServer.Spawn(cursor);
	}
}
