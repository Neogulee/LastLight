using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Option : MonoBehaviour
{
    public Vector3 originalScale;

    private void Awake()
    {

        Locator.event_manager.register<OptionEvent>(ToggleScale);
        
    }
    void Start()
    {
        originalScale = new Vector3(0.3950352f, 0.56656f, 1f);
        SetScale(Vector3.zero); 
    }


    void ToggleScale(IEventParam param)
    {
        Debug.Log("ToggleScale");
        if (transform.localScale == Vector3.zero)
        {
            Locator.pause_controller.pause();
            SetScale(originalScale); 
        }
        else
        {
            Locator.pause_controller.unpause();
            SetScale(Vector3.zero); 
        }
    }
    void ToggleScale()
    {
        if (transform.localScale == Vector3.zero)
        {
            Locator.pause_controller.pause();
            SetScale(originalScale);
        }
        else
        {
            Locator.pause_controller.unpause();
            SetScale(Vector3.zero);
        }
    }

    void SetScale(Vector3 scale)
    {
        transform.localScale = scale;
    }
    public void ButtonClicked()
    {
        ToggleScale();
    }
}
