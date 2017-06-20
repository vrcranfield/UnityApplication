using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class PointerBehaviour : VRTK_Pointer {

    protected VRTK_RadialMenu radialMenu = null;

    protected override void DoActivationButtonPressed(object sender, ControllerInteractionEventArgs e)
    {
        if (radialMenu == null)
            radialMenu = GlobalVariables.radialMenu;

        if (radialMenu != null && !radialMenu.isShown)
        {
            base.DoActivationButtonPressed(sender, e);
        }
    }

    protected override void Awake()
    {
        base.Awake();
        radialMenu = GlobalVariables.radialMenu;
    }
}
