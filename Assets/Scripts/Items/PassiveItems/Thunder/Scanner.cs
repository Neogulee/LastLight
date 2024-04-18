using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    public float scanRange = 3;
    public LayerMask targetLayer;
    public RaycastHit2D[] targets;
    public Transform nearestTarget;

    private void Update()
    {
        targets = Physics2D.CircleCastAll(transform.position,scanRange,Vector2.zero,0,targetLayer);
        Debug.Log(targets.Length);
        nearestTarget = GetNearest();
        
    }
    Transform GetNearest()
    {
        Transform result = null;
        float diff = 100;
        foreach (RaycastHit2D item in targets)
        {
            Vector3 myPos = transform.position;
            Vector3 targetPos = item.transform.position;
            float curDiff = Vector3.Distance(myPos, targetPos);
            if(curDiff < diff)
            {
                diff = curDiff;
                result = item.transform;
            }
        }


        return result;
    }
}
