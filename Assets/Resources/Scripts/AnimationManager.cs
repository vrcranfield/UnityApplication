using UnityEngine;

using System;
using System.Collections;


public class AnimationManager : MonoBehaviour
{
    public bool isPlaying = false;

    int currentFrame = 0;
	int delay = 0;

	const int DELAY_COUNT = 2;

    void Awake()
    {
        Globals.animation = this;
    }

    void Start()
    {
        ShowNextFrame();
    }

    void Update()
    {
        if (isPlaying)
        {
            if (delay >= DELAY_COUNT)
            {
                delay = 0;
                ShowNextFrame();
            }
            else
            {
                delay++;
            }
        }
    }

    public void Play()
    {
        isPlaying = true;
    }

    public void Pause()
    {
        isPlaying = false;
    }

    private void ShowNextFrame()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        transform.GetChild(currentFrame).gameObject.SetActive(true);
        currentFrame = (currentFrame + 1) % transform.childCount;
    }

    public bool isAnimation()
    {
        return transform.childCount > 1;
    }

}

