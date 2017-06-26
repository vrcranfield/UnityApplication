using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorBehaviour : MonoBehaviour {

    Material startingMaterial;
    Material defaultMaterial;

	void Awake () {
        startingMaterial = GetComponent<Renderer>().material;
        defaultMaterial = new Material(Shader.Find("Diffuse"));
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetMaterial(bool isParquet)
    {
        if (isParquet)
            GetComponent<Renderer>().material = startingMaterial;
        else
            GetComponent<Renderer>().material = defaultMaterial;
    }
}
