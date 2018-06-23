using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : Photon.PunBehaviour {

	public PhotonLogLevel loglevel = PhotonLogLevel.Full;
	bool connectedToRoom = false;
	string gameVersion = "1";
	bool isLoaded;

	private void Awake() {
		PhotonNetwork.logLevel = loglevel;
		PhotonNetwork.autoJoinLobby = false;
		PhotonNetwork.automaticallySyncScene = true;
	}

	private void Start() {
		if (PhotonNetwork.connected) {
			PhotonNetwork.JoinRandomRoom();
		} else {
			PhotonNetwork.ConnectUsingSettings(gameVersion);
		}
	}

	private void Update() {
		if (isLoaded) {
			return;
		}

		if (connectedToRoom) {
			if (PhotonNetwork.isMasterClient) {
				LoadLevel();
			}
		}
	}

	public override void OnPhotonRandomJoinFailed(object[] codeAndMsg) {
		Debug.LogWarning("Failed to join room, creating new one");
		PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = 10 }, null);
	}

	public override void OnConnectedToMaster() {
		PhotonNetwork.JoinRandomRoom();
		connectedToRoom = true;
	}

	public void LoadLevel() {
		if (!PhotonNetwork.isMasterClient) {

		} else {
			PhotonNetwork.LoadLevel(1);
			isLoaded = true;
		}
	}

	private void OnApplicationQuit() {
		Debug.Log("Quitting game");
		QuitGame();
	}

	public void QuitGame() {
		PhotonNetwork.LeaveRoom();
	}
}
