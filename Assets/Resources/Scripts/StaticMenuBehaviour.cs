using UnityEngine;
using UnityEngine.UI;

public class StaticMenuBehaviour : MonoBehaviour {

    private VRManagerBehaviour vrManager;
    private GameObject room;

    void Awake()
    {
        // Register
        GlobalVariables.staticMenu = gameObject;

        // Save references
        room = GameObject.FindGameObjectWithTag("Room");

        // Hide
        gameObject.SetActive(false);
    }

    void Start () {
        vrManager = GlobalVariables.vrManager;
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
        room.SetActive(value);
    }
}
