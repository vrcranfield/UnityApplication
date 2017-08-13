using UnityEngine;

/**
 * Manager for the animation of the ParaView object
 */
public class AnimationManager : MonoBehaviour
{
    // Constants
    const int DELAY_COUNT = 2;

    // Fields
    private GameObject obj;
    private int currentFrame = 0;
	private int delay = 0;
    private bool isPlaying = false;

    // Event handling
    public delegate void CallbackEventHandler();
    public event CallbackEventHandler NextFrameLoadedCallbacks;

    /**
     * Called at object's initialization
     */
    void Awake()
    {
        // Register self to globals
        Globals.animation = this;

        // Register callback to ParaView Object Loaded event
        Globals.ParaviewObjectLoadedCallbacks += OnParaviewObjectLoaded;
        Globals.ParaviewObjectUnloadedCallbacks += OnParaviewObjectUnloaded;
    }

    /**
     * Called at object's initialization, after all Awakes.
     */
    void Start()
    {
        ShowNextFrame();
    }

    /**
     * Called at every frame
     */
    void Update()
    {
        if (isPlaying)
        {
            if (delay >= DELAY_COUNT)
            {
                // Update frame
                delay = 0;
                ShowNextFrame();
            }
            else
            {
                // Wait
                delay++;
            }
        }
    }

    /**
     * Callback to the ParaViewObjectLoaded event
     */
    public void OnParaviewObjectLoaded(GameObject paraviewObj)
    {
        // Save reference to the root of the frames
        this.obj = Globals.paraviewObj.transform.FindDeepChild("FramedObject").gameObject;
    }

    /**
     * Callback to the ParaViewObjectUnloaded event
     */
    public void OnParaviewObjectUnloaded()
    {
        this.obj = null;
    }

    /**
     * Play the animation
     */
    public void Play()
    {
        if(isObjectLoaded())
            isPlaying = true;
    }

    /**
     * Pause the animation
     */
    public void Pause()
    {
        if(isObjectLoaded())
            isPlaying = false;
    }

    /**
     * Hide all frames but the next one
     */
    private void ShowNextFrame()
    {
        if(isObjectLoaded())
        {
            // Hide all frames
            foreach (Transform child in obj.transform)
            {
                child.gameObject.SetActive(false);
            }

            // Show next frame
            obj.transform.GetChild(currentFrame).gameObject.SetActive(true);

            // Update counter
            currentFrame = (currentFrame + 1) % obj.transform.childCount;
            
            // Trigger event
            if (NextFrameLoadedCallbacks != null)
                NextFrameLoadedCallbacks();
        }
    }

    /**
     * Returns the availability of any animation
     */
    public bool isAnimation()
    {
        return isObjectLoaded() && obj.transform.childCount > 1;
    }

    /**
     * Returns current status for ParaView object loading
     */
    public bool isObjectLoaded()
    {
        return obj != null;
    }

    /**
     * Returns current animation status
     */
    public bool isAnimationPlaying()
    {
        return isPlaying;
    }
}