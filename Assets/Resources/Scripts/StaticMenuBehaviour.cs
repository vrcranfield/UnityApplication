using UnityEngine;
using UnityEngine.UI;

public class StaticMenuBehaviour : MonoBehaviour {

    private VRManagerBehaviour vrManager;
    private GameObject headset;
    private RoomBehaviour room;

    void Awake()
    {
        // Register
        GlobalVariables.staticMenu = this;

        // Save references
        headset = GameObject.FindGameObjectWithTag("Headset");

        // Hide
        gameObject.SetActive(false);
    }

    void Start () {
        vrManager = GlobalVariables.vrManager;
        room = GlobalVariables.room;
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
        Debug.Log("Close");
        gameObject.SetActive(false);

    }

    public void OnLightIntensitySliderChanged(Slider slider)
    {
        float value = slider.value;
        RenderSettings.ambientLight = new Color(value, value, value, 1);
    }

    public void OnInvertHandsToggleChanged(bool value)
    {
        vrManager.SetControllersSwap(value);
    }

    public void OnShowEnvironmentToggleChanged(bool value)
    {
        room.ToggleShow(value);
    }

    public void Show()
    {
        // Get the XZ projection of the forward position of the headset camera
        Vector3 lookForwardPositionXZ = new Vector3(headset.transform.forward.x, 0, headset.transform.forward.z);

        // Gets the position at exactly 2.5 meters in front of the headset, along the XZ plane.
        Vector3 headsetFrontPosition = headset.transform.position + 2.5f * lookForwardPositionXZ.normalized;

        // Sets the menu position to headsetFrontPosition, but at the same height as before.
        transform.position = new Vector3(headsetFrontPosition.x, transform.position.y, headsetFrontPosition.z);

        // Set the rotation so that the menu faces the camera
        transform.rotation = Quaternion.LookRotation(transform.position - headset.transform.position);

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
}
