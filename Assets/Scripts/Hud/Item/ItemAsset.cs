using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using static UnityEditor.IMGUI.Controls.PrimitiveBoundsHandle;
using static UnityEngine.Rendering.DebugUI.Table;

public class ItemAsset : MonoBehaviour
{
    [SerializeField] ItemData activeItem1;
    [SerializeField] ItemData activeItem2;
    [SerializeField] ItemData activeItem3;
    [SerializeField] ItemData activeItem4;
    [SerializeField] ItemData activeItem5;
    [SerializeField] ItemData activeItem6;

    [SerializeField] ItemData passiveItem1;
    [SerializeField] ItemData passiveItem2;
    [SerializeField] ItemData passiveItem3;
    [SerializeField] ItemData passiveItem4;
    [SerializeField] ItemData passiveItem5;
    [SerializeField] ItemData passiveItem6;

    public ItemData GetActiveData(ItemData.ItemType Type)
    {
        switch (Type)
        {
            default:
            case ItemData.ItemType.activeItem1:
                return activeItem1;
            case ItemData.ItemType.activeItem2:
                return activeItem2;
            case ItemData.ItemType.activeItem3:
                return activeItem3;
            case ItemData.ItemType.activeItem4:
                return activeItem4;
            case ItemData.ItemType.activeItem5:
                return activeItem5;
            case ItemData.ItemType.activeItem6:
                return activeItem6;
            // �̺κ��� ���߿� �и� ���� 
            case ItemData.ItemType.passiveItem1:
                return passiveItem1;
            case ItemData.ItemType.passiveItem2:
                return passiveItem2;
            case ItemData.ItemType.passiveItem3:
                return passiveItem3;
            case ItemData.ItemType.passiveItem4:
                return passiveItem4;
            case ItemData.ItemType.passiveItem5:
                return passiveItem5;
            case ItemData.ItemType.passiveItem6:
                return passiveItem6;

        }
    }

    public ItemData GetPassiveData(ItemData.ItemType type)
    {
        switch (type)
        {
            default:
            case ItemData.ItemType.passiveItem1:
                return passiveItem1;
            case ItemData.ItemType.passiveItem2:
                return passiveItem2;
            case ItemData.ItemType.passiveItem3:
                return passiveItem3;
            case ItemData.ItemType.passiveItem4:
                return passiveItem4;
            case ItemData.ItemType.passiveItem5:
                return passiveItem5;
            case ItemData.ItemType.passiveItem6:
                return passiveItem6;
        }
    }
}
