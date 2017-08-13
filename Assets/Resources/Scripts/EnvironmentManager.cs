using System.Collections;
using UnityEngine;

/**
 * Manager for the Environment of the scene
 */
public class EnvironmentManager : MonoBehaviour {

    // Fields
    private GameObject[] walls;
    private FloorBehaviour floor;

    /**
     * Called at object's initialization
     */
    void Awake ()
    {
        // Register self to globals
        Globals.room = this;

        // Save references
        walls = GameObject.FindGameObjectsWithTag("Wall");
        floor = transform.GetComponentInChildren<FloorBehaviour>();
    }

    /**
     * Toggle walls and floor material visibility
     */
    public void ToggleShow(bool show)
    {
        foreach (GameObject wall in walls)
        {
            wall.SetActive(show);
        }
        floor.SetMaterial(show);
    }
}
