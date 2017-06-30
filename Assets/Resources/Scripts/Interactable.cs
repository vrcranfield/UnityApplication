using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {

    private bool interacting = false;
    private ControllerBehaviour attachedController;

	void Start () {
        foreach (Renderer r in GetComponentsInChildren<MeshRenderer>(true))
        {
            SetUpRigidBody(r.gameObject);
            SetUpCollider(r.gameObject);
        }
	}

    private void SetUpRigidBody(GameObject obj)
    {
        obj.AddComponent<Rigidbody>();
        obj.GetComponent<Rigidbody>().useGravity = false;
    }

    private void SetUpCollider(GameObject obj)
    {
        obj.AddComponent<BoxCollider>();
        obj.GetComponent<BoxCollider>().isTrigger = true;
    }

    private void FitColliderToChildren()
    {
        bool hasBounds = false;
        Bounds bounds = new Bounds(Vector3.zero, Vector3.zero);

        foreach(Renderer childRenderer in GetComponentsInChildren<MeshRenderer>(true)) {
            if (childRenderer != null)
            {
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
        }

        BoxCollider collider = GetComponent<BoxCollider>();
        collider.center = bounds.center - transform.position;
        collider.size = bounds.size;
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
            interacting = false;
        }
    }

    public bool IsInteracting()
    {
        return this.interacting;
    }

}
