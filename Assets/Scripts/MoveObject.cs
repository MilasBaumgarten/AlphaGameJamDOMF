using UnityEngine;

public class MoveObject : MonoBehaviour {
	Rigidbody rb;
	Camera mainCam;

	bool mouseDragging = false;
	bool mouseOverObject = false;

	public ObjectsSO objects;

	[Header("Type of Object")]
	public ObjectType objectType = ObjectType.MOUSE;
	private float startHeight;
	private GameObject cursor;

	public enum ObjectType {
		FREE,
		MOUSE
	};

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		mainCam = Camera.main;
		startHeight = transform.position.y;

		cursor = GameObject.Find("Cursor");
		Debug.Log(cursor);
	}

	private void Update() {
		if (mouseOverObject || mouseDragging) {
			if (Input.GetButtonDown("Fire1")) {
				mouseDragging = !mouseDragging;
			}
		}

		if (mouseDragging) {
			rb.useGravity = false;

			// berechen wohin Object bewegt werden soll
			RaycastHit hit;
			Vector3 mousePos;
			Ray mouseToWorldRay = mainCam.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast(mouseToWorldRay, out hit, objects.maxDist, objects.collisionLayer)){
				mousePos = hit.point;
			} else {
				mousePos = mouseToWorldRay.direction.normalized * objects.maxDist + mainCam.transform.position;
			}

			// bewege Objekt abhängig vom Typ
			switch (objectType) {
				case (ObjectType.FREE):
					transform.position = mousePos;
					break;
				case (ObjectType.MOUSE):
					Vector3 nextPos = Vector3.ProjectOnPlane(mousePos, Vector3.up) + (startHeight * Vector3.up);
					Vector3 movementDelta = nextPos - transform.position;
					transform.position += movementDelta;
					cursor.transform.position += new Vector3(movementDelta.x, movementDelta.z, 0);
					break;
			}
		} else {
			rb.useGravity = true;
		}
	}

	private void OnMouseEnter() {
		mouseOverObject = true;
	}

	private void OnMouseExit() {
		mouseOverObject = false;
	}
}