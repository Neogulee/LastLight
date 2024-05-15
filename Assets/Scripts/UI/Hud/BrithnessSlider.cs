using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;

public class BrithnessSlinder : MonoBehaviour
{
    public Slider slider;
    // Light2D light2D
    public Light2D _light;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponentInChildren<Slider>();
        _light = GetComponentInChildren<Light2D>();
        slider.onValueChanged.AddListener(ChangeIntensity);
    }
    void ChangeIntensity(float value)
    {

        _light.intensity = value;
    }

}
