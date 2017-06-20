using UnityEngine;
using UnityEngine.UI;

public class StaticMenuBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
        // Register
        GlobalVariables.staticMenu = gameObject;
        gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnQuitClicked()
    {
    #if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }

    public void OnCloseClicked()
    {
        Debug.Log("Close");
        gameObject.SetActive(false);

    }

    public void OnLightIntensityChanged(Slider slider)
    {
        float value = slider.value;
        RenderSettings.ambientLight = new Color(value, value, value, 1);
    }

    public void OnInvertHandsToggled(bool value)
    {
        Debug.Log("Invert hands changed to " + value);
    }

}
