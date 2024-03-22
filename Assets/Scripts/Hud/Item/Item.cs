using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class Item : MonoBehaviour
{
    public ItemData data;
    public int level { get; private set; } = 1;
    public ItemSlot itemSlot;
    public Weapon weapon;

    Image icon;
    Text textLevel;
    Text itemName;
    Text itemDesc;

    void Awake()
    {
        icon = GetComponentsInChildren<Image>()[1];
        icon.sprite = data.itemIcon;

        Text[] texts = GetComponentsInChildren<Text>();
        textLevel = texts[0];
        itemName = texts[1];
        itemDesc = texts[2];
        itemName.text = data.itemName;
        itemDesc.text = data.itemDesc;

        itemSlot = FindObjectOfType<ItemSlot>();
    }

    void OnEnable()
    {
        textLevel.text = "Lv." + level.ToString();
    }
    
    public void OnClick()
    {
        if (level == 1)
        {
            GameObject newWeapon = new GameObject();
            weapon = newWeapon.AddComponent<Weapon>();
            weapon.Init(data);
        }
        itemSlot.AddItem(this);
    }

    public void IncreaseLevel()
    {
        level = Mathf.Min(level + 1, data.maxLevel);
    }
}
