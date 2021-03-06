﻿using UnityEngine;
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
	public bool					InScreenTransition;

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
			Invoke("DelayedLevel1Unlock", 0.1F);
		}
		if (SaveManager.CurrentSave.Puzzle2Done == true) {
			LevelToUnlock = 2;
			Invoke("DelayedLevel2Unlock", 0.1F);
		}
		if (SaveManager.CurrentSave.Puzzle3Done == true) {
			LevelToUnlock = 3;
			Invoke("DelayedLevel3Unlock", 0.1F);
		}
		if (SaveManager.CurrentSave.Puzzle4Done == true) {
			LevelToUnlock = 4;
			Invoke("DelayedLevel4Unlock", 0.1F);
		}
		if (SaveManager.CurrentSave.Puzzle5Done == true) {
			LevelToUnlock = 5;
			Invoke("DelayedLevel5Unlock", 0.1F);
		}

		// GameStart, no mode selected.
        OnGameStart();
    }

	public void DelayedLevel1Unlock()
	{
		GameManager.instance.GetShadowLevelScript(1).UnlockPuzzle();
	}

    public void DelayedLevel2Unlock()
    {
        GameManager.instance.GetShadowLevelScript(2).UnlockPuzzle();
    }

    public void DelayedLevel3Unlock()
    {
        GameManager.instance.GetShadowLevelScript(3).UnlockPuzzle();
    }

    public void DelayedLevel4Unlock()
    {
        GameManager.instance.GetShadowLevelScript(4).UnlockPuzzle();
    }

    public void DelayedLevel5Unlock()
    {
        GameManager.instance.GetShadowLevelScript(5).UnlockPuzzle();
    }

    public void OnGameStart()
    {
        Debug.Log("GameController: GameStarting! No mode selected.");
        GameManager.instance.CameraController.BlackScreenTransition();
		InScreenTransition = false;
    }

    public void OnResetSaveOrdered()
    {
		if (InScreenTransition == false)
		{
			Debug.Log ("GameController: Reset save ordered!");
			GameManager.instance.CameraController.BlackScreenTransition ();
			InScreenTransition = true;
			GameManager.instance.StartMenuScript.OnClickOptionsBack ();
			Invoke ("DelayedSaveReset", 3.0F);
		}
	}

    public void DelayedSaveReset()
    {
        // When screen is black.
        // Replace player and camera;
		if (GameManager.instance.TestMode == true) {
			OnNormalModeOrdered();
		}
        GameManager.instance.PlayerGameObject.transform.position = GameObject.Find("PlayerStart").transform.position;
        GameManager.instance.CameraController.ActiveCamera.transform.LookAt(GameObject.Find("PlayerStartLookPoint").transform);
        // Close puzzles.
        foreach (ShadowLevelObject ShadowLevel in GameManager.instance.SceneShadowLevels)
        {
            ShadowLevel.LockPuzzle();
        }
        SaveManager.CurrentSave = new SaveObject();
        GameManager.instance.CameraController.BlackScreenTransition();
        GameManager.instance.SaveGameDatasFromWorld();
		Invoke ("TimerToOutOfScreenTransition", 3.0F);
    }

	// to refuse another reset save during the transition;
	public void TimerToOutOfScreenTransition()
	{
		InScreenTransition = false;
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
		InScreenTransition = false;
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

    // On End Reached
    public void OnEndReached()
    {
        Debug.Log("GameController: end reached! Reseting save...");
        GameManager.instance.CameraController.WhiteScreenTransition();
        // block player input;
        GameManager.instance.PlayerGameObject.GetComponent<FpsCameraControl>().enabled = false;
        GameManager.instance.PlayerGameObject.GetComponent<FpsMovement>().enabled = false;
        // transitionning to end and reset;
		InScreenTransition = true;
        Invoke("DelayedSaveResetEndGame", 2.5F);
    }

    public void DelayedSaveResetEndGame()
    {
        // When screen is white.
        // Replace player and camera;
        GameManager.instance.PlayerGameObject.transform.position = GameObject.Find("PlayerStart").transform.position;
        GameManager.instance.CameraController.ActiveCamera.transform.LookAt(GameObject.Find("PlayerStartLookPoint").transform);
        // Close puzzles.
        foreach (ShadowLevelObject ShadowLevel in GameManager.instance.SceneShadowLevels)
        {
            ShadowLevel.LockPuzzle();
        }
        SaveManager.CurrentSave = new SaveObject();
        GameManager.instance.CameraController.WhiteScreenTransition();
        GameManager.instance.StartMenuScript.OpenMainMenu();
        GameManager.instance.SaveGameDatasFromWorld();
        // reactive player
        GameManager.instance.PlayerGameObject.GetComponent<FpsCameraControl>().enabled = true;
        GameManager.instance.PlayerGameObject.GetComponent<FpsMovement>().enabled = true;
        GameManager.instance.PlayerGameObject.GetComponent<AdventurePlayer>().IsInEndTransition = false;
		InScreenTransition = false;
    }

    // Update is called once per frame
    void Update () {
	
	}
}
