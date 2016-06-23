using UnityEngine;
using System.Collections;

public class AdventurePlayer : MonoBehaviour {

	public float				WalkingSpeed = 1.0F;
	public float				RunningSpeed = 2.0F;
	public CharacterController	PlayerCC;
	public float				CameraHeightOffset = 1.0F;

	// Use this for initialization
	void Awake () {
		PlayerCC = GetComponent<CharacterController> ();
	}

	void SyncPosWithCharController()
	{

	}

	// Update is called once per frame
	void Update () {
		SyncPosWithCharController ();
	}
}
