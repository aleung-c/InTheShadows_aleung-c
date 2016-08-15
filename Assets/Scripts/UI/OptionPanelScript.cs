using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OptionPanelScript : MonoBehaviour {
    private GameSettings        GS;
    //private GameObject          ResetBtn;
    private bool                WithSound;
    private Text                SoundOptionText1;
    private Text                SoundOptionText2;

    // Use this for initialization
    void Start () {
        GS = GameManager.instance.GameSettings;
        // Get Options Values text to set;
        SoundOptionText1 = transform.Find("ScreenPanel").transform.Find("SoundBtn").transform.Find("Text").GetComponent<Text>();
        SoundOptionText2 = transform.Find("ScreenPanel").transform.Find("SoundBtn").transform.Find("Text (1)").GetComponent<Text>();
        //ResetBtn = transform.Find("ScreenPanel").transform.Find("ResetSaveBtn").gameObject;
    }

    void OnEnable()
    {
        WithSound = GS.WithSound;
        // just setting the display of the text;
        if (WithSound)
        {
            SoundOptionText1.text = "Yes";
            SoundOptionText2.text = "Yes";
        }
        else
        {
            SoundOptionText1.text = "No";
            SoundOptionText2.text = "No";
        }
    }
    
    public void OnClickSoundBtn() {
        if (transform.parent.GetComponent<StartMenuScript>().CanInteract)
        {
            if (WithSound)
            {
                SoundOptionText1.text = "No";
                SoundOptionText2.text = "No";
                GameManager.instance.TurnOffSounds();
                WithSound = false;
                GS.WithSound = false;
            }
            else
            {
                SoundOptionText1.text = "Yes";
                SoundOptionText2.text = "Yes";
                GameManager.instance.TurnOnSounds();
                WithSound = true;
                GS.WithSound = true;
            }
        }
            
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
