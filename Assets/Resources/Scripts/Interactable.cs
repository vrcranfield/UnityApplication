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

    private void FitColliderToChildren()
    {
        bool hasBounds = false;
        Bounds bounds = new Bounds(Vector3.zero, Vector3.zero);

        foreach(Renderer childRenderer in GetComponentsInChildren<MeshRenderer>(true)) {
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

        BoxCollider collider = GetComponent<BoxCollider>();
        collider.center = Vector3.Scale(bounds.center - transform.position, transform.localScale.Reciprocal());
        collider.size = Vector3.Scale(bounds.size, transform.localScale.Reciprocal());
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
