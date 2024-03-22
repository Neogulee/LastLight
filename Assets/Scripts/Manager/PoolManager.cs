using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public GameObject[] prefabs;

    List<GameObject>[] pools;

    void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];
        for (int i = 0; i < prefabs.Length; i++)
        {
            pools[i] = new List<GameObject>();
        }
    }

    public GameObject Get(int index)
    {
        GameObject select = null;
        foreach (GameObject item in pools[index])
        {
            if (!item.activeSelf)
            {
                select = item;
                break;
            }
        }
        if (select == null)
        {
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select);
        }
        select.SetActive(true); // 아이템을 활성화하는 부분을 이동했습니다.
        return select;
    }
}
