using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[CreateAssetMenu(fileName = "Item" , menuName ="ItemData")]
public class ItemData : ScriptableObject
{
    public enum ActivePassiveType
    {
        ACTIVE,
        PASSIVE
    }
    [Header("# Main Info")]
    public ActivePassiveType activePassiveType;
    public int itemId;
    public string itemName;
    public string itemDesc;
    public string itemDesc_Plus;
    public int maxLevel;
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
}
