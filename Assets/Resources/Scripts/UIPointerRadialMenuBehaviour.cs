using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class UIPointerRadialMenuBehaviour : VRTK_UIPointer {

    protected VRTK_RadialMenu radialMenu;

    public override bool PointerActive()
    {
        if (radialMenu == null)
            radialMenu = GlobalVariables.radialMenu;

        if (radialMenu != null && radialMenu.isShown)
        {
            return false;
        }

        return base.PointerActive();
    }

    protected override void Awake()
    {
        base.Awake();
        radialMenu = GlobalVariables.radialMenu;
    }
}
