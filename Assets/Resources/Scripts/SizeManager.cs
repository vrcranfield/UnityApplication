﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeManager : MonoBehaviour
{
    [Tooltip("Activate or deactivate autoresizing")]
    public bool isAutoResizing = true;

    [Tooltip("Set target radius")]
    public float targetSize = 2;

    [Tooltip("Scaling speed")]
    public float scaleSpeed = 1f; // Scales by a factor of scaleSpeed every second

    private GameObject obj;

    void Awake()
    {
        Globals.sizer = this;
        Globals.ParaviewObjectLoadedCallbacks += new Globals.CallbackEventHandler(OnParaviewObjectLoaded);
    }

    public void OnParaviewObjectLoaded(GameObject paraviewObj)
    {
        this.obj = paraviewObj;

        if (isAutoResizing)
            AutoResize();
    }

    public void ScaleUp()
    {
        if (obj != null)
            obj.transform.localScale *= (1 + scaleSpeed * Time.deltaTime);
    }

    public void ScaleDown()
    {
        if (obj != null)
            obj.transform.localScale *= (1 - scaleSpeed * Time.deltaTime);
    }

    public void AutoResize()
    {
        // Resize
        float objectRadius = obj.GetComponentInChildren<MeshRenderer>().bounds.size.magnitude;
        obj.transform.localScale = targetSize * obj.transform.localScale / objectRadius;
        Globals.logger.Log("Resizing object of radius: " + objectRadius);

        // Recenter
        /*Vector3 objectCenter = obj.GetComponent<MeshRenderer>().bounds.center;
        transform.position -= new Vector3(objectCenter.x, 0, objectCenter.z);*/
    }

}
