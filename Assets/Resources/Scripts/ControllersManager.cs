using UnityEngine;
using VRTK;

public class ControllersManager : MonoBehaviour {

    private VRTK_SDKManager manager;

    private GameObject leftController;
    private GameObject rightController;

    private ControllerBehaviour leftBehaviour;
    private ControllerBehaviour rightBehaviour;
    
    void Awake()
    {
        // Register as global Variable
        Globals.controllers = this;

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
        leftBehaviour.SetControllerMode(!swapped);
        rightBehaviour.SetControllerMode(swapped);
    }
}
