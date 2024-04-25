using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollMainMenu : MonoBehaviour
{
    // 삭제 예정 

    public float speed = 5.0f;
    void Update()
    {
        // x좌표를 speed씩 이동하도록 함
        transform.position += new Vector3(speed, 0, 0) * Time.deltaTime;
    }
}
