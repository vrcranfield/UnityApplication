using UnityEngine;
using UnityEngine.UI;
using VRTK;

/// <summary>
/// This canvas adds a frames per second text element to the headset. To use the prefab it must be placed into the scene then the headset camera needs attaching to the canvas:
/// </summary>
/// <remarks>
///   * Select `FramesPerSecondCanvas` object from the scene objects
///   * Find the `Canvas` component
///   * Set the `Render Camera` parameter to the camera used by the VR Headset (e.g. SteamVR: [CameraRig]-> Camera(Head) -> Camera(eye)])
///
/// This script is pretty much a copy and paste from the script at: http://talesfromtherift.com/vr-fps-counter/ So all credit to Peter Koch for his work. Twitter: @peterept
/// </remarks>
/// <example>
/// `VRTK/Examples/018_CameraRig_FramesPerSecondCounter` displays the frames per second in the centre of the headset view. Pressing the trigger generates a new sphere and pressing the touchpad generates ten new spheres. Eventually when lots of spheres are present the FPS will drop and demonstrate the prefab.
/// </example>
public class LogManager : MonoBehaviour
{
    [Tooltip("The size of the font the text is displayed in.")]
    public int fontSize = 32;
    [Tooltip("The position of the text within the headset view.")]
    public Vector3 position = Vector3.zero;
    [Tooltip("The colour of the text for warnings.")]
    public Color warnColor = Color.yellow;
    [Tooltip("The colour of the text for errors.")]
    public Color badColor = Color.red;

    private Color defaultColor;

    protected const float updateInterval = 0.5f;
    protected Canvas canvas;
    protected Text text;

    void Awake()
    {
        Globals.logger = this;

        VRTK_SDKManager sdkManager = VRTK_SDKManager.instance;
        if (sdkManager != null)
        {
            sdkManager.LoadedSetupChanged += LoadedSetupChanged;
        }

        canvas = transform.GetComponentInParent<Canvas>();
        text = GetComponent<Text>();

        if (canvas != null)
        {
            canvas.planeDistance = 0.5f;
        }

        if (text != null)
        {
            text.fontSize = fontSize;
            text.transform.localPosition = position;
            defaultColor = text.color;
        }
        SetCanvasCamera();

    }

    protected virtual void LoadedSetupChanged(VRTK_SDKManager sender, VRTK_SDKManager.LoadedSetupChangeEventArgs e)
    {
        SetCanvasCamera();
    }

    protected virtual void SetCanvasCamera()
    {
        Transform sdkCamera = VRTK_DeviceFinder.HeadsetCamera();
        if (sdkCamera != null)
        {
            canvas.worldCamera = sdkCamera.GetComponent<Camera>();
        }
    }

    public void Log(string message)
    {
        text.text = message;
        text.color = defaultColor;
        Debug.Log(message);
    }

    public void LogWarning(string message)
    {
        text.text = message;
        text.color = warnColor;
        Debug.LogWarning(message);
    }

    public void LogError(string message)
    {
        text.text = message;
        text.color = badColor;
        Debug.LogError(message);
    }

    public void ToggleShow(bool value)
    {
        text.enabled = value;
    }
}
