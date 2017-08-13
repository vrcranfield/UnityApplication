using UnityEngine;
using UnityEngine.UI;
using VRTK;

/**
 * Manager for the message logger
 */
public class LogManager : MonoBehaviour
{
    // Unity editor options
    [Tooltip("The size of the font the text is displayed in.")]
    public int fontSize = 32;
    [Tooltip("The position of the text within the headset view.")]
    public Vector3 position = Vector3.zero;
    [Tooltip("The colour of the text for warnings.")]
    public Color warnColor = Color.yellow;
    [Tooltip("The colour of the text for errors.")]
    public Color badColor = Color.red;

    // Fields
    private Color defaultColor;
    protected Canvas canvas;
    protected Text text;

    /**
     * Called at object's initialization
     */
    void Awake()
    {
        // Register self to globals
        Globals.logger = this;

        // Register callback to VR Setup Changed event
        VRTK_SDKManager sdkManager = VRTK_SDKManager.instance;
        if (sdkManager != null)
        {
            sdkManager.LoadedSetupChanged += LoadedSetupChanged;
        }

        // Save references
        canvas = transform.GetComponentInParent<Canvas>();
        text = GetComponent<Text>();

        // Set up objects
        SetUpCanvas();
        SetUpText();
        SetCanvasCamera();
    }

    /**
     * Initializes up the canvas containing the text
     */
    private void SetUpCanvas()
    {
        if (canvas != null)
            canvas.planeDistance = 0.5f;
    }

    /**
     * Initializes up the text object
     */
    private void SetUpText()
    {
        if (text != null)
        {
            text.fontSize = fontSize;
            text.transform.localPosition = position;
            defaultColor = text.color;
        }

    }

    /**
     * Attaches the object to the HMD camera
     */
    protected virtual void SetCanvasCamera()
    {
        Transform sdkCamera = VRTK_DeviceFinder.HeadsetCamera();
        if (sdkCamera != null)
        {
            canvas.worldCamera = sdkCamera.GetComponent<Camera>();
        }
    }

    /**
     * Callback to VR Setup Changed event
     */
    protected virtual void LoadedSetupChanged(VRTK_SDKManager sender, VRTK_SDKManager.LoadedSetupChangeEventArgs e)
    {
        SetCanvasCamera();
    }

    /**
     * Logs an information message
     */
    public void Log(string message)
    {
        text.text = message;
        text.color = defaultColor;
        Debug.Log(message);
    }

    /**
     * Logs a warning message
     */
    public void LogWarning(string message)
    {
        text.text = message;
        text.color = warnColor;
        Debug.LogWarning(message);
    }

    /**
     * Logs an error message
     */
    public void LogError(string message)
    {
        text.text = message;
        text.color = badColor;
        Debug.LogError(message);
    }

    /**
     * Toggles log visibility
     */
    public void ToggleShow(bool value)
    {
        text.enabled = value;
    }
}
