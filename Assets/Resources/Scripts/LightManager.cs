using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    void Awake()
    {
        Globals.lighting = this;
    }

    public void SetIntensity(float value)
    {
        RenderSettings.ambientLight = new Color(value, value, value, 1);
    }
}
