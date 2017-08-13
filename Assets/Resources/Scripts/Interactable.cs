using UnityEngine;

/**
 * Behavior for objects that allow controller interaction
 */
public class Interactable : MonoBehaviour {

    // Fields
    private bool interacting = false;
    private ControllerBehaviour attachedController;

    /**
     * Called at object's initialization
     */
    void Awake () {
        // Add a collider and a rigid body to object
        SetUpCollider();
        SetUpRigidBody();
    }

    /**
     * Called at object's initialization, after all Awakes.
     */
    void Start ()
    {
        // Register callback to NextFrameLoaded event
        Globals.animation.NextFrameLoadedCallbacks += FitColliderToChildren;
    }

    /**
     * Adds a gravity-less rigid body to the object
     */
    private void SetUpRigidBody()
    {
        gameObject.AddComponent<Rigidbody>();
        gameObject.GetComponent<Rigidbody>().useGravity = false;
    }

    /**
     * Adds a collider to the object
     */
    private void SetUpCollider()
    {
        gameObject.AddComponent<BoxCollider>();
        gameObject.GetComponent<BoxCollider>().isTrigger = true;

        // Resizes the collider according to active children
        FitColliderToChildren();
    }

    /**
     * Resizes the collider to fit only active children
     */
    public void FitColliderToChildren()
    {
        // Temporarily reset the parent's orientation, since the bounds will 
        // be given in world coordinates but the collider is in local coordinates.
        Quaternion oldRotation = transform.rotation;
        transform.rotation = Quaternion.identity;

        bool hasBounds = false;
        Bounds bounds = new Bounds(Vector3.zero, Vector3.zero);

        // Iterate over all active children. ParaView objects have 2 active object per frame,
        // and one frame active at any time.
        foreach(Renderer childRenderer in GetComponentsInChildren<MeshRenderer>()) {
            if (hasBounds)
            {
                bounds.Encapsulate(childRenderer.bounds);
            }
            else
            {
                bounds = childRenderer.bounds;
                hasBounds = true;
            }
        }

        // Set collider center and size (with conversion from global to local)
        BoxCollider collider = GetComponent<BoxCollider>();
        collider.center = Vector3.Scale(bounds.center - transform.position, transform.localScale.Reciprocal());
        collider.size = Vector3.Scale(bounds.size, transform.localScale.Reciprocal());

        // Reset the objects orientation
        transform.rotation = oldRotation;
    }

    /**
     * Begin interaction with a controller
     */
    public void OnBeginInteraction(ControllerBehaviour controller)
    {
        // Save reference to controller
        attachedController = controller;

        // Follow controller's position and orientation
        this.transform.parent = controller.transform;

        interacting = true;
    }

    /**
     * End interaction with a controller
     */
    public void OnEndInteraction(ControllerBehaviour controller)
    {
        // React only if it's the controller the object is following
        if (controller == attachedController)
        {
            // Delete reference and unparent
            attachedController = null;
            this.transform.parent = null;
            interacting = false;
        }
    }

    /**
     * Returns current interaction status
     */
    public bool IsInteracting()
    {
        return this.interacting;
    }

    /**
     * Before the object is destroyed, unregister callbacks
     */
    void OnDestroy()
    {
        Globals.animation.NextFrameLoadedCallbacks -= FitColliderToChildren;
    }

}
