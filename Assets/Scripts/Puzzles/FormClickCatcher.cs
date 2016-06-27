using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class FormClickCatcher : MonoBehaviour {
	private GameObject				FormRoot;
	public ObjectClickingEvent		OnClickDown;
	public ObjectClickingEvent		OnClickUp;

	// Use this for initialization
	void Awake () {
		FormRoot = transform.parent.gameObject;
		OnClickDown = new ObjectClickingEvent ();
		OnClickUp = new ObjectClickingEvent ();
	}

	void OnMouseDown ()
	{
		OnClickDown.Invoke (FormRoot);
	}

	void OnMouseUp ()
	{
		OnClickUp.Invoke (FormRoot);
	}

	// Update is called once per frame
	void Update () {
	
	}
}
