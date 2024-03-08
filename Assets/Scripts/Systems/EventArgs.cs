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
public class OnRightMoveEvent: IEventParam{}

public class OffLeftMoveEvent: IEventParam{}
public class OffRightMoveEvent: IEventParam{}

public class OnAttackEvent: IEventParam{}
