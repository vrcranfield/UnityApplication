using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlicingPlaneBehaviour : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        Globals.slicingPlane = this;
        gameObject.SetActive(false);
	}

    public void Show (ControllerBehaviour controller)
    {
        transform.position = controller.transform.position;
        transform.rotation = controller.transform.rotation;
        transform.parent = controller.transform;
        gameObject.SetActive(true);
    }

    public void Hide ()
    {
        gameObject.SetActive(false);
        transform.parent = null;
    }

    public bool IsShowing()
    {
        return gameObject.activeSelf;
    }
}
