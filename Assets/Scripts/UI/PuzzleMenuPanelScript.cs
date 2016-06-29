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
				CurrentSpawnedObj.transform.SetParent(FormStatusUIContainer.transform);
                CurrentSpawnedObj.transform.localScale = defaultLocalScale;

            }
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
