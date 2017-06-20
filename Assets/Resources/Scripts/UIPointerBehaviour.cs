using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class UIPointerBehaviour : VRTK_UIPointer {

    protected VRTK_RadialMenu radialMenu;

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
