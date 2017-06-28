using UnityEngine;
using VRTK;

public class RadialMenuBehaviour : MonoBehaviour {


    ParaUnity.FrameManager frameManager;
    VRTK_RadialMenu vrtkRadialMenu;

    bool isAnimationPlaying = false;

    void Awake()
    {
        Globals.ParaviewObjectLoadedCallbacks += new Globals.CallbackEventHandler(OnParaviewObjectLoaded);
        vrtkRadialMenu = GetComponentInChildren<VRTK_RadialMenu>();
    }

    public void OnButtonClick(int buttonId)
    {
        Globals.logger.Log("Button " + buttonId + " clicked");
    }

    public void OnParaviewObjectLoaded(GameObject paraviewObj)
    {
        frameManager = Globals.frameContainer.GetComponent<ParaUnity.FrameManager>();
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
            Globals.logger.LogError("No framemanager found!");
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
