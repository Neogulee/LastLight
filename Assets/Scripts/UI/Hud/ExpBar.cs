using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EXPBar : MonoBehaviour
{
    public LevelManager level_manager = null;
    Slider exp_slider;

    void Awake()
    {
        exp_slider = GetComponent<Slider>();
    }

    void LateUpdate()
    {
        exp_slider.value = (float)level_manager.exp / level_manager.exp_to_next_level + 0.01f;
    }
}
