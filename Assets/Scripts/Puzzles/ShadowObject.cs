using UnityEngine;
using System.Collections;

public class ShadowObject : MonoBehaviour {
	public Vector3		TargetRotation;
	public GameObject	ObjOffset;
	public GameObject	ObjRotation;
	public FormClickCatcher	ClickCatcher;

	private Vector3		NewRotation;


	void Awake () {
		ObjOffset = transform.Find ("ObjOffset").gameObject;
		ObjRotation = ObjOffset.transform.Find ("ObjRotation").gameObject;
		TargetRotation = ObjRotation.transform.eulerAngles;
		ClickCatcher = GetComponentInChildren<FormClickCatcher> ();
	}

	void Start() {

	}

	public void RandomizeHorizontalRotation()
	{
        NewRotation.x = ObjRotation.transform.eulerAngles.x;
        NewRotation.y = Random.Range (0.0F, 360.0F);
		NewRotation.z = ObjRotation.transform.eulerAngles.z;
		ObjRotation.transform.eulerAngles = NewRotation;
	}

	public void RandomizeVerticalRotation()
	{
        NewRotation.x = Random.Range(0.0F, 360.0F);
        NewRotation.y = ObjRotation.transform.eulerAngles.y;
		NewRotation.z = ObjRotation.transform.eulerAngles.z;
		ObjRotation.transform.eulerAngles = NewRotation;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
