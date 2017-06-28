using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resizer : MonoBehaviour
{
    [Tooltip("Activate or deactivate autoresizing")]
    public bool isAutoResizing = true;

    [Tooltip("Set resizing step")]
    public float resizingStep = 0.1f;

    [Tooltip("Set target radius")]
    public float targetSize = 2;

    // Use this for initialization
    void Start () {
        if(isAutoResizing)
        {
            MeshRenderer ren = GetComponentInChildren<MeshRenderer>();

            // Resize
            float objectRadius = ren.bounds.size.magnitude;
            transform.localScale = targetSize * transform.localScale / objectRadius;
            Globals.logger.Log("Resizing object of radius: " + objectRadius);

            // Recenter
            /*Vector3 objectCenter = ren.bounds.center;
            transform.position -= new Vector3(objectCenter.x, 0, objectCenter.z);*/
        }
    }

}
