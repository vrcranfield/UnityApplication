﻿using UnityEngine;
using VRTK;

public static class Globals
{
    public static StaticMenuBehaviour staticMenu;
    public static VRTK_RadialMenu radialMenu;

    public static GameObject paraviewObj;

    public static LogManager logger;
    public static ModeManager modeManager;
    public static HeadsetManager headset;
    public static ControllersManager controllers;
    public static EnvironmentManager room;
    public static AnimationManager animation;
    public static SizeManager sizer;

    public delegate void CallbackEventHandler(GameObject paraviewObj);
    public static event CallbackEventHandler ParaviewObjectLoadedCallbacks;

    public static void RegisterParaviewObject(GameObject paraviewObj)
    {
        Globals.paraviewObj = paraviewObj;

        if (ParaviewObjectLoadedCallbacks != null)
            ParaviewObjectLoadedCallbacks(paraviewObj);
    }

}