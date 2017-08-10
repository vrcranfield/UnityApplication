using UnityEngine;
using UnityEngine.UI;

public class StaticMenuManager : MonoBehaviour
{

    private ControllersManager controllers;
    private EnvironmentManager room;
    private LogManager logger;
    private HeadsetManager headset;
    private BoundingBoxBehaviour boundingBox;
    private LightManager lighting;

    void Awake()
    {
        // Register
        //Globals.staticMenu = this;

        // Hide
        gameObject.SetActive(false);

        Globals.ParaviewObjectLoadedCallbacks += OnParaviewObjectLoaded;
        Globals.ParaviewObjectUnloadedCallbacks += OnParaviewObjectUnloaded;
    }

    void Start()
    {
        controllers = Globals.controllers;
        room = Globals.room;
        logger = Globals.logger;
        headset = Globals.headset;
        boundingBox = Globals.boundingBox;
        lighting = Globals.lighting;
    }

    public void OnParaviewObjectLoaded(GameObject paraviewObj)
    {
        // Enable the invert hands button
        transform.FindDeepChild("InvertHandsToggle").GetComponent<Toggle>().interactable = true;
    }

    public void OnParaviewObjectUnloaded()
    {
        // Disable the invert hands button
        transform.FindDeepChild("InvertHandsToggle").GetComponent<Toggle>().interactable = false;
    }


    public void OnQuitButtonClicked()
    {
#if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void OnCloseButtonClicked()
    {
        gameObject.SetActive(false);

    }

    public void OnLightIntensitySliderChanged(Slider slider)
    {
        lighting.SetIntensity(slider.value);
    }

    public void OnInvertHandsToggleChanged(bool value)
    {
        controllers.SetControllersSwap(value);
    }

    public void OnShowEnvironmentToggleChanged(bool value)
    {
        room.ToggleShow(value);
    }

    public void OnShowLogToggleChanged(bool value)
    {
        logger.ToggleShow(value);
    }

    public void OnEnableBoundingBoxChanged(bool value)
    {
        boundingBox.ToggleActive(value);
    }

    public void Show()
    {
        // Places itself in front of the camera
        UpdatePosition();

        // Show menu
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public bool isShown()
    {
        return gameObject.activeSelf;
    }

    private void UpdatePosition()
    {
        if (headset == null)
        {
            // Necessary because the headset can take a while to spawn
            headset = Globals.headset;
        }
        // Gets the position at exactly 2.5 meters in front of the headset, along the XZ plane.
        Vector3 headsetFrontPosition = Globals.headset.GetFrontPositionXZ(2.5f);

        // Sets the menu position to headsetFrontPosition, but at the same height as before.
        transform.position = new Vector3(headsetFrontPosition.x, transform.position.y, headsetFrontPosition.z);

        // Set the rotation so that the menu faces the camera
        transform.rotation = Quaternion.LookRotation(transform.position - Globals.headset.transform.position);
    }
}
