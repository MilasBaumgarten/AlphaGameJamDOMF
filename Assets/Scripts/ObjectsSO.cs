using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ObjectsSO : ScriptableObject {
	public float maxDist = 3.0f;
	public LayerMask collisionLayer;
}
