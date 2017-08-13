using UnityEngine;

/**
 * Manager for the VR headset
 */
public class HeadsetManager : MonoBehaviour {

    /**
     * Called at object's initialization
     */
    void Awake () {
        Globals.headset = this;
	}

    /**
     * Returns the position at a certain distance in front of the camera
     */
    public Vector3 GetFrontPositionXZ(float distance)
    {
        // Get the XZ projection of the forward position of the headset camera
        Vector3 lookForwardXZ = new Vector3(transform.forward.x, 0, transform.forward.z);

        // Gets the point at `distance` meters in along lookForwardXZ
        return transform.position + distance * lookForwardXZ.normalized;
    }
	
}
