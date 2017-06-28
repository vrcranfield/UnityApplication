using UnityEngine;
using VRTK;

public class RadialMenuBehaviour : MonoBehaviour {


    ParaUnity.FrameManager frameManager;
    VRTK_RadialMenu vrtkRadialMenu;

    bool isAnimationPlaying = false;

    void Awake()
    {
        GlobalVariables.ParaviewObjectLoadedCallbacks += new GlobalVariables.CallbackEventHandler(OnParaviewObjectLoaded);
        vrtkRadialMenu = GetComponentInChildren<VRTK_RadialMenu>();
    }

    public void OnButtonClick(int buttonId)
    {
        Debug.Log("Button " + buttonId + " Clicked!");
    }

    public void OnParaviewObjectLoaded(GameObject paraviewObj)
    {
        frameManager = GlobalVariables.frameContainer.GetComponent<ParaUnity.FrameManager>();
    }

    public void OnPlayPauseButtonClicked(int btnId)
    {

        VRTK_RadialMenu.RadialMenuButton btn = GetComponentInChildren<VRTK.VRTK_RadialMenu>().GetButton(btnId);

        if (frameManager != null)
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
            UpdatePlayPauseButtonIcon(btn);

        } else
        {
            // TODO disable button if nothing is loaded?
            Debug.Log("CLICK");
        }
    }
    
    private void UpdatePlayPauseButtonIcon(VRTK_RadialMenu.RadialMenuButton btn)
    {
        string spriteName = (isAnimationPlaying) ? "PauseButton" : "PlayButton";


        Sprite icon = Resources.Load<Sprite>("GUI/" + spriteName);

        btn.ButtonIcon = icon;
        vrtkRadialMenu.UpdateButtonSprites();
    }
}
