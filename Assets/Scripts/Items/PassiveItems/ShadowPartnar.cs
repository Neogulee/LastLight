using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class PlayerAction
{
    public Vector2 pos;
    public Sprite sprite;

    public int on_idx;
    public int off_idx;
    public PlayerAction(Vector2 pos, Sprite sprite, int on_idx = -1, int off_idx = -1)
    {
        this.pos = pos;
        this.sprite = sprite;
        this.on_idx = on_idx;
        this.off_idx = off_idx;
    }
}
public class ShadowPartnar : PassiveItem
{
    private Queue<PlayerAction> playerActions;
    public List<Damager> damager;


    private int onIdx = -1;
    private int offIdx = -1;
    void Start()
    {
        playerActions = new Queue<PlayerAction>();
        SetupDamager(damage);
        StartCoroutine(IUpdate());
        Locator.event_manager.register<OnAttackerEvent>(SummonAttacker);
        Locator.event_manager.register<OffAttackerEvent>(StopSummonAttacker);
    }
    public override bool increase_level()
    {
        if (base.increase_level())
        {
            SetupDamager(damage);
            return true;
        }
        else return false;

    }
    void SetupDamager(int damage)
    {
        damager = new List<Damager>();
        foreach(Damager d in transform.parent.GetComponent<MeleeAttacker>().damagers)
        {
            damager.Add(d);
        }

        for(int i=0;i<damager.Count;i++)
        {
            damager[i] = transform.GetChild(0).Find(damager[i].gameObject.name).GetComponent<Damager>();
            damager[i].damage /= 5;
            damager[i].damage *= damage;
        }
    }
    void SummonAttacker(IEventParam param)
    {
        OnAttackerEvent e = (OnAttackerEvent)param;
        onIdx = e.idx;
    }
    void StopSummonAttacker(IEventParam param)
    {
        OffAttackerEvent e = (OffAttackerEvent)param;
        offIdx = e.idx;
    }
    IEnumerator IUpdate()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.01f);
            playerActions.Enqueue(new PlayerAction(transform.parent.position, transform.parent.GetComponent<SpriteRenderer>().sprite, onIdx, offIdx));
            onIdx = -1;
            offIdx = -1;

            if (playerActions.Count > 30)
            {
                PlayerAction action = playerActions.Dequeue();
                transform.position = action.pos;
                GetComponent<SpriteRenderer>().sprite = action.sprite;
                if(action.on_idx != -1)
                {
                    damager[action.on_idx].enable();
                }
                if(action.off_idx != -1)
                {
                    damager[action.off_idx].disable();
                }
            }
        }
    }
}
