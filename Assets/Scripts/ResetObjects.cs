using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetObjects : MonoBehaviour {

	private void OnCollisionEnter(Collision collision) {
		collision.collider.GetComponent<MoveObject>().Reset();
	}
}
