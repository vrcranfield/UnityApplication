using UnityEngine;
using VRTK;

public static class GlobalVariables
{
    public static StaticMenuBehaviour staticMenu;
    public static VRTK_RadialMenu radialMenu;
    public static VRManagerBehaviour vrManager;
    public static RoomBehaviour room;
    public static GameObject paraviewObj;
    public static GameObject frameContainer;

    public static OverlayText overlayText;

    public delegate void CallbackEventHandler(GameObject paraviewObj);
    public static event CallbackEventHandler ParaviewObjectLoadedCallbacks;

    public static void RegisterParaviewObject(GameObject paraviewObj)
    {
        GlobalVariables.paraviewObj = paraviewObj;

        if (ParaviewObjectLoadedCallbacks != null)
            ParaviewObjectLoadedCallbacks(paraviewObj);
    }

}