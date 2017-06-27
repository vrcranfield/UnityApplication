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
    }

    public void OnPlayPauseButtonClicked()
    {
        if(frameManager != null)
        {
            if (isAnimationPlaying)
            {
                frameManager.Pause();
            }
            else
            {
                frameManager.Play();
            }

            isAnimationPlaying = frameManager.isPlaying;
        } else
        {
            // TODO disable button if nothing is loaded?
            Debug.Log("CLICK");
        }
    }
}
