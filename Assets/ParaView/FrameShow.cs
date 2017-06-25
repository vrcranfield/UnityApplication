using UnityEngine;

using System;
using System.Collections;


namespace ParaUnity
{
    // I have a feeling that this is what loops the animations.
	public class FrameShow : MonoBehaviour
	{
        const bool IS_ANIMATION = false;

        int currentFrame = 0;

		int delay = 0;

		const int DELAY_COUNT = 1;

        void Start()
        {
            ShowNextFrame();
        }

        void Update()
        {
            if (IS_ANIMATION)
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

        private void ShowNextFrame()
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
            transform.GetChild(currentFrame).gameObject.SetActive(true);
            currentFrame = (currentFrame + 1) % transform.childCount;
        }
    }

}