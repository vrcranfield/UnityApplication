using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingOverlayBehaviour : MonoBehaviour {

    void Awake ()
    {
        GlobalVariables.ParaviewObjectLoadedCallbacks += new GlobalVariables.CallbackEventHandler(OnParaviewObjectLoaded);
    }

	// Use this for initialization
	void OnParaviewObjectLoaded(GameObject paraviewObject)
    {
        gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
