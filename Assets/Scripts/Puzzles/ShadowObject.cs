using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class ShadowObject : MonoBehaviour {
	[Header("Object Settings")]
	public bool					HasHorizontalRotation;
	public bool					HasVerticalRotation;
	public bool					HasOffsetDisplacement;

	[Header("Realtime Set Variables")]
	public Quaternion			TargetRotation;
	public Vector3				TargetPosition;

	public GameObject			ObjRotation;
	public GameObject			ObjOffset;

	public FormClickCatcher		ClickCatcher;
	[HideInInspector]
	public UnityEvent			FormDone;
	[HideInInspector]
	public bool					OrderSentFormDone;

	// private:
	private Quaternion			NewRotation;
	private Vector3				NewRotationEuler;
	private Vector3				NewPosition;


	void Awake () {
		FormDone = new UnityEvent ();
		ObjOffset = transform.Find ("ObjOffset").gameObject;
		ObjRotation = ObjOffset.transform.Find ("ObjRotation").gameObject;

		// Set positions the player must reach
		TargetRotation = ObjRotation.transform.GetChild(0).transform.rotation;
		TargetPosition = ObjOffset.transform.position;

		// Required for quick subscribing to FormClickCatcher events.
		ClickCatcher = GetComponentInChildren<FormClickCatcher> ();
	}

	void Start() {

	}

	public void RandomizeHorizontalRotation()
	{
		NewRotationEuler = ObjRotation.transform.eulerAngles;
		NewRotationEuler.y += Random.Range (0.0F, 360.0F);
		ObjRotation.transform.eulerAngles = NewRotationEuler;

		/*
		NewRotation = ObjRotation.transform.GetChild(0).transform.rotation;
		NewRotation = Quaternion.AngleAxis(Random.Range (0.0F, 360.0F), Vector3.up);
		ObjRotation.transform.GetChild(0).transform.rotation = NewRotation;
		*/
	}

	public void RandomizeVerticalRotation()
	{
		NewRotationEuler = ObjRotation.transform.eulerAngles;
		NewRotationEuler.x += Random.Range (0.0F, 360.0F);
		ObjRotation.transform.eulerAngles = NewRotationEuler;
		/*
		NewRotation = ObjRotation.transform.GetChild(0).transform.rotation;
		NewRotation = Quaternion.AngleAxis(Random.Range (0.0F, 360.0F), ObjRotation.transform.forward);
		ObjRotation.transform.GetChild(0).transform.rotation = NewRotation;*/
	}

	public void RandomizePosition()
	{
		NewPosition.x = Random.Range(-0.5F, 0.5F);
		NewPosition.y = Random.Range(-0.5F, 0.5F);
		NewPosition.z = ObjOffset.transform.localPosition.z;
		ObjOffset.transform.localPosition = NewPosition;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
