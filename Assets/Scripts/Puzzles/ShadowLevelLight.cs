using UnityEngine;
using System.Collections;

public class ShadowLevelLight : MonoBehaviour {
	private ShadowLevelObject	ShadowLevel;
	private ShadowLevelObject	PreviousShadowLevel;
	private ShadowLevelObject	NextShadowLevel;
	private GameObject			levelLight;
	private Color				EndColor; // color of level light when puzzleDone;
	private string				Blue = "#0287C3";
	private string				Orange = "#FF6600";
	public bool					LightChanged;

	// Use this for initialization
	void Start () {
		ShadowLevel = transform.parent.GetComponent<ShadowLevelObject> ();
		ShadowLevel.OnPuzzleDone.AddListener (OnShadowLevelCompleted);
		ShadowLevel.OnPuzzleUnlock.AddListener (OnShadowLevelUnlock);
		levelLight = transform.Find ("LevelStatusLight").gameObject;
		ColorUtility.TryParseHtmlString(Blue, out EndColor);

		// set starting light color;

		PreviousShadowLevel = GameManager.instance.GetShadowLevelScript ((ShadowLevel.PuzzleNumber) - 1);
		NextShadowLevel = GameManager.instance.GetShadowLevelScript ((ShadowLevel.PuzzleNumber) + 1);
		if (PreviousShadowLevel)
		{
			ChangeLightColorToOrange();
		}
	}

	/// <summary>
	/// Respond to the OnPuzzleDone event.
	/// </summary>
	void OnShadowLevelCompleted()
	{
		ChangeLightColorToBlue ();
		if (NextShadowLevel)
		{
			NextShadowLevel.LightScript.ChangeLightColorToWhite();
		}
	}

	/// <summary>
	/// Respond to the OnPuzzleUnlock event.
	/// </summary>
	void OnShadowLevelUnlock()
	{
		ChangeLightColorToBlue ();
	}

	/// <summary>
	/// Changes the light color to blue.
	/// </summary>
	void ChangeLightColorToBlue()
	{
		ColorUtility.TryParseHtmlString(Blue, out EndColor);
		levelLight.GetComponent<Light>().color = EndColor;
	}

	/// <summary>
	/// Changes the light color to orange.
	/// </summary>
	void ChangeLightColorToOrange()
	{
		ColorUtility.TryParseHtmlString(Orange, out EndColor);
		levelLight.GetComponent<Light>().color = EndColor;
	}

	/// <summary>
	/// Changes the light color to white.
	/// </summary>
	void ChangeLightColorToWhite()
	{
		levelLight.GetComponent<Light>().color = Color.white;
	}

	/// <summary>
	/// Resets the color of the light.
	/// </summary>
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
