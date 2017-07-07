using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlicingManager : MonoBehaviour {

    SlicingPlaneBehaviour plane;

    void Awake()
    {
        Globals.slicingManager = this;
    }

	void Start () {
        plane = Globals.slicingPlane;	
	}

    public void ShowPlane()
    {
        ControllerBehaviour controller = Globals.controllers.getNonRadialController();
        plane.Show(controller);
    }

    public void HidePlane()
    {
        plane.Hide();
    }

    public void TogglePlane()
    {
        if (plane.IsShowing())
            HidePlane();
        else
            ShowPlane();
    }

    public bool IsShowing()
    {
        return plane.IsShowing();
    }

    public void Clip()
    {
        plane.Clip();
    }

    public void HighlightIntersection()
    {
        plane.HighlightIntersection();
    }

}
