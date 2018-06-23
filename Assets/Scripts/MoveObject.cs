using UnityEngine;

public class MoveObject : MonoBehaviour {
	Rigidbody rb;
	Camera mainCam;

	bool mouseDragging = false;
	bool mouseOverObject = false;

	public float maxDist = 3.0f;
	public LayerMask collisionLayer;

	[Header("restrict Object Movement to X and Z Axis")]
	public bool confineTo2D = false;
	private float startHeight;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		mainCam = Camera.main;
		startHeight = transform.position.y;
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

			if (Physics.Raycast(mouseToWorldRay, out hit, maxDist, collisionLayer)){
				mousePos = hit.point;
			} else {
				mousePos = mouseToWorldRay.direction.normalized * maxDist + mainCam.transform.position;
			}

			if (confineTo2D) {
				transform.position = Vector3.ProjectOnPlane(mousePos, Vector3.up) + (startHeight * Vector3.up);
			} else {
				transform.position = mousePos;
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
