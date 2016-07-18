using UnityEngine;
using System.Collections;

public class PuzzleMenuPanelScript : MonoBehaviour {
	public ShadowLevelObject	CurrentLevel;
	public GameObject			FormStatusPrefab;
	public GameObject			FormStatusUIContainer;
	public GameObject			CurrentSpawnedObj;

	// others.
	private int					i;
    private Vector3             defaultLocalScale;

	void Awake()
	{
		FormStatusUIContainer = transform.Find ("MainPanel").transform.Find ("LowerMiddlePanel").transform.gameObject;
        defaultLocalScale.x = 1.0F;
        defaultLocalScale.y = 1.0F;
        defaultLocalScale.z = 1.0F;
    }

	// Use this for initialization
	void OnEnable ()
	{
		if (CurrentLevel)
		{
			for (i = 0; i < CurrentLevel.FormCount; i++)
			{
				CurrentSpawnedObj = (GameObject)Instantiate (FormStatusPrefab, Vector3.zero, Quaternion.identity);
				CurrentSpawnedObj.GetComponent<UIFormStatus> ().CorrespondingObject = CurrentLevel.FormContainer.transform.GetChild(i).gameObject;
				Debug.Log(CurrentLevel.FormContainer.transform.GetChild(i).gameObject.name.ToString());
				CurrentSpawnedObj.transform.SetParent(FormStatusUIContainer.transform);
                CurrentSpawnedObj.transform.localScale = defaultLocalScale;
            }
		}
	}

	public void OnClickBack()
	{
		if (GameManager.instance.GameController.InPuzzleMode == true)
		{
			OnLeavePuzzle ();
			GameManager.instance.GameController.PuzzleToFpsMode ();
			GameManager.instance.StartMenuScript.ExitPuzzlePlayAnimation(false);
		}
	}

	void OnLeavePuzzle()
	{
		foreach(Transform child in FormStatusUIContainer.transform)
		{
			child.GetComponent<UIFormStatus> ().enabled = false;
			Destroy(child.gameObject);
		}
	}

	public void DeactivatePuzzlePanel()
	{
		this.gameObject.SetActive(false);
	}

	// Update is called once per frame
	void Update ()
	{
	
	}
}
