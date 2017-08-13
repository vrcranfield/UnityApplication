using UnityEngine;
using UnityEngine.UI;

/**
 * Manager for the static menu
 */
public class StaticMenuManager : MonoBehaviour
{
    // Fields
    private ControllersManager controllers;
    private EnvironmentManager room;
    private LogManager logger;
    private HeadsetManager headset;
    private BoundingBoxBehaviour boundingBox;
    private LightManager lighting;
    private ModeManager mode;

    /**
     * Called at object's initialization
     */
    void Awake()
    {
        // Register self to globals
        Globals.staticMenu = this;

        // Register callback to ParaView Object Loaded event
        Globals.ParaviewObjectLoadedCallbacks += OnParaviewObjectLoaded;
        Globals.ParaviewObjectUnloadedCallbacks += OnParaviewObjectUnloaded;

        // Hide the menu
        Hide();
    }

    /**
     * Called at object's initialization, after all Awakes.
     */
    void Start()
    {
        // Save references
        controllers = Globals.controllers;
        room = Globals.room;
        logger = Globals.logger;
        headset = Globals.headset;
        boundingBox = Globals.boundingBox;
        lighting = Globals.lighting;
        mode = Globals.modeManager;
    }

    /**
     * Callback to the ParaViewObjectLoaded event
     */
    public void OnParaviewObjectLoaded(GameObject paraviewObj)
    {
        // Enable the inter hands and the bounding box toggles
        transform.FindDeepChild("InvertHandsToggle").GetComponent<Toggle>().interactable = true;
        transform.FindDeepChild("EnableBoundingBoxToggle").GetComponent<Toggle>().interactable = true;
    }

    /**
     * Callback to the ParaViewObjectunloaded event
     */
    public void OnParaviewObjectUnloaded()
    {
        // Disable the inter hands and the bounding box toggles
        transform.FindDeepChild("InvertHandsToggle").GetComponent<Toggle>().interactable = false;
        transform.FindDeepChild("EnableBoundingBoxToggle").GetComponent<Toggle>().interactable = false;
    }

    /**
     * Callback for the quit button.
     */
    public void OnQuitButtonClicked()
    {
        // Quit the application
        if(mode.isEditorMode())
            UnityEditor.EditorApplication.isPlaying = false;
        else
            Application.Quit();
    }

    /**
     * Callback for the close button.
     */
    public void OnCloseButtonClicked()
    {
        // Hide the menu
        Hide();
    }

    /**
     * Callback for the light intensity slider.
     */
    public void OnLightIntensitySliderChanged(Slider slider)
    {
        lighting.SetIntensity(slider.value);
    }
    
    /**
     * Callback for the invert hands toggle.
     */
    public void OnInvertHandsToggleChanged(bool value)
    {
        controllers.SetControllersSwap(value);
    }

    /**
     * Callback for the show environment toggle.
     */
    public void OnShowEnvironmentToggleChanged(bool value)
    {
        room.ToggleShow(value);
    }

    /**
     * Callback for the show log toggle.
     */
    public void OnShowLogToggleChanged(bool value)
    {
        logger.ToggleShow(value);
    }

    /**
     * Callback for the show bounding box toggle.
     */
    public void OnEnableBoundingBoxChanged(bool value)
    {
        boundingBox.ToggleActive(value);
    }

    /**
     * Shows the menu
     */
    public void Show()
    {
        // Places itself in front of the camera
        UpdatePosition();

        // Show menu
        gameObject.SetActive(true);
    }

    /**
     * Hides the menu
     */
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    /**
     * Returns the current visibility status
     */
    public bool isShown()
    {
        return gameObject.activeSelf;
    }

    /**
     * Positions the menu in front of the camera
     */
    private void UpdatePosition()
    {
        if (headset == null)
        {
            headset = Globals.headset;
        }

        // Gets the position at 2.5m in front of the headset, along the XZ plane.
        Vector3 headsetFrontPosition = Globals.headset.GetFrontPositionXZ(2.5f);

        // Sets the menu's x and y coordinate to headsetFrontPosition.
        transform.position = new Vector3(headsetFrontPosition.x, transform.position.y, headsetFrontPosition.z);

        // Sets the rotation so that the menu faces the camera
        transform.rotation = Quaternion.LookRotation(transform.position - Globals.headset.transform.position);
    }
}
