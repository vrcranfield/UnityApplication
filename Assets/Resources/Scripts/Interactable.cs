using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {

	void Start () {
        SetUpRigidBody();
        SetUpCollider();
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

        for (int i = 0; i < transform.childCount; ++i)
        {
            Renderer childRenderer = transform.GetChild(i).GetComponent<MeshRenderer>() ;
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
}
