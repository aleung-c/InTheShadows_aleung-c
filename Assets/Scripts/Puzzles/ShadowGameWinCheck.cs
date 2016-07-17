using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class ShadowGameWinCheck : MonoBehaviour {
    public UnityEvent       AllFormOkay;
    public GameObject       FormContainer;

	public int				TargetCorrectNumber;
	public int				CurNbOfCorrect;

	private float			checkMarginRotation;
	private float			checkMarginPosition;

	private ShadowObject	childScript;
	private GameObject		objRotation;
	private Quaternion		targetRotation;

	// protect multi event sending
	private bool			PuzzleDoneOrderSent;

    void OnStart()
    {
        AllFormOkay = new UnityEvent();
    }

	// Use this for initialization
	void OnEnable () {
		PuzzleDoneOrderSent = false;
		FormContainer = GetComponent<ShadowLevelObject> ().FormContainer;
		TargetCorrectNumber = FormContainer.transform.childCount;
		checkMarginRotation = GetComponent<ShadowLevelObject> ().CheckMarginRotation;
		checkMarginPosition = GetComponent<ShadowLevelObject> ().CheckMarginPosition;
		// reset child order sending bool;
		foreach (Transform Child in FormContainer.transform) {
			Child.GetComponent<ShadowObject> ().OrderSentFormDone = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		CurNbOfCorrect = 0;
		foreach (Transform Child in FormContainer.transform)
		{
			childScript = Child.GetComponent<ShadowObject> ();
			objRotation = childScript.ObjRotation;
			targetRotation = childScript.TargetRotation;
			if (Quaternion.Angle(targetRotation, objRotation.transform.GetChild(0).transform.rotation) < checkMarginRotation)
			{
				CurNbOfCorrect += 1;
				if (!childScript.OrderSentFormDone)
					childScript.FormDone.Invoke();
			}
		}

		if (!PuzzleDoneOrderSent && CurNbOfCorrect == TargetCorrectNumber) {
			AllFormOkay.Invoke();
			PuzzleDoneOrderSent = true;
		}
	}
}
