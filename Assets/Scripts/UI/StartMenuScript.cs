using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Start menu script. Controller for Menu states.
/// </summary>
public class StartMenuScript : MonoBehaviour {
	[Header("Main Menu references")]
    [SerializeField]
    private GameObject  MainMenuPanel;
    [SerializeField]
    private Animator    MainPanelAnimator;
	[Header("Options Menu references")]
    [SerializeField]
    private GameObject  OptionPanel;
    [SerializeField]
    private Animator  	OptionPanelAnimator;
	[Header("Puzzle Menu references")]
	[SerializeField]
	private GameObject  PuzzlePanel;
	[SerializeField]
	private Animator    PuzzlePanelAnimator;

	[Header("Load Game Menu references")]
	[SerializeField]
	private GameObject  LoadGamePanel;
	[SerializeField]
	private Animator    LoadGamePanelAnimator;

    public bool         CanInteract = true;
    public bool         OutOfMenu = false;
	public bool			InOtherMenu = false;
	public bool			InPuzzleMenu = false;

	[HideInInspector]
	public bool			PuzzleResolved = false;

	// Use this for initialization
	void Start () {
        MainMenuPanel = transform.Find("MainPanelContainer").gameObject;
        MainPanelAnimator = MainMenuPanel.GetComponent<Animator>();

        OptionPanel = transform.Find("OptionMenuPanelContainer").gameObject;
        OptionPanelAnimator = OptionPanel.GetComponent<Animator>();

		PuzzlePanel = transform.Find("PuzzleMenuPanel").gameObject;
		PuzzlePanelAnimator = PuzzlePanel.GetComponent<Animator>();

		LoadGamePanel = transform.Find ("LoadGamePanel").gameObject;
		LoadGamePanelAnimator = LoadGamePanel.GetComponent<Animator> ();

		// Set all as it should be
		MainMenuPanel.SetActive (true);
		OptionPanel.SetActive (false);
		PuzzlePanel.SetActive (false);
		LoadGamePanel.SetActive (false);
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape) && OutOfMenu == true && InPuzzleMenu == false && InOtherMenu == false) // not in any menu
		{
			OutOfMenu = false;
			GameManager.instance.GameController.InMenu = true;
			MainMenuPanel.SetActive (true);
			MainPanelAnimator.SetTrigger ("AppearAll");
			GameManager.instance.KeyManager.MouseSensitivityX /= 2.0F;
			GameManager.instance.KeyManager.MouseSensitivityY /= 2.0F;
		}
		else if (Input.GetKeyDown (KeyCode.Escape) && OutOfMenu == false && InPuzzleMenu == false && InOtherMenu == false) // in main menu
		{
			if (GameManager.instance.TestMode == false)
			{
				OnClickNormalMode();
			}
			else
			{
				OnClickTestMode();
			}
		}

		if (Input.GetKeyDown(KeyCode.Escape) && InPuzzleMenu == true && InOtherMenu == false)
		{
			GameManager.instance.GameController.PuzzleToFpsMode();
		}
	}

    // METHODS FOR THE MAIN MENU SCREEN --------------------------------//
    public void OnClickNormalMode() {
        if (CanInteract)
        {
            CanInteract = false;
            MainPanelAnimator.SetTrigger("HideAll");
            OutOfMenu = true;
			GameManager.instance.GameController.InMenu = false;
            GameManager.instance.KeyManager.MouseSensitivityX *= 2.0F;
            GameManager.instance.KeyManager.MouseSensitivityY *= 2.0F;
			// Call to Game Controller
			GameManager.instance.GameController.OnNormalModeOrdered();
        }
    }

    public void OnClickTestMode() {
        if (CanInteract) {
            CanInteract = false;
            MainPanelAnimator.SetTrigger("HideAll");
            OutOfMenu = true;
			GameManager.instance.GameController.InMenu = false;
            GameManager.instance.KeyManager.MouseSensitivityX *= 2.0F;
            GameManager.instance.KeyManager.MouseSensitivityY *= 2.0F;
            // Call to Game Controller
            GameManager.instance.GameController.OnTestModeOrdered();
        }
    }

    public void OnClickQuit() {
		if (OutOfMenu == false && InPuzzleMenu == false) // in main menu
		{
#if UNITY_EDITOR
			Debug.Log ("Quit pressed");
#else
         Application.Quit();
#endif
		}
    }

    // METHODS FOR THE OPTION MENU SCREEN --------------------------------//

	public void OnClickOptions() {
		if (CanInteract) {
			CanInteract = false;
			InOtherMenu = true;
			MainPanelAnimator.SetTrigger("HideAll");
			//OptionPanelAnimator.SetTrigger("AppearAll");
			OptionPanel.SetActive(true);
		}
	}

    public void OnClickOptionsBack() {
        if (CanInteract) {
			InOtherMenu = false;
            CanInteract = false;
            OptionPanelAnimator.SetTrigger("DisappearAll");
            MainPanelAnimator.SetTrigger("AppearAll");
            OptionPanel.SetActive(true);
        }
    }

	// PUZZLE MENU CALLS AND METHODS

	public void StartPuzzleMenu(ShadowLevelObject SelectedLevel)
	{
		if (OutOfMenu == true)
		{
			PuzzlePanel.GetComponent<PuzzleMenuPanelScript> ().CurrentLevel = SelectedLevel;
			PuzzlePanel.SetActive(true);
		}
	}

	public void ExitPuzzlePlayAnimation(bool Win)
	{
		if (Win == true)
			PuzzlePanel.GetComponent<Animator> ().SetTrigger("DisappearWin");
		else
			PuzzlePanel.GetComponent<Animator> ().SetTrigger("Disappear");
	}

	public void ExitPuzzleMenu()
	{
		if (OutOfMenu == true)
		{
			PuzzlePanel.GetComponent<PuzzleMenuPanelScript> ().CurrentLevel = null;
		}
	}
}