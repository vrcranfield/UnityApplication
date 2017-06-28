using UnityEngine;
using VRTK;

public class ControllerBehaviour : MonoBehaviour
{
    private GameObject radialMenu;

    public bool isRadialMenuController;

    private void Awake()
    {
        radialMenu = transform.Find("RadialMenu").gameObject;
    }

    private void Start()
    {
        //Setup controller event listeners for Menu button
        GetComponent<VRTK_ControllerEvents>().ButtonTwoReleased += new ControllerInteractionEventHandler(DoButtonTwoReleased);
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
