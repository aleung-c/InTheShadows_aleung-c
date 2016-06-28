﻿using UnityEngine;
using System.Collections;

public class FpsMovement : MonoBehaviour {
	// Keys
	KeyCode						MoveForward;
	KeyCode						MoveForwardAlt;

	KeyCode						MoveBackward;
	KeyCode						MoveBackwardAlt;

	KeyCode						StrafeLeft;
	KeyCode						StrafeLeftAlt;

	KeyCode						StrafeRight;
	KeyCode						StrafeRightAlt;

	private KeyControlManager 	KManager;

	// Movement variables.
	private Camera				PlayerCam;
	private CharacterController	PlayerCC;
	private float				WalkingSpeed;
	private float				RunningSpeed;

	Vector3						MovementDirection;

	// Use this for initialization
	void Start () {
		// Set keys from KeyControlManager from the global GameManager;
		PlayerCam = Camera.main;
		PlayerCC = GetComponent<AdventurePlayer> ().PlayerCC;
		KManager = GameManager.instance.KeyManager;

		MoveForward = KManager.MoveForward;
		MoveForwardAlt = KManager.MoveForwardAlt;

		MoveBackward = KManager.MoveBackward;
		MoveBackwardAlt = KManager.MoveBackwardAlt;

		StrafeLeft = KManager.StrafeLeft;
		StrafeLeftAlt = KManager.StrafeLeftAlt;

		StrafeRight = KManager.StrafeRight;
		StrafeRightAlt = KManager.StrafeRightAlt;

		// get game vars;
		WalkingSpeed = GetComponent<AdventurePlayer> ().WalkingSpeed;
		RunningSpeed = GetComponent<AdventurePlayer> ().RunningSpeed;
	}

	void HandleMovementInput()
	{
		// first press forward
		if (Input.GetKey (MoveForward) || Input.GetKey (MoveForwardAlt)) {
			MovementDirection = PlayerCam.transform.forward;
			if (Input.GetKey (StrafeLeft) || Input.GetKey (StrafeLeftAlt))
			{
				MovementDirection = Quaternion.AngleAxis(-45, Vector3.up) * MovementDirection;
			}
			else if (Input.GetKey (StrafeRight) || Input.GetKey (StrafeRightAlt))
			{
				MovementDirection = Quaternion.AngleAxis(45, Vector3.up) * MovementDirection;
			}
			PlayerCC.SimpleMove (MovementDirection * WalkingSpeed * Time.deltaTime * 100.0F);
		}
		// first press backward
		else if (Input.GetKey (MoveBackward) || Input.GetKey (MoveBackwardAlt)) {
			MovementDirection = -PlayerCam.transform.forward;
			if (Input.GetKey (StrafeLeft) || Input.GetKey (StrafeLeftAlt))
			{
				MovementDirection = Quaternion.AngleAxis(45, Vector3.up) * MovementDirection;
			}
			else if (Input.GetKey (StrafeRight) || Input.GetKey (StrafeRightAlt))
			{
				MovementDirection = Quaternion.AngleAxis(-45, Vector3.up) * MovementDirection;
			}
			PlayerCC.SimpleMove (MovementDirection * WalkingSpeed * Time.deltaTime * 100.0F);
		}
		// first press Strafe left
		else if (Input.GetKey (StrafeLeft) || Input.GetKey (StrafeLeftAlt)) {
			MovementDirection = -PlayerCam.transform.right;
			if (Input.GetKey (MoveForward) || Input.GetKey (MoveForwardAlt))
			{
				MovementDirection = Quaternion.AngleAxis(-45, Vector3.up) * MovementDirection;
			}
			else if (Input.GetKey (MoveBackward) || Input.GetKey (MoveBackwardAlt))
			{
				MovementDirection = Quaternion.AngleAxis(45, Vector3.up) * MovementDirection;
			}
			PlayerCC.SimpleMove (MovementDirection * WalkingSpeed * Time.deltaTime * 100.0F);
		}
		// first press Strafe Right
		else if (Input.GetKey (StrafeRight) || Input.GetKey (StrafeRightAlt)) {
			MovementDirection = PlayerCam.transform.right;
			if (Input.GetKey (MoveForward) || Input.GetKey (MoveForwardAlt))
			{
				MovementDirection = Quaternion.AngleAxis(-45, Vector3.up) * MovementDirection;
			}
			else if (Input.GetKey (MoveBackward) || Input.GetKey (MoveBackwardAlt))
			{
				MovementDirection = Quaternion.AngleAxis(45, Vector3.up) * MovementDirection;
			}

			PlayerCC.SimpleMove (MovementDirection * WalkingSpeed * Time.deltaTime * 100.0F);
		}



	}

	// Update is called once per frame
	void Update () {
		HandleMovementInput ();

	}
}