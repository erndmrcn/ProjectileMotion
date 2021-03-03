using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenController : MonoBehaviour
{
    public Slider HmaxSlider;
    public Slider BallSpeedSlider;
    public Slider BouncinessSlider;
    public PlayerController PlayerController;
    public Text txt_Warning;
    public Animator ScreenAnimator;
    public void Initialize()
    {
        txt_Warning.gameObject.SetActive(false);
        InitializeSliders();
    }

    public void ShowWarning()
    {
        if (ScreenAnimator)
        {
            ScreenAnimator.Play("Intro", 0, 0);
        }
        txt_Warning.gameObject.SetActive(true);
    }

    private void InitializeSliders()
    {
        HmaxInitialize();
        BallSpeedInitialize();
        BouncinessInitialize();
    }

    private void BouncinessInitialize()
    {
        BouncinessSlider.minValue = 1.0f;
        BouncinessSlider.value = 7.0f;
        BouncinessSlider.maxValue = 10.0f;
    }

    private void BallSpeedInitialize()
    {
        BallSpeedSlider.minValue = 1.0f;
        BallSpeedSlider.value = 5.0f;
        BallSpeedSlider.maxValue = 10.0f;
    }

    private void HmaxInitialize()
    {
        HmaxSlider.minValue = 1.0f;
        HmaxSlider.value = 5.0f;
        HmaxSlider.maxValue = 90.0f;
    }

    public void HmaxUpdate()
    {
        PlayerController.Hmax = HmaxSlider.value;
    }
}
