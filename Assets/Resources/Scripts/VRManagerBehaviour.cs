using UnityEngine;
using VRTK;

public class VRManagerBehaviour : MonoBehaviour {

    private VRTK_SDKManager manager;
    private GameObject leftController;
    private GameObject rightController;

    private ControllerBehaviour leftBehaviour;
    private ControllerBehaviour rightBehaviour;
    
    // Use this for initialization
    void Start () {
        GlobalVariables.vrManager = this;
        manager = gameObject.GetComponent<VRTK_SDKManager>();
        leftController = manager.scriptAliasLeftController;
        rightController = manager.scriptAliasRightController;

        leftBehaviour = leftController.GetComponent<ControllerBehaviour>();
        rightBehaviour = rightController.GetComponent<ControllerBehaviour>();
    }

    public void SetControllersSwap(bool swapped)
    {
        Debug.Log("Swap called with value: " + swapped);

        leftBehaviour.SetControllerMode(!swapped);
        rightBehaviour.SetControllerMode(swapped);
        
        //if(swapped)
        //{
        //    manager.scriptAliasLeftController = rightController;
        //    manager.scriptAliasRightController = leftController;

        //    GlobalVariables.radialMenu.transform.parent = rightController.transform;

        //} else
        //{
        //    manager.scriptAliasLeftController = leftController;
        //    manager.scriptAliasRightController = rightController;

        //    GlobalVariables.radialMenu.transform.parent = leftController.transform;
        //}

        //leftBehaviour.updateReferences();
        //rightBehaviour.updateReferences();
    }
}
