using UnityEngine;
using VRTK;

public class ControllerBehaviour : MonoBehaviour
{
    private GameObject radialMenu;

    public bool isRadialMenuController;

    private GameObject pickedObject;

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
    }

    private void OnTriggerEnter(Collider collider)
    {
        pickedObject = collider.gameObject;
    }
    private void OnTriggerExit(Collider collider)
    {
        pickedObject = null;
    }

    private void DoTriggerPressed(object sender, ControllerInteractionEventArgs e)
    {
        if (pickedObject != null)
            pickedObject.transform.parent = this.transform;
    }

    private void DoTriggerReleased(object sender, ControllerInteractionEventArgs e)
    {
        if(pickedObject != null)
            pickedObject.transform.parent = null;
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

    public void SetControllerMode(bool isRadialMenu)
    {
        radialMenu.SetActive(isRadialMenu);
        isRadialMenuController = isRadialMenu;
    }
}
