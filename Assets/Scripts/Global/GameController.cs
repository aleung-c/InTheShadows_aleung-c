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

	private int					LevelToUnlock;
	private Vector3				NewPlayerPosition;

	// Use this for initialization
	void Start () {
		// Before anything visible begins.
		MainPlayer = GameManager.instance.PlayerGameObject;
        GameManager.instance.CameraController.OnEndMoveTransition.AddListener(OnEndTransition);

		// ------------ Set world from Save
		// Place Player;
		NewPlayerPosition = GameManager.instance.PlayerGameObject.transform.position;
		NewPlayerPosition.x = SaveManager.CurrentSave.PlayerPositionX;
		NewPlayerPosition.y = SaveManager.CurrentSave.PlayerPositionY;
		NewPlayerPosition.z = SaveManager.CurrentSave.PlayerPositionZ;
		if (NewPlayerPosition == Vector3.zero)
		{
			NewPlayerPosition = GameObject.Find("PlayerStart").transform.position;
		}

		GameManager.instance.PlayerGameObject.transform.position = NewPlayerPosition;

		// Open done puzzles.
		if (SaveManager.CurrentSave.Puzzle1Done == true) {
			LevelToUnlock = 1;
			Invoke("DelayedLevelUnlock", 0.1F);
		}
		if (SaveManager.CurrentSave.Puzzle2Done == true) {
			LevelToUnlock = 2;
			Invoke("DelayedLevelUnlock", 0.1F);
		}
		if (SaveManager.CurrentSave.Puzzle3Done == true) {
			LevelToUnlock = 3;
			Invoke("DelayedLevelUnlock", 0.1F);
		}
		if (SaveManager.CurrentSave.Puzzle4Done == true) {
			LevelToUnlock = 4;
			Invoke("DelayedLevelUnlock", 0.1F);
		}
		if (SaveManager.CurrentSave.Puzzle5Done == true) {
			LevelToUnlock = 5;
			Invoke("DelayedLevelUnlock", 0.1F);
		}

		// GameStart, no mode selected.
        OnGameStart();
    }

	public void DelayedLevelUnlock()
	{
		GameManager.instance.GetShadowLevelScript(LevelToUnlock).UnlockPuzzle();
	}

    public void OnGameStart()
    {
        Debug.Log("GameController: GameStarting! No mode selected.");
        GameManager.instance.CameraController.BlackScreenTransition();
    }

    public void OnResetSaveOrdered()
    {
        Debug.Log("GameController: Reset save ordered!");
        GameManager.instance.CameraController.BlackScreenTransition();
        GameManager.instance.StartMenuScript.OnClickOptionsBack();
        Invoke("DelayedSaveReset", 2.5F);
    }

    public void DelayedSaveReset()
    {
        // When screen is black.
        // Replace player and camera;
        GameManager.instance.PlayerGameObject.transform.position = GameObject.Find("PlayerStart").transform.position;
        GameManager.instance.CameraController.ActiveCamera.transform.LookAt(GameObject.Find("PlayerStartLookPoint").transform);
        // Close puzzles.
        foreach (ShadowLevelObject ShadowLevel in GameManager.instance.SceneShadowLevels)
        {
            ShadowLevel.LockPuzzle();
        }
        SaveManager.CurrentSave = new SaveObject();
        GameManager.instance.CameraController.BlackScreenTransition();
    }

	public void OnNormalModeOrdered ()
	{
		Debug.Log("GameController: Switching to Normal mode!");
		GameManager.instance.TestMode = false;
		GameManager.instance.PlayerGameObject.GetComponent<FpsMovement> ().InTestMode = false;
		GameManager.instance.PlayerGameObject.layer = 8;
	}

    public void OnTestModeOrdered ()
    {
        Debug.Log("GameController: Switching to Test mode!");
		GameManager.instance.TestMode = true;
		GameManager.instance.PlayerGameObject.GetComponent<FpsMovement> ().InTestMode = true;
		GameManager.instance.PlayerGameObject.layer = 11;
		// Open all doors.
		foreach (ShadowLevelObject ShadowLevel in GameManager.instance.SceneShadowLevels)
		{
			ShadowLevel.UnlockPuzzle();
		}
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
		GameManager.instance.StartMenuScript.ExitPuzzlePlayAnimation (true);
		Invoke ("DelayGoingBackToFps", 1.8F);
	}

	public void DelayGoingBackToFps()
	{
		// Handle Save
		if (ShadowLevelSelected && GameManager.instance.TestMode == false)
		{
			GameManager.instance.SaveGameDatasFromWorld();
			GameManager.instance.RefreshSaveGMDisplay();
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
