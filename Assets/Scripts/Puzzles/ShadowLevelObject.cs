using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Shadow level object. Main controller for puzzles
/// </summary>
public class ShadowLevelObject : MonoBehaviour {
	[Header("Puzzle Settings")]
	public int					PuzzleNumber;
	public string				PuzzleName;
	public GameObject			PuzzleCamera;
    public float                RotationSpeed = 3.0F;

	public bool					HasHorizontalRotation;
	public bool					HasVerticalRotation;
	public bool					HasOffsetDisplacement;
	public bool					IsMultiObjects;

	[Header("Puzzle Check Margin")]
	public float				CheckMargin;

	[Header("Puzzle Forms Containers")]
	public GameObject			FormContainer;
	public List<GameObject>		Forms = new List<GameObject>();

	[Header("GamePlay State")]
	public bool                 Playable = false;
	[HideInInspector]
	public int					FormCount;

    [SerializeField]
    private ShadowGamePlay      ShadowGameplay;
    private ShadowGameWinCheck  ShadowGameWinCheck;

    // Use this for pre-initialization
    void Awake() {
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
		// init puzzle random settings etc ...
		foreach (Transform child in FormContainer.transform) {
			if (HasHorizontalRotation)
				child.gameObject.GetComponent<ShadowObject> ().RandomizeHorizontalRotation();
			if (HasVerticalRotation)
				child.gameObject.GetComponent<ShadowObject> ().RandomizeVerticalRotation();
		}
	}

	public void OnPuzzleSuccess()
	{
		Debug.Log ("Puzzle Done !");
	}

	// Update is called once per frame
	void Update () {
	
	}
}
