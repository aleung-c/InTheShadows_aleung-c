using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class ShadowGameWinCheck : MonoBehaviour {
    public UnityEvent       AllFormOkay;
    public GameObject       FormContainer;

    void OnStart()
    {
        AllFormOkay = new UnityEvent();
    }

	// Use this for initialization
	void OnEnable () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
