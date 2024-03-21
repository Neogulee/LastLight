using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[Serializable]
public class SpawnInfo
{
    public GameObject enemy;
}

[Serializable]
public class PeriodicSpawnInfo: SpawnInfo
{
    public float start_time, end_time;
    public float spawn_rate;
}

[Serializable]
public class BurstSpawnInfo: SpawnInfo
{
    public float start_time, amount;
}