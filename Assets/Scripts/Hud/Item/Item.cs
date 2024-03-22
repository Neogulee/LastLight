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
    public itemSlot ItemSlot;

    public Weapon weapon;

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
        data.level = 0;
    }
    
    void LateUpdate()
    {
        //textLevel.text = "Lv." + (data.level);
    }

    void OnEnable()
    {
        textLevel.text = "Lv." + (data.level+1);
    }

    
    public void OnClick()
    {
        Time.timeScale = 1f;
        if (data.activePassiveType == ItemData.ActivePassiveType.Active)
        {
            // 레벨이 1일 경우 생성  
            switch (data.itemType)
            {
                case ItemData.ItemType.activeItem1:
                    if (data.level == 0)
                    {
                        GameObject newWeapon = new GameObject();
                        weapon = newWeapon.AddComponent<Weapon>();
                        weapon.Init(data);
                    }
                    else
                    {

                    }
                    break;
            }
            ItemSlot.AddActiveItem(data.itemType);
        }
        else if (data.activePassiveType == ItemData.ActivePassiveType.Passive)
        {
            ItemSlot.AddPassiveItem(data.itemType);
        }

    }
}
