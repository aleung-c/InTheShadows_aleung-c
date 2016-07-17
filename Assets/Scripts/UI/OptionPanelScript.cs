using UnityEngine;
using System.Collections;

public class OptionPanelScript : MonoBehaviour {
    private GameSettings        GS;

	// Use this for initialization
	void Start () {
        GS = GameManager.instance.GameSettings;
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
