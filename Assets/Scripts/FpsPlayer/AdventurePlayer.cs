using UnityEngine;
using System.Collections;

/// <summary>
/// Adventure player. Main controller class for the player.
/// </summary>
public class AdventurePlayer : MonoBehaviour {

	// Player gameplay Settings.
	[Header("Player Gameplay Settings")]
	public float				WalkingSpeed = 1.0F;
	public float				RunningSpeed = 2.0F;
	public float				CameraHeightOffset = 1.0F;

	// Control required references;
	public bool					IsControllable = true;
	public bool					CanInteractWithWorld = true;

	[Header("Player Current Interactions")]
	public bool					IsInPuzzleInteractZone = false;
    public bool                 IsInEndZone = false;
    public bool                 IsInEndTransition = false;
    public GameObject			CollidingPuzzle;

	private KeyCode				interactKey;
	private KeyCode				interactKeyAlt;

	[HideInInspector]
	public	CharacterController	PlayerCC; // public required;
	private FpsCameraControl	camControlScript;
	private FpsMovement			playerMovementScript;

	// Use this for initialization
	void Awake () {
		// set references to scripts;
		PlayerCC = GetComponent<CharacterController> ();
		camControlScript = GetComponent<FpsCameraControl> ();
		playerMovementScript = GetComponent<FpsMovement> ();
		interactKey = GameManager.instance.KeyManager.InteractKey;
		interactKeyAlt = GameManager.instance.KeyManager.InteractKeyAlt;
	}

	void CheckInputInteraction() 
	{
        if (IsControllable == true && (Input.GetKeyDown(interactKey) || Input.GetKeyDown(interactKeyAlt))) {
            Debug.Log("playerPress Interact Key");
            if (IsInPuzzleInteractZone && CollidingPuzzle && !IsInEndZone)
            {
                GameManager.instance.GameController.FpsToPuzzleMode();
            }

            if (IsInEndZone && !IsInEndTransition)
            {
                IsInEndTransition = true;
                GameManager.instance.GameController.OnEndReached();
            }
		}
	}

	// Update is called once per frame
	void Update () {
		CheckInputInteraction ();
	}
}
