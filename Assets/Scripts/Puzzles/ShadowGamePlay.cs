using UnityEngine;
using UnityEngine.Events;
using System.Collections;

// Class proto for Event with params.
public class ObjectClickingEvent : UnityEvent<GameObject>{}

/// <summary>
/// Shadow game play. Works with event sending clicked form;
/// </summary>
public class ShadowGamePlay : MonoBehaviour {
	public GameObject FormContainer;

	void Awake ()
	{
		FormContainer = transform.Find ("FormContainer").gameObject;
	}

	// Use this for initialization
	void Start () {
		foreach (Transform Child in FormContainer.transform) {
			Child.GetComponent<ShadowObject> ().ClickCatcher.OnClickDown.AddListener(OnFormMouseDown);
			Child.GetComponent<ShadowObject> ().ClickCatcher.OnClickUp.AddListener(OnFormMouseUp);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// will be applied on each clicked form
	public void OnFormMouseDown(GameObject Form)
	{
		Debug.Log ("Clicked on " + Form.name);
	}

	public void OnFormMouseUp(GameObject Form)
	{
		Debug.Log ("Released on " + Form.name);
	}
}
