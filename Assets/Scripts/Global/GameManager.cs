using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	[Header("Singleton instanciation")]
	public static GameManager       instance = null;

	[Header("References")]
    public GameSettings             GameSettings;
	public KeyControlManager		KeyManager;
	
	public GameObject				MenuGameObject;
	public StartMenuScript			StartMenuScript;
    public GameObject               PlayerGameObject;
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
		MenuGameObject = GameObject.Find ("Menu").gameObject;
		StartMenuScript = MenuGameObject.GetComponent<StartMenuScript> ();
		GameController = GameObject.Find ("GameController").GetComponent<GameController> ();
		CameraController = GameObject.Find ("CameraController").GetComponent<CameraController> ();
        GameSettings = GetComponent<GameSettings> ();
        KeyManager = GetComponent<KeyControlManager> ();
        PlayerGameObject = GameObject.Find("Player");
    }	
	
	//Update is called every frame.
	void Update()
	{
		
	}
}
