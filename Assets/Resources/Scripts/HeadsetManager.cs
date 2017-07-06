using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadsetManager : MonoBehaviour {

	void Awake () {
        Globals.headset = this;
	}

    public Vector3 GetFrontPositionXZ(float distance)
    {
        // Get the XZ projection of the forward position of the headset camera
        Vector3 lookForwardXZ = new Vector3(transform.forward.x, 0, transform.forward.z);

        // Gets the position at exactly "distance" meters in front of the headset, along the XZ plane.
        return transform.position + distance * lookForwardXZ.normalized;
    }
	
}
