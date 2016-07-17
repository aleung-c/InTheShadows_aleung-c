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

	public UnityEvent			OnPuzzleDone;

    // Use this for pre-initialization
    void Awake() {
		OnPuzzleDone = new UnityEvent ();
		PuzzleCamera = transform.Find ("PuzzleCamera").gameObject;
		FormContainer = transform.Find ("FormContainer").gameObject;
		FormCount = FormContainer.transform.childCount;
        ShadowGameplay = GetComponent<ShadowGamePlay>();
        ShadowGameWinCheck = GetComponent<ShadowGameWinCheck>();
    }

	void Start()
	{
		InitializePuzzle ();
	}

	public void StartPlaying()
	{
        ShadowGameplay.enabled = true;
        ShadowGameWinCheck.enabled = true;
		ShadowGameWinCheck.AllFormOkay.AddListener (OnPuzzleSuccess);
    }

    public void ExitPlaying()
    {
        ShadowGameplay.enabled = false;
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

	public void OnPuzzleSuccess()
	{
		Debug.Log ("Puzzle Done !");
        PuzzleDone = true;
        OnPuzzleDone.Invoke ();
	}

    // will be called for save loading;
    public void UnlockPuzzle()
    {
        PuzzleDone = true;
        OnPuzzleDone.Invoke();
    }

	// Update is called once per frame
	void Update () {
	
	}
}
