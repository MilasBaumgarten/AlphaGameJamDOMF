using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerCursor : NetworkBehaviour {

	[SyncVar]
	public NetworkInstanceId parentNetId;

	public override void OnStartClient() {
		GameObject parentObject = ClientScene.FindLocalObject(GameObject.Find("Canvas").GetComponent<NetworkIdentity>().netId);
		transform.SetParent(parentObject.transform);
	}
}
