using UnityEngine;
using VRTK;

/**
 * Behavior script for the Radial Menu
 */
public class RadialMenuBehaviour : MonoBehaviour {

    // Fields
    VRTK_RadialMenu vrtkRadialMenu;

    // Buttons id enum
    public enum ButtonsId
    {
        ScaleUp = 0,
        Slice = 1,
        ScaleDown = 2,
        PlayPause = 3
    }

    /**
     * Called at object's initialization
     */
    void Awake()
    {
        // Register callback to ParaView Object Loaded event
        Globals.ParaviewObjectLoadedCallbacks += OnParaviewObjectLoaded;
        Globals.ParaviewObjectUnloadedCallbacks += OnParaviewObjectUnloaded;

        // Save references
        vrtkRadialMenu = GetComponentInChildren<VRTK_RadialMenu>();
    }

    /**
     * Callback to the ParaViewObjectLoaded event
     */
    public void OnParaviewObjectLoaded(GameObject paraviewObj)
    {
        // If object has animation, enable play button
        UpdatePlayPauseButtonIcon();
    }

    /**
     * Callback to the ParaViewObjectUnloaded event
     */
    public void OnParaviewObjectUnloaded()
    {
        // Disable play button
        UpdatePlayPauseButtonIcon();
    }

    /**
     * Callback for holding down the scale up button
     */
    public void OnScaleUpButtonHold()
    {
        if(Globals.paraviewObj != null)
            Globals.sizer.ScaleUp();
    }

    /**
     * Callback for holding down the scale down button
     */
    public void OnScaleDownButtonHold()
    {
        if (Globals.paraviewObj != null)
            Globals.sizer.ScaleDown();
    }


    /**
     * Callback for clicking the slice button
     */
    public void OnSliceButtonClicked()
    {
        Globals.slicing.TogglePlane();
    }

    /**
     * Callback for clicking the play/pause button
     */
    public void OnPlayPauseButtonClicked()
    {
        // If the object has animation
        if (Globals.animation.isAnimation())
        {
            // Play or pause according to animation status
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

        // Update button icon
        UpdatePlayPauseButtonIcon();
    }
    
    /**
     * Updates the Play/Pause button icon according to current status
     */
    private void UpdatePlayPauseButtonIcon()
    {
        string spriteName;
        
        if(!Globals.animation.isAnimation())
        {
            // Disabled button icon
            spriteName = "PlayButtonDisabled";
        } else if(Globals.animation.isAnimationPlaying())
        {
            // Pause button icon
            spriteName = "PauseButton";
        } else
        {
            // Play button icon
            spriteName = "PlayButton";
        }

        // Load icon
        Sprite icon = Resources.Load<Sprite>("GUI/" + spriteName);

        // Change icon in radial menu
        vrtkRadialMenu.GetButton((int)ButtonsId.PlayPause).ButtonIcon = icon;
        vrtkRadialMenu.UpdateButtonSprites();
    }
}
