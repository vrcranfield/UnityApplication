using UnityEngine;
using UnityEngine.UI;

public class StaticMenuBehaviour : MonoBehaviour {

    private VRManagerBehaviour vrManager;
    private GameObject room;

	// Use this for initialization
	void Start () {
        // Register
        GlobalVariables.staticMenu = gameObject;
        vrManager = GlobalVariables.vrManager;
        room = GameObject.FindGameObjectWithTag("Room");
        gameObject.SetActive(false);
        
	}
	
	// Update is called once per frame
	void Update () {
		
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
        if(vrManager == null)
        {
            vrManager = GlobalVariables.vrManager;
        }

        vrManager.SetControllersSwap(value);
    }

    public void OnShowEnvironmentToggleChanged(bool value)
    {
        room.SetActive(value);
    }
}
