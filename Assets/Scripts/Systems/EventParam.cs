using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IEventParam
{  

}

public class OnDestroyEvent: IEventParam
{
    public Actor actor = null;
}

public class OnJumpEvent: IEventParam{}

public class OnLeftMoveEvent: IEventParam{}
public class OnUpEvent: IEventParam{}
public class OnDownEvent: IEventParam{}
public class OnRightMoveEvent: IEventParam{}

public class OnShiftEvent: IEventParam{}

public class OffLeftMoveEvent: IEventParam{}
public class OffRightMoveEvent: IEventParam{}
public class OffUpEvent: IEventParam{}

public class OnDashEvent: IEventParam { }
public class OnGroundEvent: IEventParam { public float power; public OnGroundEvent(float power) { this.power = power; } }

public class OnAttackEvent : IEventParam { public int idx; public OnAttackEvent(int idx) { this.idx = idx; } }
public class OnAttackerEvent : IEventParam { public int idx; public OnAttackerEvent(int idx) { this.idx = idx; } }
public class OffAttackerEvent : IEventParam { public int idx; public OffAttackerEvent(int idx) { this.idx = idx; } }

public class OnPlayerAttackMelee : IEventParam
{
    public int damage;
    public Collider2D target;
    public OnPlayerAttackMelee(int damage, Collider2D target)
    {
        this.damage = damage; 
        this.target = target;
    } 
}


public class OnItemKeyPressed : IEventParam
{
    public int num;
    public OnItemKeyPressed(int num) { this.num = num; }
}
public class OnGuardDamageEvent: IEventParam{}
public class OnHpChangedEvent: IEventParam
{
    public int hp = 0;
}


public class ItemAddedEvent: IEventParam
{
    public Item item;
    public ItemAddedEvent(Item item)
    {
        this.item = item;
    }
}

public class EnemyDestroyedEvent: IEventParam
{
    public IEnemy enemy;
    public EnemyDestroyedEvent(IEnemy enemy)
    {
        this.enemy = enemy;
    }
}

class OnInputItemSelection: IEventParam
{
    public int idx;
    public OnInputItemSelection(int idx) { this.idx = idx; }
}

public class GameOverEvent: IEventParam { }
public class GameClearEvent: IEventParam { }

public class  OptionEvent: IEventParam { }
