using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ObjectsSO : ScriptableObject {

	public float maxDist = 3.0f;
	public LayerMask collisionLayer;

	[Range(0,5)]
	public float mouseSensitivityInGame = 1.0f;
	public Vector2 canvasSize = new Vector2(4, 3);
}
