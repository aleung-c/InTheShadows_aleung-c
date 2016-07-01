using UnityEngine;
using UnityEngine.Events;
using System.Collections;

/// <summary>
/// Game controller. Handles every mode change.
/// </summary>
public class GameController : MonoBehaviour {
	public GameObject			MainPlayer;

	public bool					InMenu = true;
	public bool					InFpsMode = true;
	public bool					InPuzzleMode = false;
	public ShadowLevelObject	ShadowLevelSelected;

	// Use this for initialization
	void Start () {
		MainPlayer = GameManager.instance.PlayerGameObject;
		GameManager.instance.CameraController.OnEndMoveTransition.AddListener (OnEndTransition);
		GameManager.instance.CameraController.BlackScreenTransition ();
	}

	// From fps to puzzle game mode
	public void	FpsToPuzzleMode()
	{
		if (InMenu == false && InPuzzleMode == false)
		{
			// local script mode set;
			InFpsMode = false;
			InPuzzleMode = true;

			// Get Puzzle selected by player;
			ShadowLevelSelected = MainPlayer.GetComponent<AdventurePlayer> ()
				.CollidingPuzzle.GetComponent<ShadowLevelObject> ();

			// listen to completion.
			ShadowLevelSelected.OnPuzzleDone.AddListener(OnPuzzleCompleted);

			// Set Player control variables;
			MainPlayer.GetComponent<AdventurePlayer> ().IsControllable = false;
			MainPlayer.GetComponent<FpsCameraControl> ().enabled = false;
			MainPlayer.GetComponent<FpsMovement> ().enabled = false;

			// Start and set Puzzle UI
			GameManager.instance.MenuGameObject.GetComponent<StartMenuScript>().InPuzzleMenu = true;
			GameManager.instance.MenuGameObject.GetComponent<StartMenuScript>().StartPuzzleMenu(ShadowLevelSelected);

			// Send camera transition order;
			GameManager.instance.CameraController.ChangeCamPlayerToPuzzle(ShadowLevelSelected.PuzzleCamera);
		}
	}

	public void OnPuzzleCompleted()
	{
		if (ShadowLevelSelected)
		{
			// Handle Save
		}
		PuzzleToFpsMode ();
	}

	public void OnEndTransition()
	{
		// When camera finishes transition, send event invoke();
		// Debug.Log ("endtransition");
		if (InPuzzleMode == true) // == transitionning from FPS to PUZZLE;
		{
			ShadowLevelSelected.Playable = true;
			ShadowLevelSelected.StartPlaying ();
		}
		else if (InPuzzleMode == false) // == transitionning from PUZZLE to FPS;
		{
			// local script mode set;
			InFpsMode = true;

			// Set ShadowLevel vars.
			ShadowLevelSelected.Playable = false;
            ShadowLevelSelected.StartPlaying();
			ShadowLevelSelected = null;

			// Set Player control variables -> give back fps control.
            MainPlayer.GetComponent<AdventurePlayer> ().IsControllable = true;
			MainPlayer.GetComponent<FpsCameraControl> ().enabled = true;
			MainPlayer.GetComponent<FpsMovement> ().enabled = true;


		}
	}

	// From puzzle to fps game mode
	public void	PuzzleToFpsMode()
	{
		if (InPuzzleMode == true)
		{
			// local script mode set
			InPuzzleMode = false;

			// Set Puzzle UI;
			GameManager.instance.MenuGameObject.GetComponent<StartMenuScript>().InPuzzleMenu = false;
			GameManager.instance.MenuGameObject.GetComponent<StartMenuScript>().ExitPuzzleMenu();

			// Send camera transition order ==> only after this will the controls be given back.
			// See OnEndTransition() (event called);
			GameManager.instance.CameraController.ChangeCamPuzzleToPlayer ();
		}

	}

	// Update is called once per frame
	void Update () {
	
	}
}
