using UnityEngine;

/**
 * Behavior for the floor of the room
 */
public class FloorBehaviour : MonoBehaviour {

    // Fields
    private Material startingMaterial;
    private Material defaultMaterial;

    /**
     * Called at object's initialization
     */
    void Awake () {
        startingMaterial = GetComponent<Renderer>().material;
        defaultMaterial = new Material(Shader.Find("Diffuse"));
    }
	
    /**
     * Enables or disables the custom material
     */
    public void SetMaterial(bool isStartingMaterial)
    {
        if (isStartingMaterial)
            GetComponent<Renderer>().material = startingMaterial;
        else
            GetComponent<Renderer>().material = defaultMaterial;
    }
}
