using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePullingManager : MonoBehaviour
{
    static public Dictionary<string,List<GameObject>> dictionary = new Dictionary<string, List<GameObject>>();

//절대 오브젝트명이 같으면 안됨.
    static public GameObject InstantiatePulling(GameObject obj, Transform trans = null)
    {
        GameObject g = null;

        if(dictionary.ContainsKey(obj.name))
        {
            bool check = false;
            for(int i=0;i<dictionary[obj.name].Count;i++)
            {
                if(dictionary[obj.name][i] == null)
                {
                    dictionary[obj.name].RemoveAt(i);
                    i--;
                    continue;
                }

                if(!dictionary[obj.name][i].activeSelf)
                {
                    GameObject temp = dictionary[obj.name][i];
                    temp.SetActive(true);
                    if(trans != null)temp.transform.SetParent(trans);
                    else temp.transform.SetParent(null);
                    g = temp;
                    check = true;
                    break;
                }
            }
            if(!check)
            {
                g = CreateObj();
                dictionary[obj.name].Add(g);
            }
        }
        else
        {
            dictionary.Add(obj.name, new List<GameObject>());
            g = CreateObj();
            dictionary[obj.name].Add(g);
        }

        GameObject CreateObj()
        {
            if (trans == null)
            {
               return Instantiate(obj);
            }
            else
            {
                return Instantiate(obj, trans);
            }
        }

        return g;
    }
}
