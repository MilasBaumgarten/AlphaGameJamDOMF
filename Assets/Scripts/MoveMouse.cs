using UnityEngine;

public class MoveMouse : MonoBehaviour {
	Camera mainCam;

	bool mouseDragging = false;
	bool mouseOverObject = false;

	public float maxDist = 3.0f;
	public LayerMask collisionLayer;

	public GameObject cursor;

	// Use this for initialization
	void Start() {
		mainCam = Camera.main;
	}

	private void Update() {
		if (mouseOverObject || mouseDragging) {
			if (Input.GetButtonDown("Fire1")) {
				mouseDragging = !mouseDragging;
			}
		}

		if (mouseDragging) {
			// berechen wohin maus bewegt werden soll
			RaycastHit hit;
			Ray mouseToWorldRay = mainCam.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast(mouseToWorldRay, out hit, maxDist, collisionLayer)) {
				Vector3 movementDelta = hit.point - transform.position;
				transform.position += movementDelta;
				cursor.transform.position += new Vector3(movementDelta.x, movementDelta.z, 0);
			}
		}
	}

	private void OnMouseEnter() {
		mouseOverObject = true;
	}

	private void OnMouseExit() {
		mouseOverObject = false;
	}

}
