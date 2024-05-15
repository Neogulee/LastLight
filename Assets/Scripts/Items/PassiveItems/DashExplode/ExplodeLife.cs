using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeLife : MonoBehaviour
{
    public void Dead()
    {
        Destroy(gameObject);
    }
}
