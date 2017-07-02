using UnityEngine;

public class ColliderRendererBehaviour : MonoBehaviour
{

    void Awake()
    {
        Globals.colliderBox = this;
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (gameObject.activeSelf)
        {
            transform.position = Vector3.Scale(Globals.paraviewObj.GetComponent<BoxCollider>().center, Globals.paraviewObj.transform.localScale) + Globals.paraviewObj.transform.position;
            transform.localScale = Vector3.Scale(Globals.paraviewObj.GetComponent<BoxCollider>().size, Globals.paraviewObj.transform.localScale);
            transform.rotation = Globals.paraviewObj.transform.rotation;
        }
    }

    public void Show()
    {
        if (Globals.paraviewObj != null)
        {
            gameObject.SetActive(true);
        }
        else
        {
            Globals.logger.LogWarning("Cannot show ColliderBox without a ParaView Object");
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public bool IsShowing()
    {
        return gameObject.activeSelf;
    }
}