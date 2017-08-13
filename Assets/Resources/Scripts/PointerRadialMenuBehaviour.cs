using VRTK;

/**
 * Behavior for controller's pointer.
 */
public class PointerRadialMenuBehaviour : VRTK_Pointer {

    // Fields
    protected VRTK_RadialMenu radialMenu;
    protected ControllerBehaviour behaviour;

    /**
     * Called at object's initialization
     */
    protected override void Awake()
    {
        base.Awake();
        behaviour = GetComponent<ControllerBehaviour>();
        radialMenu = transform.FindDeepChild("RadialMenuUI/Panel").GetComponent<VRTK_RadialMenu>();
    }

    /**
     * Callback to pointer activation button pressed
     */
    protected override void DoActivationButtonPressed(object sender, ControllerInteractionEventArgs e)
    {
        // If the controller does not have a radial menu
        if (!behaviour.isRadialMenuController)
        {
            // Trigger pointer
            base.DoActivationButtonPressed(sender, e);
        }
    }
}
