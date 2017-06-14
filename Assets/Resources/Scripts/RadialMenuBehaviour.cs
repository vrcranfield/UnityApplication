using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialMenuBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnButtonClick(int buttonId)
    {
        Debug.Log("Button " + buttonId + " Clicked!");
    }
}
