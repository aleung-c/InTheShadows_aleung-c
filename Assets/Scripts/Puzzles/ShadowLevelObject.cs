using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Shadow level object. Main controller for puzzles
/// </summary>
public class ShadowLevelObject : MonoBehaviour {
	[Header("Puzzle Settings")]
	public int					PuzzleNumber;
	public string				PuzzleName;
    public float                RotationSpeed = 1.0F;
	public float    			DisplacementSpeed = 1.0F;

	[Header("Puzzle Check Margin")]
	public float				CheckMarginRotation;
	public float				CheckMarginPosition;

	[Header("Puzzle Forms Containers")]
	public GameObject			FormContainer;
	public List<GameObject>		Forms = new List<GameObject>();

	[Header("GamePlay State")]
	public GameObject			PuzzleCamera;
	public bool                 Playable = false;
    public bool                 PuzzleDone = false;
	[HideInInspector]
	public int					FormCount;
	
    private ShadowGamePlay      ShadowGameplay;
    private ShadowGameWinCheck  ShadowGameWinCheck;
	private ShadowObject		CurrentShadowForm;

	// References for distant order sending.
	[HideInInspector] 
	public	ShadowLevelDoor		DoorScript;
	public	ShadowLevelLight	LightScript;

	[HideInInspector]
	public UnityEvent			OnPuzzleDone;
	[HideInInspector]
	public UnityEvent			OnPuzzleUnlock;
	[HideInInspector]
	public UnityEvent			OnPuzzlelock;
	[HideInInspector]
	public bool					PuzzleDoneOrderSent;


    // Use this for pre-initialization
    void Awake() {
		// Init events
		OnPuzzleDone = new UnityEvent ();

		// Set Object references
		PuzzleCamera = transform.Find ("PuzzleCamera").gameObject;
		FormContainer = transform.Find ("FormContainer").gameObject;
		FormCount = FormContainer.transform.childCount;

		// Set references scripts.
        ShadowGameplay = GetComponent<ShadowGamePlay>();
        ShadowGameWinCheck = GetComponent<ShadowGameWinCheck>();
		DoorScript = transform.Find ("Door").GetComponent<ShadowLevelDoor> ();
		LightScript = transform.Find ("Light").GetComponent<ShadowLevelLight> ();
    }

	void Start()
	{
		InitializePuzzle ();
	}

	void InitializePuzzle()
	{
		// init puzzle random settings
		foreach (Transform child in FormContainer.transform)
		{
			CurrentShadowForm = child.gameObject.GetComponent<ShadowObject> ();
			
			if (CurrentShadowForm.HasHorizontalRotation)
				CurrentShadowForm.RandomizeHorizontalRotation();
			
			if (CurrentShadowForm.HasVerticalRotation)
				CurrentShadowForm.RandomizeVerticalRotation();
			
			if (CurrentShadowForm.HasOffsetDisplacement)
				CurrentShadowForm.RandomizePosition();
		}
	}

	public void StartPlaying()
	{
		PuzzleDoneOrderSent = false;
        ShadowGameplay.enabled = true;
        ShadowGameWinCheck.enabled = true;
		if (PuzzleDone == true) { // retrying puzzle. Rerandomize;
			InitializePuzzle();
		}

		ShadowGameWinCheck.AllFormOkay.AddListener (OnPuzzleSuccess);
    }

    public void ExitPlaying()
    {
        ShadowGameplay.enabled = false;
		ShadowGameWinCheck.AllFormOkay.RemoveListener (OnPuzzleSuccess);
		ShadowGameWinCheck.enabled = false;
    }

	/// <summary>
	/// Raises the puzzle success event.
	/// </summary>
	public void OnPuzzleSuccess()
	{
		if (PuzzleDoneOrderSent == false) {
			PuzzleDoneOrderSent = true;
			Debug.Log ("Puzzle Done !");
            GetComponent<AudioSource>().Play();
	        PuzzleDone = true;
			ShadowGameplay.Clicking = false;
			ShadowGameplay.enabled = false;
			ShadowGameWinCheck.enabled = false;
			OnPuzzleDone.Invoke ();
		}
	}

    // will be called for save loading;
    public void UnlockPuzzle()
    {
        PuzzleDone = true;
		OnPuzzleUnlock.Invoke();
    }

	public void LockPuzzle()
	{
		PuzzleDone = false;
		OnPuzzlelock.Invoke ();
	}

	// Update is called once per frame
	void Update () {
	
	}
}
