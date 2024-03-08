using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName="EnemySpawnInfo", menuName="ScriptableObjects/EnemySpawnInfo", order=1)]
public class EnemySpawnInfo : ScriptableObject
{
    public List<GameObject> enemies = new();
    public GameObject sample()
    {
        return enemies[Random.Range(0, enemies.Count)];
    }
}