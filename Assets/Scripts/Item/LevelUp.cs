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
        // 정지 시켰던 게임 다시 재게 
    }
 
    void Next()
    {
        // 전체 비활성화
        foreach(Item item in items)
        {
            item.gameObject.SetActive(false);
        }

        // 랜덤하게 3개 활성화 
        int[] ran = new int[3];
        while (true)
        {

            // 알고리즘상 문제가 있는데 추후에 수정 예정
            // 문제 1. 만랩일 경우 뺴고 다시하는 코드가 없음 
            ran[0] = Random.Range(0, items.Length);
            ran[1] = Random.Range(0, items.Length);
            ran[2] = Random.Range(0, items.Length);
            if (ran[0] != ran[1] && ran[1] != ran[2] && ran[0] != ran[2])
                break;
        }
        for(int index = 0;index < ran.Length; index++)
        {
            Item ranItem = items[ran[index]];
            ranItem.gameObject.SetActive(true);
        }

    }

    //테스트 용으로 추후에 삭제 대상
    private void Update()
    {
        // 'q' 키를 누르면 Show() 호출
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Show();
        }
    }

}
