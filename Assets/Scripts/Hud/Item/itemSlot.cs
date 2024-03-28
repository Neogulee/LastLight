using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    const int SLOTCOUNT = 6;

    [SerializeField] Transform activeItemSlotTemplate;
    [SerializeField] Transform activeItemSlotParent;
    [SerializeField] Transform passiveItemSlotTemplate;
    [SerializeField] Transform passiveItemSlotParent;


    // 레벨 여기서 관리 안해서 나중에 수정 예정

    List<Item> activeItems = new();
    List<Item> passiveItems = new();
    RectTransform[] activeItemSlots = new RectTransform[SLOTCOUNT];
    RectTransform[] passiveItemSlots = new RectTransform[SLOTCOUNT];
    void Start()
    {
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

    public void AddItem(Item item)
    {
        if(item.data.activePassiveType == ItemData.ActivePassiveType.ACTIVE)
        {
            if (!activeItems.Contains(item))
            {
                activeItems.Add(item);
            }
        }
        else
        {
            if (!passiveItems.Contains(item))
            {
                passiveItems.Add(item);
            }
        }
        ShowInventory();
    }

    public void ShowInventory()
    {
        int count = 0;

        //activeItemSlots[0].Find("Icon")
        foreach (Item item in activeItems)
        {
            activeItemSlots[count].Find("icon").GetComponent<Image>().sprite = item.data.GetSprite();
            activeItemSlots[count].Find("level").GetComponent<TMP_Text>().text = item.level.ToString();
            count++;
        }
        count = 0;
        foreach (Item item in passiveItems)
        {
            passiveItemSlots[count].Find("icon").GetComponent<Image>().sprite = item.data.GetSprite();
            passiveItemSlots[count].Find("level").GetComponent<TMP_Text>().text = item.level.ToString();
            count++;
        }
    }
}
