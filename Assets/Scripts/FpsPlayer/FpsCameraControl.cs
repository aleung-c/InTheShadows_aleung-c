using UnityEngine;
using System.Collections;

public class FpsCameraControl : MonoBehaviour {
	private Camera		Cam;
	private Vector3    	newMousePos;
	private Vector3    	NewCamAngle;
	private float		MouseSensitivityX = 1.0f;
	private float		MouseSensitivityY = 1.0f;
	private float		CameraHeightOffset;
	private Vector3		CameraPos;
	
	// Use this for initialization
	void Start ()
	{
		Cam = Camera.main;
		MouseSensitivityX = GameManager.instance.KeyManager.MouseSensitivityX;
		MouseSensitivityY = GameManager.instance.KeyManager.MouseSensitivityY;
		CameraHeightOffset = GetComponent<AdventurePlayer> ().CameraHeightOffset;
	}
	
	// Update is called once per frame
	void Update ()
	{
		// set cam pos
		Cam.transform.position = transform.position;
		CameraPos = Cam.transform.position;
		CameraPos.y += CameraHeightOffset;

		Cam.transform.position = CameraPos;
		
		// set cam angle.
		newMousePos.x = Input.GetAxis ("MouseVertical");
		newMousePos.y = Input.GetAxis ("MouseHorizontal");
		//Debug.Log (newMousePos.x.ToString ());

		//NewCamAngle = Cam.transform.eulerAngles;
		NewCamAngle.x = -newMousePos.x * MouseSensitivityX * Time.deltaTime;
		NewCamAngle.y = newMousePos.y * MouseSensitivityY * Time.deltaTime;
		NewCamAngle.z = 0.0F;
		Cam.transform.Rotate (NewCamAngle);
		NewCamAngle = Cam.transform.eulerAngles;
		NewCamAngle.z = 0.0F;
		Cam.transform.eulerAngles = NewCamAngle;
		// limit cam vertical orientation;
		//Debug.Log (NewCamAngle.x);
		/*if (NewCamAngle.x < 270.0F) {
			NewCamAngle.x = 270.0F;
		} else if (NewCamAngle.x > 90.0F) {
			NewCamAngle.x = 90.0F;
		}*/
		// set cam transform;
		//Cam.transform.eulerAngles = NewCamAngle;
	}
}