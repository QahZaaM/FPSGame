using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour {

	[SerializeField]
	Behaviour[] m_componentsToDisable;
	
	[SerializeField]
	string m_remoteLayerName = "RemotePlayer";

	Camera sceneCamera;

	void Start() {
		if(!isLocalPlayer) {
			DisableComponents();
			AssignRemoteLayer();
		} else {
			sceneCamera = Camera.main;
			if(sceneCamera != null) {
				sceneCamera.gameObject.SetActive(false);
			}
		}

		RegisterPlayer();		
	}

	void RegisterPlayer() {
		string ID = "Player " + GetComponent<NetworkIdentity>().netId;
		transform.name = ID;
	}

	void AssignRemoteLayer() {
		gameObject.layer = LayerMask.NameToLayer(m_remoteLayerName);
	}

	void DisableComponents() {
		for(int i = 0; i < m_componentsToDisable.Length; i++) {
				m_componentsToDisable[i].enabled = false;
			}
	}

	void OnDisable() {
		if(sceneCamera != null) {
			sceneCamera.gameObject.SetActive(true);
		}
	}
}
