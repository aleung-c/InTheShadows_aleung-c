using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StartMenuScript : MonoBehaviour {
    [SerializeField]
    private GameObject  MainMenuPanel;
    [SerializeField]
    private Animator    MainPanelAnimator;
    [SerializeField]
    private GameObject  OptionPanel;
    [SerializeField]
    private Animator  OptionPanelAnimator;

    public bool         CanInteract = true;
    public bool         OutOfMenu = false;

	// Use this for initialization
	void Start () {
        MainMenuPanel = transform.Find("MainPanelContainer").gameObject;
        MainPanelAnimator = MainMenuPanel.GetComponent<Animator>();
        OptionPanel = transform.Find("OptionMenuPanelContainer").gameObject;
        OptionPanelAnimator = OptionPanel.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
	    if (OutOfMenu == true && Input.GetKeyDown(KeyCode.Escape))
        {
            OutOfMenu = false;
            MainMenuPanel.SetActive(true);
            MainPanelAnimator.SetTrigger("AppearAll");
            GameManager.instance.KeyManager.MouseSensitivityX /= 2.0F;
            GameManager.instance.KeyManager.MouseSensitivityY /= 2.0F;
        }
	}

    // METHODS FOR THE MAIN MENU SCREEN --------------------------------//
    public void OnClickNormalMode() {
        if (CanInteract)
        {
            CanInteract = false;
            MainPanelAnimator.SetTrigger("HideAll");
            OutOfMenu = true;
            GameManager.instance.KeyManager.MouseSensitivityX *= 2.0F;
            GameManager.instance.KeyManager.MouseSensitivityY *= 2.0F;
        }
    }

    public void OnClickTestMode() {
        if (CanInteract) {
            CanInteract = false;
            MainPanelAnimator.SetTrigger("HideAll");
            OutOfMenu = true;
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
}
