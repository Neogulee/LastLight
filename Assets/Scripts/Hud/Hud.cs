using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Hud : MonoBehaviour
{
    public enum InfoType
    {
        Exp,
        Level,
        Kill,
        Time,
        Hp
    }
    public InfoType type;

    Text myText;
    Slider mySlider;

    void Awake()
    {
        myText = GetComponent<Text>();
        mySlider = GetComponent<Slider>();
    }

    void LateUpdate()
    {
        switch (type)
        {
            case InfoType.Exp:
                float curExp = TempGameManager.instance.curExp;
                float maxExp = TempGameManager.instance.listExp[TempGameManager.instance.curLevel - 1];
                mySlider.value = curExp / maxExp;
                break;
            case InfoType.Level:
                break;
            case InfoType.Kill:
                break;
            case InfoType.Time:
                break;
            case InfoType.Hp:
                float curHp = TempGameManager.instance.curHp;
                float maxHp = TempGameManager.instance.maxHp;
                mySlider.value = curHp/maxHp;
                break;

        }    
    }
}
