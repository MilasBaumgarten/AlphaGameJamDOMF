using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour {
	Rigidbody rb;
	Camera mainCam;

	bool mouseDragging = false;
	bool mouseOverObject = false;

	public float maxDist = 2.0f;
	public float moveSpeed = 1.0f;
	public LayerMask collisionLayer;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		mainCam = Camera.main;
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
			transform.position = mousePos;

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
