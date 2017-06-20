using UnityEngine;
using VRTK;

public class VRManagerBehaviour : MonoBehaviour {

    private VRTK_SDKManager manager;
    private GameObject leftController;
    private GameObject rightController;

    // Use this for initialization
    void Start () {
        GlobalVariables.vrManager = this;
        manager = gameObject.GetComponent<VRTK_SDKManager>();
        leftController = manager.scriptAliasLeftController;
        rightController = manager.scriptAliasRightController;
	}

    public void SetControllersSwap(bool swapped)
    {
        if(swapped)
        {
            manager.scriptAliasLeftController = rightController;
            manager.scriptAliasRightController = leftController;

        } else
        {
            manager.scriptAliasLeftController = leftController;
            manager.scriptAliasRightController = rightController;
        }
    }
}
