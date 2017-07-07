using UnityEngine;
using VRTK;

public class ControllerBehaviour : MonoBehaviour
{
    private GameObject radialMenu;

    public bool isRadialMenuController;

    private Interactable collidedObject;

    void Awake()
    {
        radialMenu = transform.Find("RadialMenu").gameObject;
    }

    void Start()
    {
        //Setup controller event listeners for Menu button
        GetComponent<VRTK_ControllerEvents>().ButtonTwoReleased += new ControllerInteractionEventHandler(DoButtonTwoReleased);
        GetComponent<VRTK_ControllerEvents>().TriggerClicked += new ControllerInteractionEventHandler(DoTriggerPressed);
        GetComponent<VRTK_ControllerEvents>().TriggerReleased += new ControllerInteractionEventHandler(DoTriggerReleased);
        GetComponent<VRTK_ControllerEvents>().GripReleased += new ControllerInteractionEventHandler(DoGripReleased);
    }

    private void OnTriggerEnter(Collider collider)
    {
        // We don't want the controllers to see each other
        if (collider.GetComponent<Interactable>() != null)
        {
            Globals.logger.Log("[" + gameObject.name + "] Entered collision with: " + collider.gameObject.name);
            Globals.boundingBox.Show();
            collidedObject = collider.GetComponent<Interactable>();
        }
    }
    private void OnTriggerExit(Collider collider)
    {
        // We don't want the controllers to see each other
        if (collider.GetComponent<Interactable>() != null)
        {
            Globals.logger.Log("[" + gameObject.name + "] Exited collision with: " + collider.gameObject.name);
            Globals.boundingBox.Hide();
            collidedObject = null;
        }
    }

    private void DoTriggerPressed(object sender, ControllerInteractionEventArgs e)
    {
        if (collidedObject != null)
            collidedObject.OnBeginInteraction(this);
    }

    private void DoTriggerReleased(object sender, ControllerInteractionEventArgs e)
    {
        if(collidedObject != null)
            collidedObject.OnEndInteraction(this);
    }

    private void DoButtonTwoReleased(object sender, ControllerInteractionEventArgs e)
    {
        if(Globals.staticMenu.isShown())
        {
            Globals.staticMenu.Hide();
        } else
        {
            Globals.staticMenu.Show();
        }
    }

    private void DoGripReleased(object sender, ControllerInteractionEventArgs e)
    {
        if(Globals.slicingManager.IsShowing())
        {
            //Globals.slicingManager.HighlightIntersection();
            Globals.slicingManager.Clip();
        }
    }

    public void SetControllerMode(bool isRadialMenu)
    {
        radialMenu.SetActive(isRadialMenu);
        isRadialMenuController = isRadialMenu;
    }
}
