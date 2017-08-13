using UnityEngine;
using VRTK;

/**
 * Global variables, for inter-object communication
 */
public static class Globals
{
    // VRTK
    public static VRTK_RadialMenu radialMenu;

    // ParaView
    public static GameObject paraviewObj;
    public static BoundingBoxBehaviour boundingBox;
    public static SlicingPlaneBehaviour slicingPlane;

    // Managers
    public static LogManager logger;
    public static ModeManager modeManager;
    public static HeadsetManager headset;
    public static ControllersManager controllers;
    public static EnvironmentManager room;
    public static AnimationManager animation;
    public static SizeManager sizer;
    public static SlicingManager slicing;
    public static LightManager lighting;
    public static StaticMenuManager staticMenu;

    // Events and callbacks
    public delegate void ParaviewObjectLoaded(GameObject paraviewObj);
    public static event ParaviewObjectLoaded ParaviewObjectLoadedCallbacks;
    public delegate void ParaviewObjectUnloaded();
    public static event ParaviewObjectUnloaded ParaviewObjectUnloadedCallbacks;

    /**
     * Adds a ParaView object to the scene and triggers the callbacks.
     */
    public static void RegisterParaviewObject(GameObject paraviewObj)
    {
        Globals.paraviewObj = paraviewObj;

        // Calbacks
        if (ParaviewObjectLoadedCallbacks != null)
            ParaviewObjectLoadedCallbacks(paraviewObj);
    }

    /**
    * Removes a ParaView object from the scene and triggers the callbacks.
    */
    public static void UnregisterParaviewObject()
    {
        // Callbacks
        if (ParaviewObjectUnloadedCallbacks != null)
            ParaviewObjectUnloadedCallbacks();

        Globals.paraviewObj = null;
    }

}