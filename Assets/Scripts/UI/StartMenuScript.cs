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
	public bool			InPuzzleMenu = false;

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
	    if (OutOfMenu == true && InPuzzleMenu == false && Input.GetKeyDown(KeyCode.Escape))
        {
            OutOfMenu = false;
			GameManager.instance.GameController.InMenu = true;
            MainMenuPanel.SetActive(true);
            MainPanelAnimator.SetTrigger("AppearAll");
            GameManager.instance.KeyManager.MouseSensitivityX /= 2.0F;
            GameManager.instance.KeyManager.MouseSensitivityY /= 2.0F;
        }

		if (InPuzzleMenu == true && Input.GetKeyDown (KeyCode.Escape))
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
        }
    }

    public void OnClickOptions() {
        if (CanInteract) {
            CanInteract = false;
            MainPanelAnimator.SetTrigger("HideAll");
            //OptionPanelAnimator.SetTrigger("AppearAll");
            OptionPanel.SetActive(true);
        }
    }

    public void OnClickQuit() {
#if UNITY_EDITOR
        Debug.Log("Quit pressed");
#else
         Application.Quit();
#endif
    }

    // METHODS FOR THE OPTION MENU SCREEN --------------------------------//
    public void OnClickOptionsBack() {
        if (CanInteract) {
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

	public void ExitPuzzleMenu()
	{
		if (OutOfMenu == true)
		{
			PuzzlePanel.GetComponent<PuzzleMenuPanelScript> ().CurrentLevel = null;
			PuzzlePanel.SetActive(false);
		}
	}

}