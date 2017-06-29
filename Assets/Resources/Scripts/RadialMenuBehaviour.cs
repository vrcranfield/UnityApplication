using UnityEngine;
using VRTK;

public class RadialMenuBehaviour : MonoBehaviour {

    AnimationManager animationManager;
    VRTK_RadialMenu vrtkRadialMenu;

    public enum ButtonsId
    {
        PlayPause = 3
    }

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
        animationManager = Globals.animation;
        
        if(animationManager.isAnimation())
        {
            // TODO activate button
            //vrtkRadialMenu.GetButton((int)ButtonsId.PlayPause).
        }
    }

    public void OnPlayPauseButtonClicked()
    {
        if (animationManager != null && animationManager.isAnimation())
        {
            if (isAnimationPlaying)
            {
                animationManager.Pause();
            }
            else
            {
                animationManager.Play();
            }

            isAnimationPlaying = animationManager.isPlaying;

        } else if (animationManager == null)
        {
            Globals.logger.LogWarning("No Paraview Object loaded");
        } else if (!animationManager.isAnimation())
        {
            Globals.logger.LogWarning("Object does not have an animation");
        }

        UpdatePlayPauseButtonIcon();
    }
    
    private void UpdatePlayPauseButtonIcon()
    {
        string spriteName;
        
        if(!animationManager.isAnimation())
        {
            spriteName = "PlayButtonDisabled";
        } else if(isAnimationPlaying)
        {
            spriteName = "PauseButton";
        } else
        {
            spriteName = "PlayButton";
        }

        Sprite icon = Resources.Load<Sprite>("GUI/" + spriteName);

        vrtkRadialMenu.GetButton((int)ButtonsId.PlayPause).ButtonIcon = icon;
        vrtkRadialMenu.UpdateButtonSprites();
    }
}
