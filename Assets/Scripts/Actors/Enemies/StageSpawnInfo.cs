using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName="StageSpawnInfo", menuName="ScriptableObjects/StageSpawnInfo", order=1)]
public class StageSpawnInfo : ScriptableObject
{
    public List<PeriodicSpawnInfo> periodic_spawns = new();
    public List<BurstSpawnInfo> burst_spawns = new();

    public StageSpawnInfo()
    {
        periodic_spawns.Sort((PeriodicSpawnInfo a, PeriodicSpawnInfo b) => {
            if (a.start_time == b.start_time)
                return 0;
            return a.start_time < b.start_time ? -1 : 1;
        });

        burst_spawns.Sort((BurstSpawnInfo a, BurstSpawnInfo b) => {
            if (a.start_time == b.start_time)
                return 0;
            return a.start_time < b.start_time ? -1 : 1;
        });
    }
}