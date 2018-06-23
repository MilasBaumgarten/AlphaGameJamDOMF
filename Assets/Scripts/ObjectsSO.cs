using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ObjectsSO : ScriptableObject {

	public float maxDist = 3.0f;
    public float followSpeed = 1.0f;
    public float raiseHeight = 0.05f;
	public LayerMask collisionLayer;
	public Vector2 canvasSize = new Vector2(4, 3);
}
