using UnityEngine;
using VRTK;

public static class GlobalVariables
{
    public static StaticMenuBehaviour staticMenu;
    public static VRTK_RadialMenu radialMenu;
    public static VRManagerBehaviour vrManager;
    public static RoomBehaviour room;
    public static GameObject paraviewObj;

    public delegate void CallbackEventHandler(GameObject paraviewObj);
    public static event CallbackEventHandler Callback;

    public static void RegisterParaviewObject(GameObject paraviewObj)
    {
        GlobalVariables.paraviewObj = paraviewObj;
        if (Callback != null)
            Callback(paraviewObj);
    }

}