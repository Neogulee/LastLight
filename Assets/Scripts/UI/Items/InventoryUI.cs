using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class InventoryUI : MonoBehaviour
{
    public GameObject slot_prefab = null;
    public float slot_size = 30.0f;
    private List<RectTransform> active_items = new(), passive_items = new();
    
    void Start()
    {
        IItemManager item_manager = Locator.item_manager;
        Locator.event_manager.register<ItemAddedEvent>(on_item_added);
        init_slots(active_items, item_manager.max_active_num, 0);
        init_slots(passive_items, item_manager.max_passive_num, 1);
    }

    private void init_slots(List<RectTransform> slots, int num, int row)
    {
        for (int i = 0; i < num; i++)
        {
            RectTransform slot_transform = Instantiate(slot_prefab, transform).GetComponent<RectTransform>();
            slot_transform.anchoredPosition = new Vector2((i + 0.5f) * slot_size, -slot_size * (row + 0.5f));
            slots.Add(slot_transform);
        }
    }

    private void on_item_added(IEventParam param)
    {
        IItemManager item_manager = Locator.item_manager;
        update_slots(active_items, item_manager.get_active_items().Cast<Item>().ToList());
        update_slots(passive_items, item_manager.get_passive_items().Cast<Item>().ToList());
    }

    private void update_slots(List<RectTransform> slots, List<Item> items)
    {
        for (int i = 0; i < items.Count; i++)
        {
            slots[i].GetComponentInChildren<Image>().sprite = items[i].info.sprite;
            slots[i].GetComponentInChildren<TMP_Text>().text = items[i].level.ToString();
        }
    }
}
