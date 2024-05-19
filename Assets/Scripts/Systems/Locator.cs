using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class Locator
{
    public static EventManager event_manager = new EventManager();
    public static VFXFactory vfx_factory = null;
    public static Player player = null;
    public static CsvParser csv_parser = new CsvParser();
    public static IItemPool item_pool = new ItemPool();
    public static IItemFactory item_factory = new ItemFactory(item_pool.get_infos());
    public static IItemManager item_manager = new ItemManager();
    public static LevelManager level_manager = null;
    public static ProjectileFactory projectile_factory = null;
    public static IPauseController pause_controller = new PauseController();
    public static DamageManager damageManager = null;

    public static void reset()
    {
        event_manager = new EventManager();
        item_manager = new ItemManager();
        vfx_factory = null;
        player = null;
        level_manager = null;
        projectile_factory = null;
    }
}