using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class itemSlot : MonoBehaviour
{
    [SerializeField] Transform activeItemSlotTemplate;
    [SerializeField] Transform activeItemSlotParent;
    [SerializeField] Transform passiveItemSlotTemplate;
    [SerializeField] Transform passiveItemSlotParent;

    [SerializeField] ItemAsset itemAsset;


    static Dictionary<ItemData.ItemType, int> activeItemData;
    static Dictionary<ItemData.ItemType, int> passiveItemData;
    RectTransform[] activeItemSlots = new RectTransform[6];
    RectTransform[] passiveItemSlots = new RectTransform[6];
    const int SLOTCOUNT = 6;
    // Start is called before the first frame update
    void Start()
    {
        activeItemData = new Dictionary<ItemData.ItemType, int>();

        for(int i = 0; i < SLOTCOUNT; i++)
        {
            activeItemSlots[i] = Instantiate(activeItemSlotTemplate, activeItemSlotParent).GetComponent<RectTransform>();
            activeItemSlots[i].anchoredPosition = new Vector2(i * 30, 0f);
            activeItemSlots[i].gameObject.SetActive(true);

            passiveItemSlots[i] = Instantiate(passiveItemSlotTemplate, passiveItemSlotParent).GetComponent<RectTransform>();
            passiveItemSlots[i].anchoredPosition = new Vector2(i * 30, 0f);
            passiveItemSlots[i].gameObject.SetActive(true);
        }
        ShowInventory();
    }


    public void AddActiveItem(ItemData.ItemType itemType)
    {
        ItemData item;

        switch(itemType)
        {
            default:
            case ItemData.ItemType.activeItem1:
                item = itemAsset.GetActiveData(ItemData.ItemType.activeItem1);
                break;
            case ItemData.ItemType.activeItem2:
                item = itemAsset.GetActiveData(ItemData.ItemType.activeItem2);
                break;
            case ItemData.ItemType.activeItem3:
                item = itemAsset.GetActiveData(ItemData.ItemType.activeItem3);
                break;
            case ItemData.ItemType.activeItem4:
                item = itemAsset.GetActiveData(ItemData.ItemType.activeItem4);
                break;
            case ItemData.ItemType.activeItem5:
                item = itemAsset.GetActiveData(ItemData.ItemType.activeItem5);
                break;
            case ItemData.ItemType.activeItem6:
                item = itemAsset.GetActiveData(ItemData.ItemType.activeItem6);
                break;
            case ItemData.ItemType.passiveItem1:
                item = itemAsset.GetPassiveData(ItemData.ItemType.passiveItem1);
                break;
            case ItemData.ItemType.passiveItem2:
                item = itemAsset.GetPassiveData(ItemData.ItemType.passiveItem2);
                break;
            case ItemData.ItemType.passiveItem3:
                item = itemAsset.GetPassiveData(ItemData.ItemType.passiveItem3);
                break;
            case ItemData.ItemType.passiveItem4:
                item = itemAsset.GetPassiveData(ItemData.ItemType.passiveItem4);
                break;
            case ItemData.ItemType.passiveItem5:
                item = itemAsset.GetPassiveData(ItemData.ItemType.passiveItem5);
                break;
            case ItemData.ItemType.passiveItem6:         
                item = itemAsset.GetPassiveData(ItemData.ItemType.passiveItem6);
                break;
        }

        if (activeItemData.ContainsKey(itemType))
        {
            item.IncreseLevel();

        }
        else
        {
            activeItemData.Add(itemType, 1);
        }
        ShowInventory();
    }

    public void AddPassiveItem(ItemData.ItemType itemType)
    {

    }
    public void ShowInventory()
    {
        int count = 0;

        //activeItemSlots[0].Find("Icon")
        foreach (ItemData.ItemType itemType in activeItemData.Keys)
        {
            Debug.Log(count);
            activeItemSlots[count].Find("icon").GetComponent<Image>().sprite = itemAsset.GetActiveData(itemType).GetSprite();
            activeItemSlots[count].Find("level").GetComponent<TMP_Text>().text = itemAsset.GetActiveData(itemType).GetLevel().ToString();
            count++;
        } 
    }
}
