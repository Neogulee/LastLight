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
                newWeapon = new GameObject().AddComponent<Shield>();
                break;
            case 1:
                newWeapon = new GameObject().AddComponent<WoundsOfWind>();
                break;
            case 2:
                newWeapon = new GameObject().AddComponent<FlyingAx>();
                break;
            case 3:
                newWeapon = new GameObject().AddComponent<GateofBabylon>();
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
