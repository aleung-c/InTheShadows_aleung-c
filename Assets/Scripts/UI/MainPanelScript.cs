using UnityEngine;
using System.Collections;

public class MainPanelScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void CanInteractWithMenus() {
        transform.parent.GetComponent<StartMenuScript>().CanInteract = true;
    }

    public void CannotInteractWithMenus() {
        transform.parent.GetComponent<StartMenuScript>().CanInteract = false;
    }
}
