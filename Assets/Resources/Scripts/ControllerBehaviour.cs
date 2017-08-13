using UnityEngine;
using VRTK;

/**
 * Behavior for both HTC Vive controllers
 */
public class ControllerBehaviour : MonoBehaviour
{
    // Fields
    private GameObject radialMenu;
    private Interactable collidedObject;
    public bool isRadialMenuController;

    /**
     * Called at object's initialization
     */
    void Awake()
    {
        // Save reference to Radial Menu
        radialMenu = transform.Find("RadialMenu").gameObject;
    }

    /**
     * Called at object's initialization, after all Awakes.
     */
    void Start()
    {
        // Register callbacks to button press
        GetComponent<VRTK_ControllerEvents>().ButtonTwoReleased += new ControllerInteractionEventHandler(DoButtonTwoReleased);
        GetComponent<VRTK_ControllerEvents>().TriggerClicked += new ControllerInteractionEventHandler(DoTriggerPressed);
        GetComponent<VRTK_ControllerEvents>().TriggerReleased += new ControllerInteractionEventHandler(DoTriggerReleased);
    }

    /**
     * Callback for object collision
     */
    private void OnTriggerEnter(Collider collider)
    {
        // Consider collision only with Interactable objects
        if (collider.GetComponent<Interactable>() != null)
        {
            // Show the bounding box if it's enabled
            Globals.boundingBox.Show();

            // Save reference to object
            collidedObject = collider.GetComponent<Interactable>();
        }
    }

    /**
     * Callback for the end of an object collision
     */
    private void OnTriggerExit(Collider collider)
    {
        // Consider collision only with Interactable objects
        if (collider.GetComponent<Interactable>() != null)
        {
            // Hide the bounding box if enabled
            Globals.boundingBox.Hide();

            // Delete reference
            collidedObject = null;
        }
    }

    /**
     * Callback for pressing the trigger button
     */
    private void DoTriggerPressed(object sender, ControllerInteractionEventArgs e)
    {
        // If currently colliding with the object, grab it
        if (collidedObject != null)
            collidedObject.OnBeginInteraction(this);
    }


    /**
     * Callback for releasing the trigger button
     */
    private void DoTriggerReleased(object sender, ControllerInteractionEventArgs e)
    {
        // If currently colliding with the object, release it
        if (collidedObject != null)
            collidedObject.OnEndInteraction(this);
    }

    /**
     * Callback for clicking the menu button
     */
    private void DoButtonTwoReleased(object sender, ControllerInteractionEventArgs e)
    {
        // Toggle the static menu
        if(Globals.staticMenu.isShown())
        {
            Globals.staticMenu.Hide();
        } else
        {
            Globals.staticMenu.Show();
        }
    }

    /**
     * Enables or disables the radial menu for this controller
     */
    public void SetControllerMode(bool isRadialMenu)
    {
        radialMenu.SetActive(isRadialMenu);
        isRadialMenuController = isRadialMenu;
    }
}
