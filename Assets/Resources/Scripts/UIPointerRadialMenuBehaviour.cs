using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class UIPointerRadialMenuBehaviour : VRTK_UIPointer {

    protected VRTK_RadialMenu radialMenu;
    protected ControllerBehaviour behaviour;

    protected override void Awake()
    {
        base.Awake();
        behaviour = GetComponent<ControllerBehaviour>();
    }

    void Start()
    {
        foreach (Transform child in transform)
            if (child.CompareTag("RadialMenu"))
                radialMenu = child.GetComponent<VRTK_RadialMenu>();
    }

    public override bool PointerActive()
    {
        if (!behaviour.isRadialMenuController || radialMenu.isShown)
        {
            return false;
        }

        return base.PointerActive();
    }
}
