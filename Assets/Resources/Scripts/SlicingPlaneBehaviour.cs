using UnityEngine;

/**
 * Behavior script for the mock implementation of the slicing plane
 */
public class SlicingPlaneBehaviour : MonoBehaviour {

    /**
     * Attaches the plane to the controller's position and shows it
     */
    public void Show (ControllerBehaviour controller)
    {
        transform.position = controller.transform.position;
        transform.rotation = controller.transform.rotation;
        transform.parent = controller.transform;
        transform.localPosition += new Vector3(0, 0, 1.05f);
        gameObject.SetActive(true);
    }

    /**
     * Hides the plane
     */
    public void Hide ()
    {
        gameObject.SetActive(false);
        transform.parent = null;
    }

    /**
     * Returns current plane visibility
     */
    public bool IsShowing()
    {
        return gameObject.activeSelf;
    }
}
