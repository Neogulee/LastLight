using CsvHelper;
using CsvHelper.Configuration.Attributes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;


public class ItemInfo: CsvInfo
{
    [Name("description")]
    public string description { get; set; }

    [Name("max level")]
    public int max_level { get; set; }
    
    [Name("damage")]
    public string damage_str { get; set; }

    [Name("cooldown")]
    public string cooldown_str { get; set; }

    [Name("is active")]
    [BooleanTrueValues("O")]
    [BooleanFalseValues("X", "")]
    public bool is_active { get; set; }
    public Sprite sprite;
    public List<int> damage, cooldown;
}

public interface IItemPool
{
    public List<string> get_names();
    public List<ItemInfo> get_infos();
    public ItemInfo get_info(string name);
    public ItemInfo get_info(System.Type type);
    public ItemInfo get_info<T>();
}

/// <summary>
/// ItemInfos' manager
/// Load Sprites from "Sprites/Items/<name>.png"
/// </summary>
public class ItemPool: IItemPool
{ 
    private Dictionary<string, ItemInfo> item_infos;
    public ItemPool()
    {
        item_infos = Locator.csv_parser.read<ItemInfo>("items");
        foreach (string name in item_infos.Keys)
        {
            item_infos[name].damage = parse_list(item_infos[name].damage_str);
            item_infos[name].cooldown = parse_list(item_infos[name].cooldown_str);
            item_infos[name].sprite = Resources.Load<Sprite>("Sprites/Items/" + name);
        }
    }

    private List<int> parse_list(string str)
    {
        string temp = Regex.Replace(str, @"\s+", "");
        return temp.Split(",")?.Select(int.Parse)?.ToList();
    }

    public List<string> get_names()
    {
        return item_infos.Keys.ToList();
    }

    public List<ItemInfo> get_infos()
    {
        return item_infos.Values.ToList();
    }

    public ItemInfo get_info(string name)
    {
        if (item_infos.ContainsKey(name))
            return item_infos[name];
        return null;
    }

    public ItemInfo get_info(System.Type type)
    {
        return get_info(type.Name);
    }

    public ItemInfo get_info<T>()
    {
        return get_info(typeof(T).Name);
    }
}