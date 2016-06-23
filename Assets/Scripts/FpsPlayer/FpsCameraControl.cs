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
		//Debug.Log (newMousePos.y.ToString ());
		newMousePos.y = Input.GetAxis ("MouseHorizontal");
		NewCamAngle = Cam.transform.eulerAngles;
		NewCamAngle.x += -newMousePos.x * MouseSensitivityX * Time.deltaTime;
		NewCamAngle.y += newMousePos.y * MouseSensitivityY * Time.deltaTime;
		Cam.transform.eulerAngles = NewCamAngle;
	}
}