using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeManager : MonoBehaviour {

#if UNITY_EDITOR
    private const bool EDITOR_MODE = true;
#else
    private const bool EDITOR_MODE = false;
#endif

    private OverlayText overlayText;

    void Awake() {
        GlobalVariables.modeManager = this;
        Debug.Log("Paraview Loader running in " + ((EDITOR_MODE) ? "Editor" : "Player") + " mode");
    }

    void Start()
    {
        overlayText = GlobalVariables.overlayText;
    }

    public bool isEditorMode()
    {
        return EDITOR_MODE;
    }

    public void Log(string message)
    {
        if(EDITOR_MODE)
        {
            Debug.Log(message);
        } else
        {
            overlayText.SetText(message);
        }
    }

    
}
