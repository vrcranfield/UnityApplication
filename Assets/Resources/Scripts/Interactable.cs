using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {

    private bool interacting = false;
    private ControllerBehaviour attachedController;

	void Awake () {
        SetUpCollider();
        SetUpRigidBody();
    }

    void Start ()
    {
        Globals.animation.NextFrameLoadedCallbacks += new AnimationManager.CallbackEventHandler(FitColliderToChildren);
    }

    private void SetUpRigidBody()
    {
        gameObject.AddComponent<Rigidbody>();
        gameObject.GetComponent<Rigidbody>().useGravity = false;
    }

    private void SetUpCollider()
    {
        gameObject.AddComponent<BoxCollider>();
        gameObject.GetComponent<BoxCollider>().isTrigger = true;
        FitColliderToChildren();
    }

    public void FitColliderToChildren()
    {
        // Temporarily reset the parent's orientation, since the bounds will 
        // be given in world coordinates but the collider is in local coordinates.
        Quaternion oldRotation = transform.rotation;
        transform.rotation = Quaternion.identity;

        bool hasBounds = false;
        Bounds bounds = new Bounds(Vector3.zero, Vector3.zero);

        // Iterate over all active children. Normally, it should be 2 MeshRenderer's (front and back) active
        // for every frame. Since there is only one frame active in any given moment, it should be only two.
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

        // Set collider center and size (with conversion from global to local
        BoxCollider collider = GetComponent<BoxCollider>();
        collider.center = Vector3.Scale(bounds.center - transform.position, transform.localScale.Reciprocal());
        collider.size = Vector3.Scale(bounds.size, transform.localScale.Reciprocal());

        // Reset the objects orientation
        transform.rotation = oldRotation;
    }

    public void OnBeginInteraction(ControllerBehaviour controller)
    {
        attachedController = controller;
        this.transform.parent = controller.transform;
        interacting = true;
    }

    public void OnEndInteraction(ControllerBehaviour controller)
    {
        if (controller == attachedController)
        {
            attachedController = null;
            this.transform.parent = null;
            interacting = false;
        }
    }

    public bool IsInteracting()
    {
        return this.interacting;
    }

}
