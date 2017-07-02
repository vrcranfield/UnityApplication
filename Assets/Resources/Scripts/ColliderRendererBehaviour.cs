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
        if (Globals.paraviewObj == null)
            Hide();
        else if (IsShowing())
        {
            UpdatePosition();
        }
    }

    public void Show()
    {
        if (Globals.paraviewObj != null)
        {
            UpdatePosition();
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

    private void UpdatePosition()
    {
        Vector3 scaledCenter = Vector3.Scale(Globals.paraviewObj.GetComponent<BoxCollider>().center, Globals.paraviewObj.transform.localScale);
        Vector3 scaledSize = Vector3.Scale(Globals.paraviewObj.GetComponent<BoxCollider>().size, Globals.paraviewObj.transform.localScale);

        transform.SetPositionAndRotation(Globals.paraviewObj.transform.position, Globals.paraviewObj.transform.rotation);

        transform.Translate(scaledCenter);
        transform.localScale = scaledSize;
    }
}