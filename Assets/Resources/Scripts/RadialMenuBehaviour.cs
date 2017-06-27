using UnityEngine;

public class RadialMenuBehaviour : MonoBehaviour {

    ParaUnity.FrameManager frameManager;

    void Awake()
    {
        GlobalVariables.ParaviewObjectLoadedCallbacks += new GlobalVariables.CallbackEventHandler(OnParaviewObjectLoaded);
    }

    public void OnButtonClick(int buttonId)
    {
        Debug.Log("Button " + buttonId + " Clicked!");
    }

    public void OnParaviewObjectLoaded(GameObject paraviewObj)
    {
        frameManager = GlobalVariables.frameContainer.GetComponent<ParaUnity.FrameManager>();
        Debug.Log("YAY: " + paraviewObjFrameShow);
    }
}
