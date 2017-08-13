using System;
using System.Collections;
using UnityEngine;

/**
 * Behavior script for the Bounding Box surrounding the object
 */
public class BoundingBoxBehaviour : MonoBehaviour
{
    // Constants
    private const float ANIMATION_DURATION = 0.125f; // seconds

    // Fields
    private float defaultAlpha;
    private Material material;
    private IEnumerator coroutine;
    private GameObject paraviewObj;

    /**
     * Called at object's initialization
     */
    void Awake()
    {
        // Register self to globals
        Globals.boundingBox = this;

        // Save references
        material = GetComponent<Renderer>().material;
        defaultAlpha = material.color.a;

        // Register callback to ParaView Object Loaded event
        Globals.ParaviewObjectLoadedCallbacks += OnParaviewObjectLoaded;
        Globals.ParaviewObjectUnloadedCallbacks += OnParaviewObjectUnloaded;

        // By default, hide and disable object
        SetAlpha(0.0f);
        gameObject.SetActive(false);
    }

    /**
     * Called at every frame
     */
    void Update()
    {
        if (IsShowing())
        {
            UpdatePosition();
        }
    }

    /**
     * Callback to the ParaViewObjectLoaded event
     */
    private void OnParaviewObjectLoaded(GameObject paraviewObj)
    {
        this.paraviewObj = paraviewObj;
    }

    /**
     * Callback to the ParaViewObjectUnloaded event
     */
    private void OnParaviewObjectUnloaded()
    {
        this.paraviewObj = null;
        HideImmediate();
    }

    /**
     * Position and show the box with a fade in animation
     */
    public void Show()
    {
        if (gameObject.activeSelf && paraviewObj != null)
        {
            UpdatePosition();

            // Stop current animations
            if (coroutine != null)
                StopCoroutine(coroutine);

            // Start fading in
            coroutine = FadeIn();
            StartCoroutine(coroutine);
        }
    }

    /**
     * Hide the box with a fade out animation
     */
    public void Hide()
    {
        if (gameObject.activeSelf && paraviewObj != null)
        {
            // Stop current animations
            if (coroutine != null)
                StopCoroutine(coroutine);

            // Start fading out
            coroutine = FadeOut();
            StartCoroutine(coroutine);
        }
    }

    /**
     * Hide the object without animations
     */
    public void HideImmediate()
    {
        if (gameObject.activeSelf)
        {
            SetAlpha(0.0f);
        }
    }

    /**
     * Enable or disable the gameobject
     */
    public void ToggleActive(bool value)
    {
        gameObject.SetActive(value);
    }

    /**
     * Returns current object visibility
     */
    public bool IsShowing()
    {
        return material.color.a > 0;
    }

    /**
     * Updates scale, position and rotation to mirror the object's collider
     */
    private void UpdatePosition()
    {
        Vector3 scaledCenter = Vector3.Scale(Globals.paraviewObj.GetComponent<BoxCollider>().center, Globals.paraviewObj.transform.localScale);
        Vector3 scaledSize = Vector3.Scale(Globals.paraviewObj.GetComponent<BoxCollider>().size, Globals.paraviewObj.transform.localScale);

        transform.SetPositionAndRotation(Globals.paraviewObj.transform.position, Globals.paraviewObj.transform.rotation);

        transform.Translate(scaledCenter);
        transform.localScale = scaledSize;
    }

    /**
     * Couroutine for fade in animation
     */
    private IEnumerator FadeIn()
    {
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / ANIMATION_DURATION)
        {
            SetAlpha(Mathf.Lerp(0.0f, defaultAlpha, t));
            yield return null;
        }
    }

    /**
     * Coroutine for fade out animation
     */
    private IEnumerator FadeOut()
    {
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / ANIMATION_DURATION)
        {
            SetAlpha(Mathf.Lerp(defaultAlpha, 0.0f, t));
            yield return null;
        }
    }

    /**
     * Sets the object's transparency to custom value
     */
    private void SetAlpha(float alpha)
    {
        material.color = new Color(material.color.r, material.color.g, material.color.b, alpha);
    }
}