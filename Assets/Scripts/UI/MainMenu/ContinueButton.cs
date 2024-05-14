using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ContinueButton : MonoBehaviour
{
    public Option option;


    public void Clicked()
    {
        // 버튼 클릭하면 space바 눌러도 이 함수가 작동하는 버그 있어서 아래처럼 임시 방편으로 사용
        if(option.transform.localScale != Vector3.zero)
        {
            option.ButtonClicked();
        }
    }
}
