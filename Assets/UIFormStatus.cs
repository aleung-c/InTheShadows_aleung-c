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

    private bool        Active = false;

	// Use this for initialization
	void OnEnable () {
		FormStatusTitle = transform.Find ("Title").gameObject;
		FormStatusTitleText = FormStatusTitle.transform.Find ("Text").GetComponent<Text> ();

		FormStatusOrientation = transform.Find ("OrientationStatus").gameObject;
		FormStatusOrientationText = FormStatusOrientation.transform.Find ("CurVal").GetComponent<Text> ();

		FormStatusPosition = transform.Find ("PositionStatus").gameObject;
		FormStatusPositionText = FormStatusPosition.transform.Find ("CurVal").GetComponent<Text> ();
        StartCoroutine("UpdateUIStatusRoutine");
        Active = true;

    }

	void OnDisable()
	{
        

    }

    void UpdateUI()
    {
        float CurAngleDiff = Quaternion.Angle(CorrespondingObject.GetComponent<ShadowObject> ().TargetRotation,
            CorrespondingObject.GetComponent<ShadowObject> ().ObjRotation.transform.rotation);
        FormStatusOrientationText.text = (CurAngleDiff.ToString()) + " degres";
    }

    IEnumerator UpdateUIStatusRoutine()
    {
        for (;;)
        {
            if (Active == true)
            {
                UpdateUI();
            }
            yield return new WaitForSeconds(0.3F);
        }
    }

	// Update is called once per frame
	void Update () {
	
	}
}
