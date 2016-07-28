using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OptionPanelScript : MonoBehaviour {
    private GameSettings        GS;
    //private GameObject          ResetBtn;

	// Use this for initialization
	void Start () {
        GS = GameManager.instance.GameSettings;
        //ResetBtn = transform.Find("ScreenPanel").transform.Find("ResetSaveBtn").gameObject;
    }
    
    public void OnClickSoundBtn() {
        if (transform.parent.GetComponent<StartMenuScript>().CanInteract)
            GS.WithSound = false;
    }

    public void OnClickResetSave()
    {
        GameManager.instance.GameController.OnResetSaveOrdered();
    }

    public void CanInteractWithMenus() {
        transform.parent.GetComponent<StartMenuScript>().CanInteract = true;
    }

    public void CannotInteractWithMenus() {
        transform.parent.GetComponent<StartMenuScript>().CanInteract = false;
    }

    public void OptionScreenExiting() {
        gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update () {
	
	}
}
