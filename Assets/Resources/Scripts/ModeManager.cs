using UnityEngine;

/**
 * Manager for runtime mode of the application
 */
public class ModeManager : MonoBehaviour {

    // Constants, set at compile time
#if UNITY_EDITOR
    private const bool EDITOR_MODE = true;
#else
    private const bool EDITOR_MODE = false;
#endif

    /**
     * Called at object's initialization
     */
    void Awake() {
        // Register self to globals
        Globals.modeManager = this;
    }

    /**
     * Retuns current runtime mode
     */
    public bool isEditorMode()
    {
        return EDITOR_MODE;
    }

    /**
     * Quits the application according to mode
     */
     public void QuitApplication()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
