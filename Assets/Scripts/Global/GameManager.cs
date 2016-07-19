﻿using UnityEngine;
using System.Collections;

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
        SaveManager.Load();
        SaveObject.Current = SaveManager.CurrentSave;
        // GameController will take care of level loading.
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

    public void SaveGameFile()
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
