using UnityEngine;
using VRTK;

/**
 * Manager for the Controllers of the ParaView object
 */
public class ControllersManager : MonoBehaviour {

    // Fields
    private VRTK_SDKManager manager;

    private GameObject leftController;
    private GameObject rightController;

    private ControllerBehaviour leftBehaviour;
    private ControllerBehaviour rightBehaviour;

    /**
     * Called at object's initialization
     */
    void Awake()
    {
        // Register self to globals
        Globals.controllers = this;

        // Save references
        manager = gameObject.GetComponent<VRTK_SDKManager>();

        leftController = manager.scriptAliasLeftController;
        rightController = manager.scriptAliasRightController;

        leftBehaviour = leftController.GetComponent<ControllerBehaviour>();
        rightBehaviour = rightController.GetComponent<ControllerBehaviour>();

        // Register callback to ParaView Object Loaded event
        Globals.ParaviewObjectLoadedCallbacks += OnParaviewObjectLoaded;
        Globals.ParaviewObjectUnloadedCallbacks += OnParaviewObjectUnloaded;
    }

    /**
     * Called at object's initialization, after all Awakes.
     */
    void Start () {
        // By default, controllers have no radial menu
        SetControllersDefault();
    }

    /**
     * Callback to the ParaViewObjectLoaded event
     */
    public void OnParaviewObjectLoaded(GameObject paraviewObj)
    {
        // Set the controllers with radial menu on left
        SetControllersSwap(false);
    }

    /**
     * Callback to the ParaViewObjectLoaded event
     */
    public void OnParaviewObjectUnloaded()
    {
        // Disable radial menu on both controllers
        SetControllersDefault();
    }

    /**
     * Disables radial menu on both controllers
     */
    public void SetControllersDefault()
    {
        leftBehaviour.SetControllerMode(false);
        rightBehaviour.SetControllerMode(false);
    }

    /**
     * Enables radial menu on one controller
     */
    public void SetControllersSwap(bool swapped)
    {
        leftBehaviour.SetControllerMode(!swapped);
        rightBehaviour.SetControllerMode(swapped);
    }

    /**
     * Returns a reference to the controller withouth the radial menu
     */
    public ControllerBehaviour getNonRadialController()
    {
        return (leftBehaviour.isRadialMenuController ? rightBehaviour : leftBehaviour);
    }
}
