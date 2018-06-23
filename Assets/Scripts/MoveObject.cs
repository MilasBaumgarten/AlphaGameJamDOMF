using UnityEngine;
using UnityEngine.Networking;

public class MoveObject : NetworkBehaviour {
	Rigidbody rb;
	Camera mainCam;

	public float raiseHeight = 0.1f;

	bool mouseDragging = false;
	bool mouseOverObject = false;

	public ObjectsSO objects;

	[Header("Type of Object")]
	public ObjectType objectType = ObjectType.MOUSE;
	private Vector3 startPosition;
	private Quaternion startRotation;
	private GameObject cursor;

	public enum ObjectType {
		FREE,
		MOUSE
	};

	// stuff einstellen
	void Start () {
		rb = GetComponent<Rigidbody>();
		mainCam = Camera.main;
		startPosition = transform.position;
		startRotation = transform.rotation;

		cursor = GameObject.Find("Cursor");
	}

	private void Update() {
		// Objekt halten/ loslassen
		if (mouseOverObject || mouseDragging) {
			if (Input.GetButtonDown("Fire1")) {
				mouseDragging = !mouseDragging;
			}
		}

		// objekt wird gehalten
		if (mouseDragging) {
			rb.useGravity = false;
			Drag();
			
		} else {
			rb.useGravity = true;
		}
	}

	void Drag() {
		RaycastHit hit;
		Vector3 mousePos;
		Ray mouseToWorldRay = mainCam.ScreenPointToRay(Input.mousePosition);

		// berechen wohin Object bewegt werden soll
		if (Physics.Raycast(mouseToWorldRay, out hit, objects.maxDist, objects.collisionLayer)) {
			mousePos = hit.point;
		} else {
			mousePos = mouseToWorldRay.direction.normalized * objects.maxDist + mainCam.transform.position;
		}

		// Objekt hängt so nicht mehr im Boden fest
		mousePos.y += GetComponent<BoxCollider>().size.y / 2 + raiseHeight;

		// bewege Objekt abhängig vom Typ
		switch (objectType) {
			case (ObjectType.FREE):
				//transform.position = mousePos;
				rb.MovePosition(mousePos);
				break;
			case (ObjectType.MOUSE):
				// bewege Maus nur auf Oberfläche
				if (hit.point != Vector3.zero) {
					// bewege Maus
					Vector3 nextPos = Vector3.ProjectOnPlane(mousePos, Vector3.up) + (startPosition.y * Vector3.up);
					Vector3 movementDelta = nextPos - transform.position;
					transform.position += movementDelta;
					// bewege Cursor
					cursor.transform.localPosition += new Vector3(movementDelta.x, movementDelta.z, 0) * objects.mouseSensitivityInGame;

					// clampe die inGame Maus auf Bildschirm
					Vector2 boundaries;
					boundaries.x = (objects.canvasSize.x / 2) - (cursor.GetComponent<RectTransform>().sizeDelta.x / 2);
					boundaries.y = (objects.canvasSize.y / 2) - (cursor.GetComponent<RectTransform>().sizeDelta.x / 2);
					cursor.transform.localPosition = new Vector3(Mathf.Clamp(cursor.transform.localPosition.x, -boundaries.x, boundaries.x),
																 Mathf.Clamp(cursor.transform.localPosition.y, -boundaries.y, boundaries.y), 0);
				}
				break;
		}
	}

	private void OnMouseEnter() {
		mouseOverObject = true;
	}

	private void OnMouseExit() {
		mouseOverObject = false;
	}

	public void Reset() {
		Debug.Log("RESET");
		this.transform.position = startPosition;
		this.transform.rotation = startRotation;
	}
}