using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBehaviour : MonoBehaviour {

    GameObject[] walls;
    FloorBehaviour floor;

    void Awake ()
    {
        GlobalVariables.room = this;
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
