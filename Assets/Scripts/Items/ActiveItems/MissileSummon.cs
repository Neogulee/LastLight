using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Scanner))]
public class MissileSummon : ActiveItem
{
    [SerializeField] private GameObject missilePrefab;
    private Scanner scanner;


    private float cooldownNow = 0;
    private void Start()
    {
        transform.localPosition = Vector3.zero;
        scanner = GetComponent<Scanner>();
        if (scanner == null)
        {
            Debug.LogError("Scanner 스크립트를 찾을 수 없습니다.");
            return;
        }
    }
    public override void use()
    {
        if (cooldown_timer > 0)
        {
            return;
        }
        Debug.Log("Test Item Use");
        scanner.FindEnemy();
        for (int i = 0; i < scanner.targets.Length; i++)
        {
            for (int j = 0; j < level; j++)
            {
                RaycastHit2D target = scanner.targets[i];
                GameObject missile = Instantiate(missilePrefab, transform.position, Quaternion.identity);
                missile.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
                missile.GetComponent<Missile>().target = target.transform;
            }
        }
    }
}
