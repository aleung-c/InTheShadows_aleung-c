using UnityEngine;
using UnityEngine.Events;
using System.Collections;

// Class proto for Event with params.
public class ObjectClickingEvent : UnityEvent<GameObject>{}

/// <summary>
/// Shadow game play. Works with event sending clicked form;
/// </summary>
public class ShadowGamePlay : MonoBehaviour {
    public GameObject   FormContainer;
    [Header("In game private visible")]
    [SerializeField]
    private bool        clicking;

    [SerializeField]
    private bool        pressingVerticalMode;
    [SerializeField]
    private bool        pressingDisplacementMode;

    private Vector3     newFormRotation;
    private Vector3     newFormPosition;

    // keys
    private KeyCode      verticalModeKey;
    private KeyCode      verticalModeKeyAlt;

    private KeyCode      displacementModeKey;
    private KeyCode      displacementModeKeyAlt;

    // Settings
    private float                RotationSpeed;
    private bool                 HasHorizontalRotation;
    private bool                 HasVerticalRotation;
    private bool                 HasOffsetDisplacement;
    private bool                 IsMultiObjects;

    [SerializeField]
    private GameObject CurrentForm;

    void Awake()
    {
        FormContainer = transform.Find("FormContainer").gameObject;

        verticalModeKey = GameManager.instance.KeyManager.VerticalPuzzleButton;
        verticalModeKeyAlt = GameManager.instance.KeyManager.VerticalPuzzleButtonAlt;

        displacementModeKey = GameManager.instance.KeyManager.DisplacementPuzzleButton;
        displacementModeKeyAlt = GameManager.instance.KeyManager.DisplacementPuzzleButtonAlt;
    }

    // Use this for initialization
    void OnEnable()
    {
        // Get PuzzleSettings
        RotationSpeed = GetComponent<ShadowLevelObject>().RotationSpeed;
        HasHorizontalRotation = GetComponent<ShadowLevelObject>().HasHorizontalRotation;
        HasVerticalRotation = GetComponent<ShadowLevelObject>().HasVerticalRotation;
        HasOffsetDisplacement = GetComponent<ShadowLevelObject>().HasOffsetDisplacement;
        IsMultiObjects = GetComponent<ShadowLevelObject>().IsMultiObjects;
        foreach (Transform Child in FormContainer.transform) {
            Child.GetComponent<ShadowObject>().ClickCatcher.OnClickDown.AddListener(OnFormMouseDown);
            Child.GetComponent<ShadowObject>().ClickCatcher.OnClickUp.AddListener(OnFormMouseUp);
        }
    }

    // Avoiding memory leaks
    void OnDisable()
    {
        foreach (Transform Child in FormContainer.transform) {
            Child.GetComponent<ShadowObject>().ClickCatcher.OnClickDown.RemoveListener(OnFormMouseDown);
            Child.GetComponent<ShadowObject>().ClickCatcher.OnClickUp.RemoveListener(OnFormMouseUp);
        }
    }

    void MoveForm()
    {
        if (CurrentForm)
        {
            //newFormRotation = CurrentForm.GetComponent<ShadowObject>().ObjRotation.transform.rotation;
            newFormPosition = CurrentForm.GetComponent<ShadowObject>().ObjOffset.transform.localPosition;

            // Priority order : displacement > VerticalMode > nothing pressed (horizontal)
            // Displacement //
            if (HasOffsetDisplacement && pressingDisplacementMode)
            {
                newFormPosition.y += Input.GetAxis("MouseVertical") * Time.deltaTime;
                Debug.Log(newFormPosition.y);
                if (newFormPosition.y < -0.9F)
                    newFormPosition.y = -0.9F;
                else if (newFormPosition.y > 0.9F)
                    newFormPosition.y = 0.9F;
                newFormPosition.x += Input.GetAxis("MouseHorizontal") * Time.deltaTime;
                if (newFormPosition.x < -1.5F)
                    newFormPosition.x = -1.5F;
                else if (newFormPosition.x > 1.5F)
                    newFormPosition.x = 1.5F;
            }
            // Vertical //
            else if (HasVerticalRotation && pressingVerticalMode)
            {
                newFormRotation.x = Input.GetAxis("MouseVertical");
                newFormRotation.y = 0.0F;
                newFormRotation.z = 0.0F;
            }
            // Horizontal //
            else if (HasHorizontalRotation)
            {
                newFormRotation.y = Input.GetAxis("MouseHorizontal");
                newFormRotation.x = 0.0F;
                newFormRotation.z = 0.0F;
            }
            CurrentForm.GetComponent<ShadowObject>().ObjOffset.transform.localPosition = newFormPosition;
            CurrentForm.GetComponent<ShadowObject>().ObjRotation.transform.Rotate(newFormRotation, RotationSpeed * Time.deltaTime * 10.0F, Space.World);
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
        
        // Interaction Cheking;
        if (clicking == true)
        {
            MoveForm();
        }
	}

	// will be applied on each clicked form
	public void OnFormMouseDown(GameObject Form)
	{
		Debug.Log ("Clicked on " + Form.name);
        clicking = true;
        CurrentForm = Form;
    }

	public void OnFormMouseUp(GameObject Form)
	{
		Debug.Log ("Released on " + Form.name);
        clicking = false;
        CurrentForm = null;
    }
}
