using UnityEngine;

/**
 * Manager for the lighting of the scene
 */
public class LightManager : MonoBehaviour
{
    /**
     * Called at object's initialization
     */
    void Awake()
    {
        // Register self to globals
        Globals.lighting = this;
    }

    /**
     * Set's ambient light intensity to custom value
     */
    public void SetIntensity(float value)
    {
        RenderSettings.ambientLight = new Color(value, value, value, 1);
    }
}
