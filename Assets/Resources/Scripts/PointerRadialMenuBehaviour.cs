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
        radialMenu = gameObject.transform.Find("RadialMenu").GetComponent<VRTK_RadialMenu>();
    }

    void Start()
    {
    }

    protected override void DoActivationButtonPressed(object sender, ControllerInteractionEventArgs e)
    {
        if (!behaviour.isRadialMenuController || !radialMenu.isShown)
        {
            base.DoActivationButtonPressed(sender, e);
        }
    }
}
