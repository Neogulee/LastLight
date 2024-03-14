using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Item" , menuName ="ItemData")]
public class ItemData : ScriptableObject
{

    public enum ItemType
    {
        Melee,
        Range,
        Glove,
        Shoe,
        Heal,
        activeItem1,
        activeItem2,
        activeItem3,
        activeItem4,
        activeItem5,
        activeItem6,
        passiveItem1,
        passiveItem2,
        passiveItem3,
        passiveItem4,
        passiveItem5,
        passiveItem6,
    }
    [Header("# Main Info")]
    public ItemType itemType;
    public int itemId;
    public string itemName;
    public string itemDesc;
    public string itemDesc_Plus;
    public int maxLevel;
    public int level = 0;
    public Sprite itemIcon;

    // �Ʒ� ������ ���߿� �Ⱦ� ��� ���� ���� 
    [Header("# Level Data")]
    public float baseDamage;
    public int baseCount;
    public float[] damages;
    public int[] counts;
    [Header("# Weapon")]
    public GameObject projectile;

    public Sprite GetSprite()
    {
        return itemIcon;
    }

    public int GetLevel()
    {
        return level;
    }

    public void IncreseLevel()
    {
        level++;
    }

}
