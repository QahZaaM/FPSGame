using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

	[SerializeField]
	private float m_speed = 5.0f;
	[SerializeField]
	private float lookSensitivity = 3.0f;
	private PlayerMotor m_motor;

	void Start() {
		m_motor = GetComponent<PlayerMotor>();
	}

	void Update() {
		//Calculate movement velocity as a 3D vector
		float xMove = Input.GetAxisRaw("Horizontal");
		float zMove = Input.GetAxisRaw("Vertical");

		Vector3 moveHorizontal = transform.right * xMove;
		Vector3 moveVertical = transform.forward * zMove;

		//Final Movement vector
		Vector3 velocity = (moveHorizontal + moveVertical).normalized * m_speed;

		//Apply Movement
		m_motor.Move(velocity);

		//calculate rotation as a 3d Vector (turning around)
		float yRotation = Input.GetAxisRaw("Mouse X");

		Vector3 rotation = new Vector3(0.0f, yRotation, 0) * lookSensitivity;

		//Apply rotation
		m_motor.Rotate(rotation);

		//calculate camera rotation as a 3d Vector (turning around)
		float xRotation = Input.GetAxisRaw("Mouse Y");

		Vector3 cameraRotation = new Vector3(xRotation, 0.0f, 0.0f) * lookSensitivity;

		//Apply rotation
		m_motor.RotateCamera(cameraRotation);


	}
}
