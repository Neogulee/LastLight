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
public class OnRightMoveEvent: IEventParam{}

public class OffLeftMoveEvent: IEventParam{}
public class OffRightMoveEvent: IEventParam{}
public class OffUpEvent: IEventParam{}

public class PlayerDash: IEventParam{}

public class OnAttackEvent: IEventParam{}

public class OnHpChangeEvent: IEventParam
{
    public int hp = 0;
}