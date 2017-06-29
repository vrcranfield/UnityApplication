﻿using UnityEngine;
using VRTK;

public class RadialMenuBehaviour : MonoBehaviour {

    //AnimationManager animationManager;
    VRTK_RadialMenu vrtkRadialMenu;

    public enum ButtonsId
    {
        PlayPause = 3
    }
    
    void Awake()
    {
        Globals.ParaviewObjectLoadedCallbacks += new Globals.CallbackEventHandler(OnParaviewObjectLoaded);
        vrtkRadialMenu = GetComponentInChildren<VRTK_RadialMenu>();
    }

    void Start()
    {
        //animationManager = Globals.animation;
    }

    public void OnButtonClick(int buttonId)
    {
        Globals.logger.Log("Button " + buttonId + " clicked");
    }

    public void OnParaviewObjectLoaded(GameObject paraviewObj)
    {
        // If the object has an animation, enable the button
        if(Globals.animation.isAnimation())
        {
            UpdatePlayPauseButtonIcon();
        }
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
        //TODO implement
        Globals.logger.Log("Slice Button clicked");
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
