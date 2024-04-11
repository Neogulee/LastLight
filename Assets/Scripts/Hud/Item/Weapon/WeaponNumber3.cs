using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunder : Weapon
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public override void IncreaseLevel()
    {
        shootThreshold *= 0.8f;
        damage *= 1.2f;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
