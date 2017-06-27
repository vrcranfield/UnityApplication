using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoResize : MonoBehaviour
{
    [Tooltip("Activate or deactivate autoresizing")]
    public bool isAutoResizing = true;
    float objectRadius;

    // Use this for initialization
    void Start () {
        if(isAutoResizing)
        {
            float targetSize = 1;

            //objectRadius = GetComponent<Renderer>().bounds.size.magnitude;
            //transform.localScale = targetSize * transform.localScale / objectRadius;
        }
    }
	
	// Update is called once per frame
	void Update () {
		if(GlobalVariables.overlayText != null)
        {
            GlobalVariables.overlayText.SetText(objectRadius + "");
        }
	}

}
