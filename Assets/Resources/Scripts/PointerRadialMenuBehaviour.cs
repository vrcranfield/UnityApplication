using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class PointerRadialMenuBehaviour : VRTK_Pointer {

    protected VRTK_RadialMenu radialMenu;

    protected override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        radialMenu = GlobalVariables.radialMenu;
    }

    protected override void DoActivationButtonPressed(object sender, ControllerInteractionEventArgs e)
    {
        if (radialMenu != null && !radialMenu.isShown)
        {
            base.DoActivationButtonPressed(sender, e);
        }
    }
}
