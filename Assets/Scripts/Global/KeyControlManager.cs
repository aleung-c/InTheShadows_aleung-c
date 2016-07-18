using UnityEngine;
using System.Collections;

public class KeyControlManager : MonoBehaviour {
	public float		MouseSensitivityX = 1.0F;
	public float		MouseSensitivityY = 1.0F;

    [Header("Fps Part")]
	public KeyCode		MoveForward;
	public KeyCode		MoveForwardAlt;

	public KeyCode		MoveBackward;
	public KeyCode		MoveBackwardAlt;

	public KeyCode		StrafeLeft;
	public KeyCode		StrafeLeftAlt;

	public KeyCode		StrafeRight;
	public KeyCode		StrafeRightAlt;

	public KeyCode		AltitudeUp;
	public KeyCode		AltitudeUpAlt;
	
	public KeyCode		AltitudeDown;
	public KeyCode		AltitudeDownAlt;

    public KeyCode      EscapeKey;
    public KeyCode      EscapeKeyAlt;

    public KeyCode      InteractKey;
    public KeyCode      InteractKeyAlt;

    [Header("Puzzle Part")]
    public KeyCode      VerticalPuzzleButton;
    public KeyCode      VerticalPuzzleButtonAlt;

    public KeyCode      DisplacementPuzzleButton;
    public KeyCode      DisplacementPuzzleButtonAlt;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
