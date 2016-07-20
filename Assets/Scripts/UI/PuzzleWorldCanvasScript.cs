using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PuzzleWorldCanvasScript : MonoBehaviour {
	private ShadowLevelObject	shadowLevel;
	private Text				titleText;

	// Use this for initialization
	void Start () {
		shadowLevel = transform.parent.GetComponent<ShadowLevelObject> ();
		titleText = transform.Find ("MainPanel").transform.Find ("TitlePanel").transform.Find ("Title").GetComponent<Text> ();
		titleText.text = shadowLevel.PuzzleName;
	}

}
