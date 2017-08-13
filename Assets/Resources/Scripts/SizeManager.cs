using UnityEngine;

/**
 * Manager for the resizing options of the ParaView object
 */
public class SizeManager : MonoBehaviour
{
    // Unity editor options
    [Tooltip("Activate or deactivate autoresizing")]
    public bool isAutoResizing = true;
    [Tooltip("Set target radius")]
    public float targetSize = 2;
    [Tooltip("Scaling speed")]
    public float scaleSpeed = 1.0f;

    // Constants
    private float NON_LINEARITY_FACTOR = 0.02f;

    // Fields
    private GameObject obj;

    /**
     * Called at object's initialization
     */
    void Awake()
    {
        // Register self to globals
        Globals.sizer = this;

        // Register callback to ParaView Object Loaded event
        Globals.ParaviewObjectLoadedCallbacks += OnParaviewObjectLoaded;
        Globals.ParaviewObjectUnloadedCallbacks += OnParaviewObjectUnloaded;
    }

    /**
     * Callback to the ParaViewObjectLoaded event
     */
    public void OnParaviewObjectLoaded(GameObject paraviewObj)
    {
        this.obj = paraviewObj;

        // If enabled, autoresize the object
        if (isAutoResizing)
            AutoResize();
    }

    /**
     * Callback to the ParaViewObjectUnloaded event
     */
    public void OnParaviewObjectUnloaded()
    {
        this.obj = null;
    }

    /**
     * Increase the size of the object
     */
    public void ScaleUp()
    {
        if (obj != null)
        {
            obj.transform.localScale *= ((1 + NON_LINEARITY_FACTOR) + scaleSpeed * Time.deltaTime);
        }
    }

    /**
     * Decrase the size of the object
     */
    public void ScaleDown()
    {
        if (obj != null)
        {
            obj.transform.localScale *= ((1 - NON_LINEARITY_FACTOR) - scaleSpeed * Time.deltaTime);
        }
    }

    /**
     * Resizes object to default target dimension
     */
    public void AutoResize()
    {
        float objectRadius = obj.GetComponentInChildren<MeshRenderer>().bounds.size.magnitude;
        obj.transform.localScale = targetSize * obj.transform.localScale / objectRadius;
    }

}
