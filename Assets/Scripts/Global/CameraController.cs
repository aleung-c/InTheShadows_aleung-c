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


	public bool					IsScreenBlack = true;
	public bool					IsScreenWhite = false;

	// screen events
	[SerializeField]
	private SpriteRenderer		BlackScreenSpriteRenderer;
	[SerializeField]
	private SpriteRenderer		WhiteScreenSpriteRenderer;

	[HideInInspector]
	public UnityEvent			OnEndMoveTransition;
	[HideInInspector]
	public UnityEvent			ScreenTransitionStart;
	[HideInInspector]
	public UnityEvent			ScreenTransitionEnd;

	public float				TransitionTime = 1.0F;
	private Color				TmpColor;

	// Use this for initialization
	void Awake () {
		ActiveCamera = Camera.main.gameObject;
		OnEndMoveTransition = new UnityEvent ();
		ScreenTransitionStart = new UnityEvent ();
		ScreenTransitionEnd = new UnityEvent ();
		BlackScreenSpriteRenderer = ActiveCamera.transform.Find ("CamBlackScreen").GetComponent<SpriteRenderer> ();
		WhiteScreenSpriteRenderer = ActiveCamera.transform.Find ("CamWhiteScreen").GetComponent<SpriteRenderer> ();
	}

	void Start()
	{

	}

	public void BlackScreenTransition()
	{
		ScreenTransitionStart.Invoke();
		StartCoroutine ("BlackScreenTransitionRoutine");
	}

	public void WhiteScreenTransition()
	{
		ScreenTransitionStart.Invoke();
		StartCoroutine ("WhiteScreenTransitionRoutine");
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
				OnEndMoveTransition.Invoke (); // when cam transition is over, fire event;
			}
		} else if (GoingBack) {
			ActiveCamera.transform.position = Vector3.Lerp (ActiveCamera.transform.position, PlayerCamPrevPos, LerpForce);
			ActiveCamera.transform.rotation = Quaternion.Lerp (ActiveCamera.transform.rotation, PlayerCamPrevAngle, LerpForce);
			if ((PlayerCamPrevPos - ActiveCamera.transform.position).sqrMagnitude < 0.001F) {
				ChangingCamera = false;
				GoingBack = false;
				OnEndMoveTransition.Invoke (); // when cam transition is over, fire event;
			}
		}
	}

	IEnumerator	BlackScreenTransitionRoutine()
	{
		for (;;) {
			if (IsScreenBlack == true)
			{
				TmpColor = BlackScreenSpriteRenderer.color;
				TmpColor.a = Mathf.Clamp(TmpColor.a - 0.01F, 0.0F, 1.0F);
				BlackScreenSpriteRenderer.color = TmpColor;
				if (BlackScreenSpriteRenderer.color.a == 0.0F)
				{
					IsScreenBlack = false;
					ScreenTransitionEnd.Invoke();
					yield break;
				}
			}
			else 
			{
				TmpColor = BlackScreenSpriteRenderer.color;
				TmpColor.a = Mathf.Clamp(TmpColor.a + 0.01F, 0.0F, 1.0F);
				BlackScreenSpriteRenderer.color = TmpColor;
				if (BlackScreenSpriteRenderer.color.a == 1.0F)
				{
					IsScreenBlack = true;
					ScreenTransitionEnd.Invoke();
					yield break;
				}
			}
			yield return new WaitForSeconds(0.01F);
		}
	}

	IEnumerator	WhiteScreenTransitionRoutine()
	{
		for (;;) {
			if (IsScreenWhite == true)
			{
				TmpColor = WhiteScreenSpriteRenderer.color;
				TmpColor.a = Mathf.Clamp(TmpColor.a - 0.01F, 0.0F, 1.0F);
				WhiteScreenSpriteRenderer.color = TmpColor;
				if (WhiteScreenSpriteRenderer.color.a == 0.0F)
				{
					IsScreenWhite = false;
					ScreenTransitionEnd.Invoke();
					yield return null;
				}
			}
			else 
			{
				TmpColor = WhiteScreenSpriteRenderer.color;
				TmpColor.a = Mathf.Clamp(TmpColor.a + 0.01F, 0.0F, 1.0F);
				WhiteScreenSpriteRenderer.color = TmpColor;
				if (WhiteScreenSpriteRenderer.color.a == 1.0F)
				{
					IsScreenWhite = true;
					ScreenTransitionEnd.Invoke();
					yield return null;
				}
			}
			yield return new WaitForSeconds(0.01F);
		}
	}
}
