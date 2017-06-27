using UnityEngine;

public class RadialMenuBehaviour : MonoBehaviour {

    ParaUnity.FrameManager frameManager;

    bool isAnimationPlaying = false;

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
        //Debug.Log("YAY: " + paraviewObjFrameShow);

        if (frameManager != null)
            GlobalVariables.staticMenu.Show();
    }

    public void OnPlayPauseButtonClicked()
    {
        if(isAnimationPlaying)
        {
            frameManager.Pause();
        } else
        {
            frameManager.Play();
        }

        isAnimationPlaying = frameManager.isPlaying;
    }
}
