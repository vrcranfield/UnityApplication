using UnityEngine;
using VRTK;

public static class Globals
{
    public static VRTK_RadialMenu radialMenu;

    public static GameObject paraviewObj;
    public static BoundingBoxBehaviour boundingBox;
    public static SlicingPlaneBehaviour slicingPlane;

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

    public delegate void ParaviewObjectLoaded(GameObject paraviewObj);
    public static event ParaviewObjectLoaded ParaviewObjectLoadedCallbacks;
    public delegate void ParaviewObjectUnloaded();
    public static event ParaviewObjectUnloaded ParaviewObjectUnloadedCallbacks;

    public static void RegisterParaviewObject(GameObject paraviewObj)
    {
        Globals.paraviewObj = paraviewObj;

        if (ParaviewObjectLoadedCallbacks != null)
            ParaviewObjectLoadedCallbacks(paraviewObj);
    }

    public static void UnregisterParaviewObject()
    {
        if (ParaviewObjectUnloadedCallbacks != null)
            ParaviewObjectUnloadedCallbacks();

        Globals.paraviewObj = null;
    }

}