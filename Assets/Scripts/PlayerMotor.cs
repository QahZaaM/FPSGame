using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour {
	[SerializeField]
	private Camera cam;
	private Vector3 m_velocity = Vector3.zero;
	private Vector3 m_rotation = Vector3.zero;
	private Vector3 m_cameraRotation = Vector3.zero;
	private Rigidbody rb;

	void Start() {
		rb = GetComponent<Rigidbody>();
	}

	//Gets a movement vector
	public void Move(Vector3 velocity) {
		m_velocity = velocity;
	}

	public void Rotate(Vector3 rotation) {
		m_rotation = rotation;
	}

	public void RotateCamera(Vector3 cameraRotation) {
		m_cameraRotation = cameraRotation;
	}

	//Runs every physics iteration
	void FixedUpdate() {
		PerformMovement();
		PerformRotation();
	}

	//Perform movement based on velocity variable
	void PerformMovement() {
		if (m_velocity != Vector3.zero) {
			rb.MovePosition(rb.position + m_velocity * Time.fixedDeltaTime);
		}
	}

	void PerformRotation() {
		rb.MoveRotation(rb.rotation * Quaternion.Euler(m_rotation));
		if(cam != null) {
			cam.transform.Rotate(-m_cameraRotation);
		}
	}

}
