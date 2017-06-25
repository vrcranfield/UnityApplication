using UnityEngine;
using VRTK;

public class ControllerBehaviour : MonoBehaviour
{
    private StaticMenuBehaviour staticMenu;
    private GameObject radialMenu;

    public bool isRadialMenuController;

    private void Awake()
    {
        radialMenu = transform.Find("RadialMenu").gameObject;
    }

    private void Start()
    {
        staticMenu = GlobalVariables.staticMenu;

        //Setup controller event listeners for Menu button
        GetComponent<VRTK_ControllerEvents>().ButtonTwoReleased += new ControllerInteractionEventHandler(DoButtonTwoReleased);
    }

    private void DoButtonTwoReleased(object sender, ControllerInteractionEventArgs e)
    {
        if(staticMenu.isShown())
        {
            Debug.Log("Hiding Menu");
            staticMenu.Hide();
        } else
        {
            Debug.Log("Showing Menu");
            staticMenu.Show();
        }
    }

    public void SetControllerMode(bool isRadialMenu)
    {
        radialMenu.SetActive(isRadialMenu);
        isRadialMenuController = isRadialMenu;
    }
}
