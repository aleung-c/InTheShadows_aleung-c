using UnityEngine;
using System.Collections;

public class PuzzleMenuPanelScript : MonoBehaviour {
	public ShadowLevelObject	CurrentLevel;
	public GameObject			FormStatusPrefab;
	public GameObject			FormStatusUIContainer;
	public GameObject			CurrentSpawnedObj;

	// others.
	private int					i;

	void Awake()
	{
		FormStatusUIContainer = transform.Find ("MainPanel").transform.Find ("LowerMiddlePanel").transform.gameObject;
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
				CurrentSpawnedObj.transform.SetParent(FormStatusUIContainer.transform);
			}
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
