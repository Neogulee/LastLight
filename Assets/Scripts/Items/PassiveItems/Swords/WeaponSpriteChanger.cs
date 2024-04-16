using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpriteChanger : MonoBehaviour
{
    public Sprite[] weaponSprites; // 각 무기에 대한 다른 스프라이트 배열

    private SpriteRenderer spriteRenderer; // 무기의 스프라이트 렌더러

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        ChangeWeaponSpriteRandomly();
    }


    private void ChangeWeaponSpriteRandomly()
    {
        if (weaponSprites.Length > 0)
        {
            int randomIndex = Random.Range(0, weaponSprites.Length);
            spriteRenderer.sprite = weaponSprites[randomIndex];
        }
        else
        {
            Debug.LogError("No weapon sprites available.");
        }
    }
}