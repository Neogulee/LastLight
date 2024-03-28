using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFactory : MonoBehaviour
{
    public static Weapon CreateWeapon(int itemId)
    {
        Weapon newWeapon = null;

        switch (itemId)
        {
            case 0:
                newWeapon = new GameObject().AddComponent<WeaponNumber0>();
                break;
            case 1:
                newWeapon = new GameObject().AddComponent<WeaponNumber1>();
                break;
            case 2:
                newWeapon = new GameObject().AddComponent<WeaponNumber2>();
                break;
            case 3:
                newWeapon = new GameObject().AddComponent<WeaponNumber3>();
                break;
            case 4:
                newWeapon = new GameObject().AddComponent<WeaponNumber4>();
                break;
            case 5:
                newWeapon = new GameObject().AddComponent<WeaponNumber5>();
                break;
            default:
                newWeapon = new GameObject().AddComponent<DummyWeapon>();
                Debug.LogError("Invalid itemId!");
                break;
        }


        return newWeapon;
    }
}
