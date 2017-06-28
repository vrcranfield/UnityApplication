using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoResize : MonoBehaviour
{
    [Tooltip("Activate or deactivate autoresizing")]
    public bool isAutoResizing = true;

    [Tooltip("Set target radius")]
    public float targetSize = 2;

    // Use this for initialization
    void Start () {
        if(isAutoResizing)
        {
            MeshRenderer ren = GetComponentInChildren<MeshRenderer>();

            // Resize
            float objectRadius = ren.bounds.size.magnitude;

            Debug.Log("Object radius: " + objectRadius);
            
            transform.localScale = targetSize * transform.localScale / objectRadius;

            // Recenter
            /*Vector3 objectCenter = ren.bounds.center;

            Debug.Log("Center: " + objectCenter.ToString());

            transform.position -= new Vector3(objectCenter.x, 0, objectCenter.z);*/
        }
    }

}
