using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public static GameManager instance = null;
	public KeyControlManager		KeyManager;

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
		KeyManager = GetComponent<KeyControlManager> ();
	}	
	
	//Update is called every frame.
	void Update()
	{
		
	}
}
