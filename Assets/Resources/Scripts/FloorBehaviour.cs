using UnityEngine;

public class FloorBehaviour : MonoBehaviour {

    Material startingMaterial;
    Material defaultMaterial;

	void Awake () {
        startingMaterial = GetComponent<Renderer>().material;
        defaultMaterial = new Material(Shader.Find("Diffuse"));
    }
	
    public void SetMaterial(bool isStartingMaterial)
    {
        if (isStartingMaterial)
            GetComponent<Renderer>().material = startingMaterial;
        else
            GetComponent<Renderer>().material = defaultMaterial;
    }
}
