using UnityEngine;
using UnityEngine.Events;
using System.Collections;

/// <summary>
/// Game controller. Handles every mode change.
/// </summary>
public class GameController : MonoBehaviour {
	public GameObject			MainPlayer;
	public bool					CanEnterInput = true;

	public bool					InMenu = true;
	public bool					InFpsMode = true;
	public bool					InPuzzleMode = false;
	public ShadowLevelObject	ShadowLevelSelected;

	// Use this for initialization
	void Start () {
		MainPlayer = GameManager.instance.PlayerGameObject;
		GameManager.instance.CameraController.OnEndTransition.AddListener (OnEndTransition);
	}

	// From fps to puzzle game mode
	public void	FpsToPuzzleMode()
	{
		if (InMenu == false && InPuzzleMode == false)
		{
			CanEnterInput = false;
			MainPlayer.GetComponent<AdventurePlayer> ().IsControllable = false;
			MainPlayer.GetComponent<FpsCameraControl> ().enabled = false;
			MainPlayer.GetComponent<FpsMovement> ().enabled = false;
			InFpsMode = false;
			InPuzzleMode = true;
			ShadowLevelSelected = MainPlayer.GetComponent<AdventurePlayer> ()
			.CollidingPuzzle.GetComponent<ShadowLevelObject> ();
			GameManager.instance.MenuGameObject.GetComponent<StartMenuScript>().InPuzzleMenu = true;
			GameManager.instance.CameraController.ChangeCamPlayerToPuzzle (ShadowLevelSelected.PuzzleCamera);
		}
	}

	public void OnEndTransition()
	{
		Debug.Log ("endtransition");
		if (InPuzzleMode == true) { // == transitionning from fps to puzzle;
			CanEnterInput = true;
			ShadowLevelSelected.Playable = true;
			ShadowLevelSelected.StartPlaying ();
		} else if (InPuzzleMode == false) { // == transitionning from puzzle to fps;
			CanEnterInput = true;
			ShadowLevelSelected.Playable = false;
			MainPlayer.GetComponent<AdventurePlayer> ().IsControllable = true;
			MainPlayer.GetComponent<FpsCameraControl> ().enabled = true;
			MainPlayer.GetComponent<FpsMovement> ().enabled = true;
			InFpsMode = true;
			GameManager.instance.MenuGameObject.GetComponent<StartMenuScript>().InPuzzleMenu = false;
		}
	}

	// From puzzle to fps game mode
	public void	PuzzleToFpsMode()
	{
		if (InPuzzleMode == true)
		{
			CanEnterInput = false;
			InPuzzleMode = false;
			GameManager.instance.CameraController.ChangeCamPuzzleToPlayer ();
		}

	}

	// Update is called once per frame
	void Update () {
	
	}
}
