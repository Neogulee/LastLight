using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeSummon : ActiveItem
{
    [SerializeField] private GameObject ropePrefab;
    public override void use()
    {
        GameObject rope = Object.Instantiate(ropePrefab);
        rope.GetComponent<Rope>().dir = Locator.player.GetComponent<APlayerController>().getAimDir();
        rope.transform.position = Locator.player.transform.position;
    }
}
