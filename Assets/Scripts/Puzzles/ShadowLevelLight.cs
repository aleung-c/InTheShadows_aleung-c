using UnityEngine;
using System.Collections;

public class ShadowLevelLight : MonoBehaviour {
	private ShadowLevelObject	shadowLevel;
	private ShadowLevelObject	previousShadowLevel;
	private ShadowLevelObject	nextShadowLevel;
	private Color				endColor; // color of level light when puzzleDone;
	private string				blue = "#0287C3";
	private string				orange = "#FF6600";
	private Light[]				levelLights;

	// Use this for initialization
	void Start ()
	{
		shadowLevel = transform.parent.GetComponent<ShadowLevelObject> ();
		shadowLevel.OnPuzzleDone.AddListener (OnShadowLevelCompleted);
		shadowLevel.OnPuzzleUnlock.AddListener (OnShadowLevelUnlock);
		levelLights = transform.GetComponentsInChildren<Light> ();

		// Set starting light color;
		previousShadowLevel = GameManager.instance.GetShadowLevelScript ((shadowLevel.PuzzleNumber) - 1);
		nextShadowLevel = GameManager.instance.GetShadowLevelScript ((shadowLevel.PuzzleNumber) + 1);
		if (previousShadowLevel)
		{
			ChangeLightsColorToOrange();
		}
	}

	void OnDisable()
	{
		shadowLevel.OnPuzzleDone.RemoveListener (OnShadowLevelCompleted);
		shadowLevel.OnPuzzleUnlock.RemoveListener (OnShadowLevelUnlock);
	}

	/// <summary>
	/// Respond to the OnPuzzleDone event.
	/// </summary>
	void OnShadowLevelCompleted()
	{
		ChangeLightsColorToBlue ();
		if (nextShadowLevel)
		{
			nextShadowLevel.LightScript.ChangeLightsColorToWhite();
		}
	}

	/// <summary>
	/// Respond to the OnPuzzleUnlock event.
	/// </summary>
	void OnShadowLevelUnlock()
	{
		ChangeLightsColorToBlue ();
	}
	
	void ChangeLightsColorToBlue()
	{
		ColorUtility.TryParseHtmlString(blue, out endColor);
		foreach (Light light in levelLights)
		{
			light.color = endColor;
		}
	}

	void ChangeLightsColorToOrange()
	{
		ColorUtility.TryParseHtmlString(orange, out endColor);
		foreach (Light light in levelLights)
		{
			light.color = endColor;
		}
	}
	
	void ChangeLightsColorToWhite()
	{
		foreach (Light light in levelLights)
		{
			light.color = Color.white;
		}
	}

	void ResetLightsColor()
	{
		foreach (Light light in levelLights)
		{
			light.color = Color.white;
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
