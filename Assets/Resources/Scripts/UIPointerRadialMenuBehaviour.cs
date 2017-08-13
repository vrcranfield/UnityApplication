using VRTK;

/**
 * Behavior for controller's pointer-UI interactions.
 */
public class UIPointerRadialMenuBehaviour : VRTK_UIPointer {

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
     * Handles interaction between the pointer and the UI elements
     */
    public override bool PointerActive()
    {
        // Disable pointer interactions for radial menu controller
        if (behaviour.isRadialMenuController)
            return false;

        return base.PointerActive();
    }
}
