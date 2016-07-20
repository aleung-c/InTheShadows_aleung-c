using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIFormStatus : MonoBehaviour {
	public GameObject		FormStatusTitle;
	public Text				FormStatusTitleText;

	public GameObject		FormStatusOrientation;
	public Text				FormStatusOrientationText;

	public GameObject		FormStatusPosition;
	public Text				FormStatusPositionText;

	// Set by menu controller;
	public GameObject		CorrespondingObject;

    private bool        	Active = false;
	private float			CurAngleDiff;
	private float			CurPosDiff;
	private float			PercentageCalculation;
	private ShadowObject	FormScript;

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
		StopAllCoroutines ();
    }

    void UpdateUI()
    {
		FormScript = CorrespondingObject.GetComponent<ShadowObject> ();

		// set Name
		// FormStatusTitleText = (FormScript.gameObject.name.ToString());

		// Update Angle Value display;
		CurAngleDiff = Quaternion.Angle (FormScript.TargetRotation, FormScript.ObjRotation.transform.GetChild(0).transform.rotation);

		// Make it look like a percentage
		PercentageCalculation = 100.0F - CurAngleDiff;
		if (PercentageCalculation < 0.0F)
			PercentageCalculation = 0.0F;
		FormStatusOrientationText.text = (PercentageCalculation.ToString("F2")) + " %";

		// Update OffsetPosition Value display;
		if (FormScript.HasOffsetDisplacement)
		{
			CurPosDiff = Vector3.Distance(FormScript.TargetPosition, FormScript.ObjOffset.transform.position) * 10.0F;
			//Debug.Log(CurPosDiff);
			PercentageCalculation = 100.0F - CurPosDiff;
			if (PercentageCalculation < 0.0F)
				PercentageCalculation = 0.0F;
			FormStatusPositionText.text = (PercentageCalculation.ToString ("F2")) + " %";
		}
		else
		{
			FormStatusPositionText.text = "OK";
		}
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
