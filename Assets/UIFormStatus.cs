using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIFormStatus : MonoBehaviour {
	public GameObject	FormStatusTitle;
	public Text			FormStatusTitleText;

	public GameObject	FormStatusOrientation;
	public Text			FormStatusOrientationText;

	public GameObject	FormStatusPosition;
	public Text			FormStatusPositionText;

	public GameObject	CorrespondingObject;

	// Use this for initialization
	void OnEnable () {
		FormStatusTitle = transform.Find ("Title").gameObject;
		FormStatusTitleText = FormStatusTitle.transform.Find ("Text").GetComponent<Text> ();

		FormStatusOrientation = transform.Find ("OrientationStatus").gameObject;
		FormStatusOrientationText = FormStatusOrientation.transform.Find ("CurVal").GetComponent<Text> ();

		FormStatusPosition = transform.Find ("PositionStatus").gameObject;
		FormStatusPositionText = FormStatusPosition.transform.Find ("CurVal").GetComponent<Text> ();
	}

	void OnDisable()
	{

	}

	// Update is called once per frame
	void Update () {
	
	}
}
