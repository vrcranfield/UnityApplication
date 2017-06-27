using UnityEngine;

public class RadialMenuBehaviour : MonoBehaviour {

    ParaUnity.FrameShow paraviewObjFrameShow;

    void Awake()
    {
        GlobalVariables.Callback += new GlobalVariables.CallbackEventHandler(OnParaviewObjectLoaded);
    }

    public void OnButtonClick(int buttonId)
    {
        Debug.Log("Button " + buttonId + " Clicked!");
    }

    public void OnParaviewObjectLoaded(GameObject paraviewObj)
    {
        paraviewObjFrameShow = paraviewObj.GetComponent<ParaUnity.FrameShow>();
        if (paraviewObjFrameShow != null)
            GlobalVariables.staticMenu.Show();
        //Debug.Log("YAY: " + paraviewObjFrameShow);
    }
}
