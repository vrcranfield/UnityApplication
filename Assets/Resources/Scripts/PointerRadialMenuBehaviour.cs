using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class PointerRadialMenuBehaviour : VRTK_Pointer {

    protected VRTK_RadialMenu radialMenu;
    protected ControllerBehaviour behaviour;

    protected override void Awake()
    {
        base.Awake();
        behaviour = GetComponent<ControllerBehaviour>();
        radialMenu = transform.FindDeepChild("RadialMenuUI/Panel").GetComponent<VRTK_RadialMenu>();
    }

    void Start()
    {
    }

    // We want to disable the pointer if this controller is the one with the radial menu
    protected override void DoActivationButtonPressed(object sender, ControllerInteractionEventArgs e)
    {
        if (!behaviour.isRadialMenuController)
        {
            base.DoActivationButtonPressed(sender, e);
        }
    }
}
