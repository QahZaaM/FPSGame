using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerShoot : NetworkBehaviour {
	
	public PlayerWeapon m_weapon;

	[SerializeField]
	private Camera m_cam;
	
	[SerializeField]
	private LayerMask m_mask;
	private const string PLAYER_TAG = "Player";

	void Start() {
		if(m_cam == null){
			Debug.LogError("PlayerShoot: No camera referenced!");
			this.enabled = false;
		}
	}

	void Update() {
		if(Input.GetButtonDown("Fire1")) {
			Shoot();
		}
	
	}
	[Client]	
	void Shoot() {
		RaycastHit hit;
		if(Physics.Raycast(m_cam.transform.position, m_cam.transform.forward, out hit, m_weapon.range, m_mask)) {
			if(hit.collider.tag == PLAYER_TAG) {
				CmdPlayerShot(hit.collider.name);
			}
		}
	}

	[Command]
	void CmdPlayerShot(string ID) {
		Debug.Log(ID + " has been shot.");
	}
}
