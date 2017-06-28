using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeManager : MonoBehaviour {

#if UNITY_EDITOR
    private const bool EDITOR_MODE = true;
#else
    private const bool EDITOR_MODE = false;
#endif

    private LogManager overlayText;

    void Awake() {
        Globals.modeManager = this;
    }

    void Start()
    {
        Globals.logger.Log("Paraview Loader running in " + ((EDITOR_MODE) ? "Editor" : "Player") + " mode");
    }

    public bool isEditorMode()
    {
        return EDITOR_MODE;
    }   
}
