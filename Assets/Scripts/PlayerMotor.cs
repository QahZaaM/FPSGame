using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour {
	[SerializeField]
	private Camera cam;
	[SerializeField]
	private float m_cameraRotationLimit = 85.0f;
	private float m_cameraRotationX = 0.0f;
	private float m_currentCameraRotationX = 0.0f;
	private Vector3 m_velocity = Vector3.zero;
	private Vector3 m_rotation = Vector3.zero;
	private Vector3 m_thrusterForce = Vector3.zero;
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

	public void RotateCamera(float cameraRotationX) {
		m_cameraRotationX = cameraRotationX;
	}

	//Get a force vector for our thrusters
	public void ApplyThruster(Vector3 thrusterForce) {
		m_thrusterForce = thrusterForce;
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

		if(m_thrusterForce != Vector3.zero) {
			rb.AddForce(m_thrusterForce * Time.fixedDeltaTime, ForceMode.Acceleration);
		}
	}

	void PerformRotation() {
		rb.MoveRotation(rb.rotation * Quaternion.Euler(m_rotation));
		if(cam != null) {
			//Set our rotation and clamp it
			m_currentCameraRotationX -= m_cameraRotationX;
			m_currentCameraRotationX = Mathf.Clamp(m_currentCameraRotationX, -m_cameraRotationLimit, m_cameraRotationLimit);

			//Apply our rotation to the transform of our camera
			cam.transform.localEulerAngles = new Vector3(m_currentCameraRotationX, 0.0f, 0.0f);
		}
	}

}
