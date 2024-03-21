using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu(fileName = "Item" , menuName ="ItemData")]
public class ItemData : ScriptableObject
{

    public enum ItemType
    {
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

    public enum ActivePassiveType
    {
        Active,
        Passive
    }
    [Header("# Main Info")]
    public ItemType itemType;
    public ActivePassiveType activePassiveType;
    public int itemId;
    public string itemName;
    public string itemDesc;
    public string itemDesc_Plus;
    public int maxLevel;
    public int level = 0;
    public Sprite itemIcon;

    // 아래 내용은 나중에 안쓸 경우 삭제 예정 
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
