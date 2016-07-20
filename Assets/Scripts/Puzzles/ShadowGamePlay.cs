using UnityEngine;
using UnityEngine.Events;
using System.Collections;

// Class proto for Event with params.
public class ObjectClickingEvent : UnityEvent<GameObject>{}

/// <summary>
/// Shadow game play. Works with event sending clicked form;
/// </summary>
public class ShadowGamePlay : MonoBehaviour {
	// ---------------------------------------- //
    public GameObject   		FormContainer;
	// ---------------------------------------- //
    [Header("In game private visible")]
    [SerializeField]
    public bool        			Clicking;

    [SerializeField]
    private bool        		pressingVerticalMode;
    [SerializeField]
    private bool        		pressingDisplacementMode;

    private Quaternion     		newFormRotation;
	private Vector3				NewFormRotationEuleur;

    private Vector3     		newFormPosition;

    // keys
    private KeyCode      		verticalModeKey;
    private KeyCode      		verticalModeKeyAlt;

    private KeyCode      		displacementModeKey;
    private KeyCode      		displacementModeKeyAlt;

	private KeyCode				resetPuzzleKey;
	private KeyCode				resetPuzzleKeyAlt;

    // Settings
    private float                RotationSpeed;
	private float                DisplacementSpeed;
    
	//private bool                 HasHorizontalRotation;
    //private bool                 HasVerticalRotation;
    //private bool                 HasOffsetDisplacement;
    
	//private bool                 IsMultiObjects;

    [SerializeField]
    private GameObject			CurrentForm;
	private ShadowObject		CurrentFormScript;
	private float				MouseMovementStockX;
	private float 				MouseMovementStockY;

    void Awake()
    {
        FormContainer = transform.Find("FormContainer").gameObject;
        verticalModeKey = GameManager.instance.KeyManager.VerticalPuzzleButton;
        verticalModeKeyAlt = GameManager.instance.KeyManager.VerticalPuzzleButtonAlt;

        displacementModeKey = GameManager.instance.KeyManager.DisplacementPuzzleButton;
        displacementModeKeyAlt = GameManager.instance.KeyManager.DisplacementPuzzleButtonAlt;

		resetPuzzleKey = GameManager.instance.KeyManager.ResetPuzzleKey;
		resetPuzzleKeyAlt = GameManager.instance.KeyManager.ResetPuzzleKeyAlt;
    }

    // Use this for initialization
    void OnEnable()
    {
        // Get PuzzleSettings
        RotationSpeed = GetComponent<ShadowLevelObject>().RotationSpeed;
		DisplacementSpeed = GetComponent<ShadowLevelObject> ().DisplacementSpeed;
        foreach (Transform Child in FormContainer.transform)
		{
            Child.GetComponent<ShadowObject>().ClickCatcher.OnClickDown.AddListener(OnFormMouseDown);
            Child.GetComponent<ShadowObject>().ClickCatcher.OnClickUp.AddListener(OnFormMouseUp);
        }
    }

    // Avoiding memory leaks
    void OnDisable()
    {
        foreach (Transform Child in FormContainer.transform)
		{
            Child.GetComponent<ShadowObject>().ClickCatcher.OnClickDown.RemoveListener(OnFormMouseDown);
            Child.GetComponent<ShadowObject>().ClickCatcher.OnClickUp.RemoveListener(OnFormMouseUp);
        }
    }

	/// <summary>
	///  Main function for form movement.
	/// </summary>
    void MoveForm()
    {
        if (CurrentForm)
        {
			CurrentFormScript = CurrentForm.GetComponent<ShadowObject>();
			newFormPosition = CurrentFormScript.ObjOffset.transform.localPosition;
            // Priority order : displacement > VerticalMode > nothing pressed (horizontal)

            // Displacement //
			if (CurrentFormScript.HasOffsetDisplacement && pressingDisplacementMode)
            {
				newFormPosition.y += Mathf.Clamp(Input.GetAxis("MouseVertical")
	                             	* DisplacementSpeed * Time.deltaTime, -1.0F, 1.0F);
				newFormPosition.x += Mathf.Clamp(Input.GetAxis("MouseHorizontal")
                                 	* DisplacementSpeed * Time.deltaTime, -1.0F, 1.0F);
				CurrentFormScript.ObjOffset.transform.localPosition = newFormPosition;

			}
            // Vertical //
			else if (CurrentFormScript.HasVerticalRotation && pressingVerticalMode)
            {
				// Using relative rotationning.
				CurrentFormScript.ObjRotation.transform.rotation =
					CurrentFormScript.ObjRotation.transform.rotation 
					* Quaternion.Euler(Input.GetAxis("MouseVertical"), 0, 0);
            }
            // Horizontal //
			else if (CurrentFormScript.HasHorizontalRotation && !pressingVerticalMode)
            {
				// Using world related up vector. (that's the reason for the euler angle conversion).
				NewFormRotationEuleur = CurrentFormScript.ObjRotation.transform.eulerAngles;
				NewFormRotationEuleur.y -= Input.GetAxis("MouseHorizontal");
				newFormRotation = Quaternion.Euler(NewFormRotationEuleur);
				// Rotation horizontal
				CurrentFormScript.ObjRotation.transform.rotation =
					Quaternion.RotateTowards(CurrentFormScript.ObjRotation.transform.rotation,
					                         newFormRotation, 45.0F);

				// Using relative rotationning. Unused, causes confusion when moving both axes.
					/*CurrentFormScript.ObjRotation.transform.rotation =
					CurrentFormScript.ObjRotation.transform.rotation 
						* Quaternion.Euler(0, Input.GetAxis("MouseHorizontal"), 0);*/
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // key input checking
        if (Input.GetKeyDown(verticalModeKey) || Input.GetKeyDown(verticalModeKeyAlt))
            pressingVerticalMode = true;
        if (Input.GetKeyUp(verticalModeKey) || Input.GetKeyUp(verticalModeKeyAlt))
            pressingVerticalMode = false;

        if (Input.GetKeyDown(displacementModeKey) || Input.GetKeyDown(displacementModeKeyAlt))
            pressingDisplacementMode = true;
        if (Input.GetKeyUp(displacementModeKey) || Input.GetKeyUp(displacementModeKeyAlt))
            pressingDisplacementMode = false;

		if ((Input.GetKeyDown (resetPuzzleKey) || Input.GetKeyDown (resetPuzzleKeyAlt)) && Clicking == false)
		{
			foreach (Transform Child in FormContainer.transform)
			{
				CurrentFormScript = Child.GetComponent<ShadowObject> ();
				if (CurrentFormScript.HasVerticalRotation)
					CurrentFormScript.RandomizeVerticalRotation();
				if (CurrentFormScript.HasHorizontalRotation)
					CurrentFormScript.RandomizeHorizontalRotation();
				if (CurrentFormScript.HasOffsetDisplacement)
					CurrentFormScript.RandomizePosition();
			}
		}
        
        // Interaction Cheking;
		if (Clicking == true)
        {
			MouseMovementStockX += Input.GetAxis("MouseHorizontal") * RotationSpeed;
			MouseMovementStockY += Input.GetAxis("MouseVertical") * RotationSpeed;
            MoveForm();
        }
	}

	// will be applied on each clicked form
	public void OnFormMouseDown(GameObject Form)
	{
		//Debug.Log ("Clicked on " + Form.name);
		MouseMovementStockX = 0.0f;
		MouseMovementStockY = 0.0f;
		Clicking = true;
        CurrentForm = Form;
    }

	public void OnFormMouseUp(GameObject Form)
	{
		//Debug.Log ("Released on " + Form.name);
		MouseMovementStockX = 0.0f;
		MouseMovementStockY = 0.0f;
		Clicking = false;
        CurrentForm = null;
    }
}
