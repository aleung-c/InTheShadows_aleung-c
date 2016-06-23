using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public static GameManager       instance = null;
    public GameSettings             GameSettings;
	public KeyControlManager		KeyManager;
    public GameObject               PlayerGameObject;

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
        // load save ... etc
        GameSettings = GetComponent<GameSettings>();
        KeyManager = GetComponent<KeyControlManager> ();
        PlayerGameObject = GameObject.Find("Player");
    }	
	
	//Update is called every frame.
	void Update()
	{
		
	}
}
