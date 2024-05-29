using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public interface IItemSelector
{
    public void show_selection();
    public void on_selected(ItemInfo info);
    public void set_active(bool value);
}

public class ItemSelector : MonoBehaviour, IItemSelector
{
    public List<ItemSelectionOption> options = new();
    private int current_cnt = 0;
    private bool is_active = false;
    
    void Awake()
    {
        current_cnt = options.Count;
        set_active(false);
        Locator.event_manager.register<OnInputItemSelection>(on_input_item_selection);
    }

    public void show_selection()
    {
        IItemManager item_manager = Locator.item_manager;
        IItemPool item_pool = Locator.item_pool;
        
        List<Item> items = item_manager.get_items();        
        List<ItemInfo> infos_pool = item_pool.get_infos();

        HashSet<ItemInfo> infos = new();
        if (item_manager.active_num < item_manager.max_active_num)
            foreach (ItemInfo info in infos_pool)
                if (info.is_active)
                    infos.Add(info);
        
        if (item_manager.passive_num < item_manager.max_passive_num)
            foreach (ItemInfo info in infos_pool)
                if (!info.is_active)
                    infos.Add(info);
                    
        foreach (Item item in items)
            if (item.level < item.info.max_level)
                infos.Add(item.info);

        var random = new System.Random();
        List<ItemInfo> selected = infos.OrderBy(x => random.Next()).Take(options.Count).ToList();
        current_cnt = Mathf.Min(options.Count, selected.Count);
        if (current_cnt == 0)
            return;
            
        for (int i = 0; i < current_cnt; i++)
        {
            ItemInfo info = selected[i];
            int level = item_manager.get_level(info) + 1;
            options[i].init(this, selected[i], level);
        }
        set_active(true);
        Locator.pause_controller.pause();
    }

    public void on_input_item_selection(IEventParam param)
    {
        if (!is_active)
            return;
            
        var args = (OnInputItemSelection)param;
        on_selected(options[args.idx].info);
    }

    public void on_selected(ItemInfo info)
    {
        set_active(false);
        Locator.pause_controller.unpause();
        Locator.item_manager.add_item(info);
    }

    public void set_active(bool value)
    {
        is_active = value;
        for (int i = 0; i < current_cnt; i++)
        {
            var option = options[i];
            option.gameObject.SetActive(value);
        }
    }
}
