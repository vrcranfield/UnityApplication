using UnityEngine;
using VRTK;

public class ControllerBehaviour : MonoBehaviour
{
    private GameObject menu;
    private GameObject headset;

    private void Start()
    {
        if (GetComponent<VRTK_ControllerEvents>() == null)
        {
            VRTK_Logger.Error(VRTK_Logger.GetCommonMessage(VRTK_Logger.CommonMessageKeys.REQUIRED_COMPONENT_MISSING_FROM_GAMEOBJECT, "VRTK_ControllerEvents_ListenerExample", "VRTK_ControllerEvents", "the same"));
            return;
        }

        if (GlobalVariables.menu != null)
            menu = GlobalVariables.menu;

        headset = GameObject.FindGameObjectWithTag("Headset");

        //Setup controller event listeners for Menu button
        GetComponent<VRTK_ControllerEvents>().ButtonTwoReleased += new ControllerInteractionEventHandler(DoButtonTwoReleased);
    }

    private void DoButtonTwoReleased(object sender, ControllerInteractionEventArgs e)
    {
        if (menu == null)
            menu = GlobalVariables.menu;

        if(menu.activeSelf)
        {
            Debug.Log("Hiding Menu");
            menu.SetActive(false);
        } else
        {
            Debug.Log("Showing Menu");

            Vector3 headsetFrontPosition = headset.transform.position + 2.5f * headset.transform.forward.normalized;

            menu.transform.position = new Vector3(headsetFrontPosition.x, menu.transform.position.y, headsetFrontPosition.z);
            menu.transform.rotation = Quaternion.LookRotation(menu.transform.position - headset.transform.position);
            menu.SetActive(true);
        }
    }

}
