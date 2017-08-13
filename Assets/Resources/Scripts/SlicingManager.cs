using UnityEngine;

/**
 * Manager for the slicing feature of the ParaView object
 */
public class SlicingManager : MonoBehaviour {

    // Fields
    private SlicingPlaneBehaviour plane;

    /**
     * Called at object's initialization
     */
    void Awake()
    {
        // Register self to globals
        Globals.slicing = this;

        // Save references
        plane = GetComponentInChildren<SlicingPlaneBehaviour>();

        // Plane is hidden by default
        HidePlane();
    }

    /**
     * Attaches the slicing plan to a controller and shows it
     */
    public void ShowPlane()
    {
        ControllerBehaviour controller = Globals.controllers.getNonRadialController();
        plane.Show(controller);
    }

    /**
     * Hides the slicing plane
     */
    public void HidePlane()
    {
        plane.Hide();
    }

    /**
     * Toggles slicing plane visibility
     */
    public void TogglePlane()
    {
        if (plane.IsShowing())
            HidePlane();
        else
            ShowPlane();
    }
}
