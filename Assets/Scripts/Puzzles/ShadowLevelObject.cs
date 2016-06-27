using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Shadow level object. Main controller for 
/// </summary>
public class ShadowLevelObject : MonoBehaviour {
	[Header("Puzzle Settings")]
	public int					PuzzleNumber;
	public string				PuzzleName;
	public GameObject			PuzzleCamera;

	public bool					HasHorizontalRotation;
	public bool					HasVerticalRotation;
	public bool					HasOffsetDisplacement;
	public bool					IsMultiObjects;

	[Header("Puzzle Forms Containers")]
	public GameObject			FormContainer;
	public List<GameObject>		Forms = new List<GameObject>();

	[Header("GamePlay State")]
	public bool Playable = false;

	// Use this for pre-initialization
	void Awake() {
		PuzzleCamera = transform.Find ("PuzzleCamera").gameObject;
		FormContainer = transform.Find ("FormContainer").gameObject;
	}

	void Start()
	{
		InitializePuzzle ();
	}

	public void StartPlaying()
	{

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

	// Update is called once per frame
	void Update () {
	
	}
}
