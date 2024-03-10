using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    const int MAX_LEVEL = 100;
    public ItemData data;
    public int level;
    public int MaxLevel;

    Image icon;
    Text textLevel;
    Text ItemName;
    Text ItemDesc;

    void Awake()
    {
        icon = GetComponentsInChildren<Image>()[1];
        icon.sprite = data.itemIcon;

        Text[] texts = GetComponentsInChildren<Text>();
        textLevel = texts[0];
        ItemName = texts[1];
        ItemDesc = texts[2];
        ItemName.text = data.itemName;
        ItemDesc.text = data.itemDesc;
    }
    
    void LateUpdate()
    {
        textLevel.text = "Lv." + (level + 1);
    }

    void OnEnable()
    {
        textLevel.text = "Lv." + (level + 1);
    }


    public void OnClick()
    {
        level++;
        
        // 테스트용 , 삭제 예정  
        if(level == MAX_LEVEL)
        {
            GetComponent<Button>().interactable = false;
        }

    }
}
