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
    public Vector2 slot_size = new Vector2(30.0f, 30.0f);
    private List<RectTransform> active_items = new(), passive_items = new();
    private List<Material> active_materials = new(), passive_materials = new();
    
    void Start()
    {
        IItemManager item_manager = Locator.item_manager;
        Locator.event_manager.register<ItemAddedEvent>(on_item_added);
        init_slots(active_items, active_materials, item_manager.max_active_num, 0);
        init_slots(passive_items, passive_materials, item_manager.max_passive_num, 1);
    }

    void update_cooldown()
    {
        List<ActiveItem> items = Locator.item_manager.get_active_items().ToList();
        for (int i = 0; i < items.Count; i++)
            active_materials[i].SetFloat("_CurrentTime", 1.0f - items[i].remain_cooldown / items[i].cooldown);
    }

    void FixedUpdate()
    {
        update_cooldown();
    }

    private void init_slots(List<RectTransform> slots, List<Material> materials, int num, int row)
    {
        for (int i = 0; i < num; i++)
        {
            RectTransform slot_transform = Instantiate(slot_prefab, transform).GetComponent<RectTransform>();
            slot_transform.anchoredPosition = new Vector2(i * slot_size.x, -slot_size.y * row);
            
            Image image = slot_transform.GetComponentsInChildren<Image>()[1];
            Material new_material = new Material(image.material);
            image.material = new_material;
            image.enabled = false;

            slots.Add(slot_transform);
            materials.Add(new_material);
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
            slots[i].GetComponentsInChildren<Image>()[1].sprite = items[i].info.sprite;
            slots[i].GetComponentInChildren<TMP_Text>().text = items[i].level.ToString();
            slots[i].GetComponentsInChildren<Image>()[1].enabled = true;
        }
    }
}
