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

        // ����ġ�ٰ� 1.1 , 1.21 ,1.331 ���� 1.1��� ���� 
        // ���Ŀ� ���� ����ġ �������� ����� ���� ���� ���� 
        float exp = 10;
        for (int i = 0; i < listExp.Length; i++)
        {
            listExp[i] = exp;
            exp *= 1.1f;
        }

    }

    

    // �̰����� �������ÿ� ���� �����ϴ� ���� �߰� ����
    // ���� ����ϴ� ������ LevelUp.cs�� �߰� ���� 
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
            // ���ӿ��� ���� �߰�
        }
    }

    public void GetHp(int heal)
    {
        curHp = Mathf.Min(curHp + heal, maxHp);
    }


    //////////////////////////
    // �׽�Ʈ ������ ���� ���� //
    /////////////////////////
    private void Update()
    {
        // q Ŭ���� ����ġ 2�� ����
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
    // �׽�Ʈ ������ ���� ���� //
    /////////////////////////
}
