using System.Collections;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour {

    GameObject[] walls;
    FloorBehaviour floor;

    void Awake ()
    {
        Globals.room = this;
        walls = GameObject.FindGameObjectsWithTag("Wall");
        floor = transform.GetComponentInChildren<FloorBehaviour>();
    }

    public void ToggleShow(bool show)
    {
        foreach (GameObject wall in walls)
        {
            wall.SetActive(show);
        }
        floor.SetMaterial(show);
    }
}
