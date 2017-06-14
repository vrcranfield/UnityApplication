using UnityEngine;

public class CanvasBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
        // Register
        GlobalVariables.menu = gameObject;
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

}
