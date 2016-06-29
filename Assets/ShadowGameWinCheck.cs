using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class ShadowGameWinCheck : MonoBehaviour {
    public UnityEvent       AllFormOkay;
    public GameObject       FormContainer;

	public int				TargetCorrectNumber;
	public int				CurNbOfCorrect;

	private float			checkMargin;

	private ShadowObject	childScript;
	private GameObject		objRotation;
	private Quaternion		targetRotation;

    void OnStart()
    {
        AllFormOkay = new UnityEvent();
    }

	// Use this for initialization
	void OnEnable () {
		FormContainer = GetComponent<ShadowLevelObject> ().FormContainer;
		TargetCorrectNumber = FormContainer.transform.childCount;
		checkMargin = GetComponent<ShadowLevelObject> ().CheckMargin;
	}
	
	// Update is called once per frame
	void Update () {
		CurNbOfCorrect = 0;
		foreach (Transform Child in FormContainer.transform) {
			childScript = Child.GetComponent<ShadowObject> ();
			objRotation = childScript.ObjRotation;
			targetRotation = childScript.TargetRotation;
			if (Quaternion.Angle(targetRotation, objRotation.transform.rotation) < checkMargin)
			{
				CurNbOfCorrect += 1;
			}

			if (Input.GetKey(KeyCode.F))
			{
				Debug.Log("cur margin = " + Quaternion.Angle(targetRotation, objRotation.transform.rotation));
			}
		}

		if (CurNbOfCorrect == TargetCorrectNumber) {
			AllFormOkay.Invoke();
		}
	}
}
