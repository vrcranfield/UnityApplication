using System;
using System.Collections;
using UnityEngine;

public class BoundingBoxBehaviour : MonoBehaviour
{
    private bool isEnabled = false;
    private float defaultAlpha;
    private Material material;

    private IEnumerator coroutine;
    private const float ANIMATION_DURATION = 0.125f; //in seconds

    private GameObject paraviewObj;

    void Awake()
    {
        Globals.boundingBox = this;
        material = GetComponent<Renderer>().material;
        defaultAlpha = material.color.a;

        Globals.ParaviewObjectLoadedCallbacks += OnParaviewObjectLoaded;
        Globals.ParaviewObjectUnloadedCallbacks += OnParaviewObjectUnloaded;

        // Hide and disable object
        SetAlpha(0.0f);
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (IsShowing())
        {
            UpdatePosition();
        }
    }

    private void OnParaviewObjectLoaded(GameObject paraviewObj)
    {
        this.paraviewObj = paraviewObj;
    }

    private void OnParaviewObjectUnloaded()
    {
        this.paraviewObj = null;

        if (IsShowing())
            Hide();
    }

    public void Show()
    {
        if (isEnabled && paraviewObj != null)
        {
            UpdatePosition();
            gameObject.SetActive(true);

            StopCoroutine(coroutine);
            coroutine = FadeIn();
            StartCoroutine(coroutine);
        }
        else if(Globals.paraviewObj == null)
        {
            Globals.logger.LogWarning("Cannot show ColliderBox without a ParaView Object");
        }
    }

    public void Hide()
    {
        StopCoroutine(coroutine);
        coroutine = FadeOut();
        StartCoroutine(coroutine);
    }

    public void SetEnabled(bool value)
    {
        isEnabled = value;
        if(!isEnabled)
        {
            Hide();
        }
    }

    public bool IsShowing()
    {
        return material.color.a > 0;
    }

    private void UpdatePosition()
    {
        Vector3 scaledCenter = Vector3.Scale(Globals.paraviewObj.GetComponent<BoxCollider>().center, Globals.paraviewObj.transform.localScale);
        Vector3 scaledSize = Vector3.Scale(Globals.paraviewObj.GetComponent<BoxCollider>().size, Globals.paraviewObj.transform.localScale);

        transform.SetPositionAndRotation(Globals.paraviewObj.transform.position, Globals.paraviewObj.transform.rotation);

        transform.Translate(scaledCenter);
        transform.localScale = scaledSize;
    }

    private IEnumerator FadeIn()
    {
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / ANIMATION_DURATION)
        {
            SetAlpha(Mathf.Lerp(0.0f, defaultAlpha, t));
            yield return null;
        }
    }

    private IEnumerator FadeOut()
    {
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / ANIMATION_DURATION)
        {
            SetAlpha(Mathf.Lerp(defaultAlpha, 0.0f, t));
            yield return null;
        }

        gameObject.SetActive(false);
    }

    private void SetAlpha(float alpha)
    {
        material.color = new Color(material.color.r, material.color.g, material.color.b, alpha);
    }
}