using UnityEngine;
using VRTK;

public class VRManagerBehaviour : MonoBehaviour {

    private VRTK_SDKManager manager;
    private GameObject leftController;
    private GameObject rightController;

    private ControllerBehaviour leftBehaviour;
    private ControllerBehaviour rightBehaviour;
    
    void Awake()
    {
        // Register as Global Variable
        GlobalVariables.vrManager = this;

        // Save references
        manager = gameObject.GetComponent<VRTK_SDKManager>();

        leftController = manager.scriptAliasLeftController;
        rightController = manager.scriptAliasRightController;

        leftBehaviour = leftController.GetComponent<ControllerBehaviour>();
        rightBehaviour = rightController.GetComponent<ControllerBehaviour>();
    }

    void Start () {
        // Initialize Controllers
        SetControllersSwap(false);
    }

    public void SetControllersSwap(bool swapped)
    {
        Debug.Log("Swap called with value: " + swapped);

        leftBehaviour.SetControllerMode(!swapped);
        rightBehaviour.SetControllerMode(swapped);
    }
}
