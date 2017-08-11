using UnityEngine;
using VRTK;

public class RadialMenuBehaviour : MonoBehaviour {

    VRTK_RadialMenu vrtkRadialMenu;

    public enum ButtonsId
    {
        PlayPause = 3
    }
    
    void Awake()
    {
        Globals.ParaviewObjectLoadedCallbacks += OnParaviewObjectLoaded;
        Globals.ParaviewObjectUnloadedCallbacks += OnParaviewObjectUnloaded;
        vrtkRadialMenu = GetComponentInChildren<VRTK_RadialMenu>();
    }

    public void OnButtonClick(int buttonId)
    {
        Globals.logger.Log("Button " + buttonId + " clicked");
    }

    public void OnParaviewObjectLoaded(GameObject paraviewObj)
    {
        UpdatePlayPauseButtonIcon();
    }

    public void OnParaviewObjectUnloaded()
    {
        UpdatePlayPauseButtonIcon();
    }

    public void OnScaleUpButtonHold()
    {
        if(Globals.paraviewObj != null)
            Globals.sizer.ScaleUp();
    }

    public void OnScaleDownButtonHold()
    {
        if (Globals.paraviewObj != null)
            Globals.sizer.ScaleDown();
    }

    public void OnSliceButtonClicked()
    {
        Globals.slicing.TogglePlane();
    }

    public void OnPlayPauseButtonClicked()
    {
        if (Globals.animation.isAnimation())
        {
            if (Globals.animation.isAnimationPlaying())
            {
                Globals.animation.Pause();
            }
            else
            {
                Globals.animation.Play();
            }
        } else if (!Globals.animation.isObjectLoaded())
        {
            Globals.logger.LogWarning("No Paraview Object loaded");
        } else if (Globals.animation.isObjectLoaded() && !Globals.animation.isAnimation())
        {
            Globals.logger.LogWarning("Object does not have an animation");
        }

        UpdatePlayPauseButtonIcon();
    }
    
    private void UpdatePlayPauseButtonIcon()
    {
        string spriteName;
        
        if(!Globals.animation.isAnimation())
        {
            spriteName = "PlayButtonDisabled";
        } else if(Globals.animation.isAnimationPlaying())
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
