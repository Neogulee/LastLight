using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Weapon : MonoBehaviour
{

    public int id;
    public float damage;
    Player player;

    float timer = 0;

    private void Awake()
    {
        player = TempGameManager.instance.player;
    }

    void Update()
    {
        switch (id)
        {
            case 0:
                timer += Time.deltaTime;
                if (timer > 1f)
                {
                    timer = 0;
                    Fire();
                }
                break;
            case 1:
                break;
        }
    }
    public void Init(ItemData data)
    {
        name = "Weapon" + data.itemId;
        transform.parent = player.transform;
        transform.localPosition = Vector3.zero;

        id = data.itemId;
        damage = data.baseDamage;
    }

    public void Fire()
    {


        // 플레이어의 이동 속도를 가져옴
        //float moveVelocity = player.GetComponent<ActorController>().velocity.x;

        //Debug.Log(moveVelocity);
        Debug.Log(player.GetAnimator().GetBool("isLeftRun"));
        // 타겟 방향 설정
        Vector3 targetDirection = (player.transform.localScale.x==-1) ? Vector3.left : Vector3.right;

        // 총알을 가져와 위치를 설정
        Transform bullet = TempGameManager.instance.poolManager.Get(id).transform;
        bullet.position = transform.position;

        // 총알의 현재 회전값을 가져옴
        Quaternion currentRotation = bullet.rotation;

        // 타겟 방향을 향하는 총알의 회전값 설정
        Quaternion targetRotation = Quaternion.FromToRotation(Vector3.up, targetDirection);

        // 회전값 설정
        bullet.rotation = targetRotation;

        // Init 함수 호출하여 총알 초기화
        bullet.GetComponent<RangedItem>().Init(damage, targetDirection);
        
    }
}
