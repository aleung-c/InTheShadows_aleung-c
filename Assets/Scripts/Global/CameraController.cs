using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class CameraController : MonoBehaviour {
	public float				LerpForce = 0.05F;
	public GameObject			ActiveCamera;
	public GameObject			TargetCamPoint;
	public Vector3				PlayerCamPrevPos;
	public Quaternion			PlayerCamPrevAngle;

	public bool					ChangingCamera = false;
	public bool					GoingBack = false;
	public UnityEvent			OnEndTransition;

	// Use this for initialization
	void Awake () {
		ActiveCamera = Camera.main.gameObject;
		OnEndTransition = new UnityEvent ();
	}

	public void ChangeCamPlayerToPuzzle(GameObject NewCamPoint)
	{
		Debug.Log ("new save Camera");
		PlayerCamPrevPos = Camera.main.transform.position;
		PlayerCamPrevAngle = Camera.main.transform.rotation;
		ChangingCamera = true;
		TargetCamPoint = NewCamPoint;
		Debug.Log ("saved cam angle = " + PlayerCamPrevAngle);
	}

	public void ChangeCamPuzzleToPlayer ()
	{
		GoingBack = true;
	}

	void FixedUpdate () {
		if (ChangingCamera && TargetCamPoint) {
			ActiveCamera.transform.position = Vector3.Lerp (ActiveCamera.transform.position, TargetCamPoint.transform.position, LerpForce);
			ActiveCamera.transform.rotation = Quaternion.Lerp (ActiveCamera.transform.rotation, TargetCamPoint.transform.rotation, LerpForce);
			if ((TargetCamPoint.transform.position - ActiveCamera.transform.position).sqrMagnitude < 0.001F) {
				ChangingCamera = false;
				GoingBack = false;
				OnEndTransition.Invoke (); // when cam transition is over, fire event;
			}
		} else if (GoingBack) {
			ActiveCamera.transform.position = Vector3.Lerp (ActiveCamera.transform.position, PlayerCamPrevPos, LerpForce);
			ActiveCamera.transform.rotation = Quaternion.Lerp (ActiveCamera.transform.rotation, PlayerCamPrevAngle, LerpForce);
			if ((PlayerCamPrevPos - ActiveCamera.transform.position).sqrMagnitude < 0.001F) {
				ChangingCamera = false;
				GoingBack = false;
				OnEndTransition.Invoke (); // when cam transition is over, fire event;
			}
		}
	}
}
