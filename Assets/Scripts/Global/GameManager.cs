﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
	[Header("Singleton instanciation")]
	public static GameManager       instance = null;

	[Header("References")]
    public GameObject               PlayerGameObject;

    public GameSettings             GameSettings;
	public KeyControlManager		KeyManager;
	
	public GameObject				MenuGameObject;
	public StartMenuScript			StartMenuScript;

	public bool						TestMode = false;

	[Header("References to level stuffs")]
	public ShadowLevelObject[]		SceneShadowLevels;
	public ShadowLevelDoor[]		SceneDoors;

    [Header("Controller References")]
	// GameControllers
	public GameController			GameController; // controls game states and mode switchings;
	public CameraController			CameraController;

	[Header("SaveStatus")]
	public List<bool>				SaveOfPuzzleDones = new List<bool>();

	//Awake is always called before any Start functions
	void Awake()
	{
		if (instance == null) {
			instance = this;
		}
		else if (instance != this) {
			Destroy (gameObject);    
		}
		DontDestroyOnLoad(gameObject);
		InitGame ();
	}
	
	//Initializes the game for each level.
	void InitGame()
	{
        // Set managers references variables
		MenuGameObject = GameObject.Find ("Menu").gameObject;
		StartMenuScript = MenuGameObject.GetComponent<StartMenuScript> ();
		GameController = GameObject.Find ("GameController").GetComponent<GameController> ();
		CameraController = GameObject.Find ("CameraController").GetComponent<CameraController> ();
        GameSettings = GetComponent<GameSettings> ();
        KeyManager = GetComponent<KeyControlManager> ();
       
		// Set game needed elements references
		PlayerGameObject = GameObject.Find("Player");
		SceneShadowLevels = GameObject.FindObjectsOfType<ShadowLevelObject> ();
		SceneDoors = GameObject.FindObjectsOfType<ShadowLevelDoor> ();

        // SaveManager also available as static Class SaveManager.
		print(Application.persistentDataPath);
        SaveManager.LoadGameFile();


		SaveOfPuzzleDones.Add (false);
		SaveOfPuzzleDones.Add (false);
		SaveOfPuzzleDones.Add (false);
		SaveOfPuzzleDones.Add (false);
		SaveOfPuzzleDones.Add (false);
		RefreshSaveGMDisplay ();
        // GameController will take care of level loading.
    }

	// Manual save for the game.
	public void SaveGameDatasFromWorld()
	{
		Debug.Log ("GameManager: Game saved from world state");
		// Adventure datas.
		SaveManager.CurrentSave.PlayerPositionX = GameManager.instance.PlayerGameObject.transform.position.x;
		SaveManager.CurrentSave.PlayerPositionY = GameManager.instance.PlayerGameObject.transform.position.y;
		SaveManager.CurrentSave.PlayerPositionZ = GameManager.instance.PlayerGameObject.transform.position.z;

		// Puzzle datas.
		SaveManager.CurrentSave.Puzzle1Done = GetShadowLevelScript(1).PuzzleDone;
		SaveManager.CurrentSave.Puzzle2Done = GetShadowLevelScript(2).PuzzleDone;
		SaveManager.CurrentSave.Puzzle3Done = GetShadowLevelScript(3).PuzzleDone;
		SaveManager.CurrentSave.Puzzle4Done = GetShadowLevelScript(4).PuzzleDone;
		SaveManager.CurrentSave.Puzzle5Done = GetShadowLevelScript(5).PuzzleDone;

		// Do the actual save action.
		SaveManager.SaveGameFile ();
	}

	public void RefreshSaveGMDisplay()
	{
		SaveOfPuzzleDones [0] = SaveManager.CurrentSave.Puzzle1Done;
		SaveOfPuzzleDones [1] = SaveManager.CurrentSave.Puzzle2Done;
		SaveOfPuzzleDones [2] = SaveManager.CurrentSave.Puzzle3Done;
		SaveOfPuzzleDones [3] = SaveManager.CurrentSave.Puzzle4Done;
		SaveOfPuzzleDones [4] = SaveManager.CurrentSave.Puzzle5Done;
	}

	public ShadowLevelObject GetShadowLevelScript(int LevelNumber)
	{
		foreach (ShadowLevelObject Level in SceneShadowLevels)
		{
			if (Level.PuzzleNumber == LevelNumber)
			{
				return (Level);
			}
		}
		return (null);
	}

	// unused for now ?
    public void LoadSaveFile()
    {

    }



    public void ResetSaveFile()
    {

    }

    //Update is called every frame.
    void Update()
	{
		
	}
}
