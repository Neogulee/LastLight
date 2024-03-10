using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempGameManager : MonoBehaviour
{
    public static TempGameManager instance;

    public bool isPause = false;

    public int curHp = 10;
    public int maxHp = 10;
    public int curLevel = 1;
    public float curExp = 0;
    public float[] listExp = new float[50];
    public int killCount = 0;

    public LevelUp uiLevelUp;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        // 겸험치바가 1.1 , 1.21 ,1.331 같이 1.1배식 증가 
        // 추후에 세부 경험치 증가량을 토론을 통해 수정 예정 
        float exp = 10;
        for (int i = 0; i < listExp.Length; i++)
        {
            listExp[i] = exp;
            exp *= 1.1f;
        }

    }

    

    // 이곳에서 레벨업시에 게임 정지하는 로직 추가 해줘
    // 게임 재게하는 로직은 LevelUp.cs에 추가 예정 
    public void GetExp(float plusExp)
    {
        curExp += plusExp;
        if (curExp >= listExp[curLevel - 1])
        {
            curExp -= listExp[curLevel - 1];
            curLevel++;
            uiLevelUp.Show();
        }
    }

    public void GetDamage(int damage)
    {
        curHp -= damage;
        if (curHp <= 0)
        {
            // 게임오버 로직 추가
        }
    }

    public void GetHp(int heal)
    {
        curHp = Mathf.Min(curHp + heal, maxHp);
    }


    //////////////////////////
    // 테스트 용으로 삭제 예쩡 //
    /////////////////////////
    private void Update()
    {
        // q 클릭시 경험치 2씩 증가
        if (Input.GetKeyDown(KeyCode.Q))
        {
            GetExp(2);
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            GetDamage(1);
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            GetHp(1);
        }
    }
    //////////////////////////
    // 테스트 용으로 삭제 예쩡 //
    /////////////////////////
}
