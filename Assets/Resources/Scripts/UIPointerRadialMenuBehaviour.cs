using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class UIPointerRadialMenuBehaviour : VRTK_UIPointer {

    protected VRTK_RadialMenu radialMenu;

    protected override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        radialMenu = GlobalVariables.radialMenu;
    }

    public override bool PointerActive()
    {
        if (radialMenu != null && radialMenu.isShown)
        {
            return false;
        }

        return base.PointerActive();
    }
}
