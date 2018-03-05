using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ConfigurableJoint))]
[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

	[SerializeField]
	private float m_speed = 5.0f;
	[SerializeField]
	private float lookSensitivity = 3.0f;
	[SerializeField]
	private float m_thrusterForce = 1000.0f;

	[Header("Spring Settings")]
	[SerializeField]
	private JointDriveMode m_jointMode = JointDriveMode.Position;
	[SerializeField]
	private float m_jointSpring = 20.0f;
	[SerializeField]
	private float m_jointMaxForce = 40.0f;

	private PlayerMotor m_motor;
	private ConfigurableJoint m_joint;

	void Start() {
		m_motor = GetComponent<PlayerMotor>();
		m_joint = GetComponent<ConfigurableJoint>();

		SetJointSettings(m_jointSpring);
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

		float cameraRotationX = xRotation * lookSensitivity;

		//Apply rotation
		m_motor.RotateCamera(cameraRotationX);

		//Calculate the thruster force based on input
		Vector3 thrusterForce = Vector3.zero;
		if(Input.GetButton("Jump")) {
			thrusterForce = Vector3.up * m_thrusterForce;
			SetJointSettings(0f);
		} else {
			SetJointSettings(m_jointSpring);
		}
		
		//Apply Thruster force
		m_motor.ApplyThruster(thrusterForce);
	}

	private void SetJointSettings(float jointSpring) {
		m_joint.yDrive = new JointDrive { 
			mode = m_jointMode, 
			positionSpring = jointSpring,
			maximumForce = m_jointMaxForce 
		};
	}
}
