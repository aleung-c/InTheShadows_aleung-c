using UnityEngine;
using System.Collections;

public class ShadowLevelLight : MonoBehaviour {
	private ShadowLevelObject	ShadowLevel;
	private GameObject			levelLight;
	private Color				EndColor; // color of level light when puzzleDone;
	public bool					LightChanged;

	// Use this for initialization
	void Start () {
		ShadowLevel = transform.parent.GetComponent<ShadowLevelObject> ();
		ShadowLevel.OnPuzzleDone.AddListener (OnShadowLevelCompleted);
		ShadowLevel.OnPuzzleUnlock.AddListener (OnShadowLevelUnlock);
		levelLight = transform.Find ("LevelStatusLight").gameObject;
		ColorUtility.TryParseHtmlString("#0287C3", out EndColor);
	}

	void OnShadowLevelCompleted()
	{
		ChangeLightColor ();
	}

	void OnShadowLevelUnlock()
	{
		ChangeLightColor ();
	}

	void ChangeLightColor()
	{
		if (!LightChanged)
		{
			LightChanged = true;
			levelLight.GetComponent<Light>().color = EndColor;
		}
	}

	void ResetLightColor()
	{
		if (LightChanged)
		{
			LightChanged = false;
			levelLight.GetComponent<Light>().color = Color.white;
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
