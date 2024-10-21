using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;


public class BritnessSlinder : MonoBehaviour
{
    public Slider slider;
    public Volume volume;
    void Start()
    {
        slider = GetComponentInChildren<Slider>();
        slider.onValueChanged.AddListener(ChangeIntensity);
    }

    void ChangeIntensity(float value)
    {
        ColorAdjustments adjustments;
        volume.profile.TryGet(out adjustments);
        adjustments.postExposure.value = value;
    }
}
