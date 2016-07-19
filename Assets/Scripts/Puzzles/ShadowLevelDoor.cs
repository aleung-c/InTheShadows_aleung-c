using UnityEngine;
using System.Collections;

public class ShadowLevelDoor : MonoBehaviour {

	private ShadowLevelObject	ShadowLevel;
	public bool					DoorOpened;


	void Awake()
	{
		DoorOpened = false;
	}

	// Use this for initialization
	void Start ()
	{
		ShadowLevel = transform.parent.GetComponent<ShadowLevelObject> ();
		ShadowLevel.OnPuzzleDone.AddListener (OnShadowLevelCompleted);
		ShadowLevel.OnPuzzleUnlock.AddListener (OpenDoorInstant);
		ShadowLevel.OnPuzzlelock.AddListener (CloseDoorInstant);
	}

	/// <summary>
	/// Respond to the OnPuzzleDone event.
	/// </summary>
	void OnShadowLevelCompleted()
	{
		// open door;
		if (DoorOpened == false)
		{
			GetComponent<Animator> ().SetTrigger ("OnOpening");
			DoorOpened = true;
		}
	}

	public void OpenDoorInstant()
	{
		if (DoorOpened == false)
		{
			GetComponent<Animator> ().SetTrigger ("InstantOpen");
			DoorOpened = true;
		}
	}

	public void CloseDoorInstant()
	{
		if (DoorOpened == true)
		{
			GetComponent<Animator> ().SetTrigger ("InstantClose");
			DoorOpened = false;
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
