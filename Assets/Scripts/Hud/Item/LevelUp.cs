using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    RectTransform rect;
    Item[] items;
    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        items = GetComponentsInChildren<Item>(true);
    }

    public void Show()
    {
        Next();
        rect.localScale = Vector3.one;
    }

    public void Hide()
    {
        rect.localScale = Vector3.zero;
        // ���� ���״� ���� �ٽ� ��� 
    }
 
    void Next()
    {
        // ��ü ��Ȱ��ȭ
        foreach(Item item in items)
        {
            item.gameObject.SetActive(false);
        }

        // �����ϰ� 3�� Ȱ��ȭ 
        int[] ran = new int[3];
        while (true)
        {

            // �˰������ ������ �ִµ� ���Ŀ� ���� ����
            // ���� 1. ������ ��� ���� �ٽ��ϴ� �ڵ尡 ���� 
            ran[0] = Random.Range(0, items.Length);
            ran[1] = Random.Range(0, items.Length);
            ran[2] = Random.Range(0, items.Length);
            if (ran[0] != ran[1] && ran[1] != ran[2] && ran[0] != ran[2])
                break;
        }
        for(int index = 0; index < ran.Length; index++)
        {
            Item ranItem = items[ran[index]];
            ranItem.gameObject.SetActive(true);
        }

    }


}
