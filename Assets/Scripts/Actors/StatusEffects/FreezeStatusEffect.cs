using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FreezeStatusEffect : StatusEffect
{
    PhysicsPlatformer actor_physics = null;
    ActorAnimator actor_animator = null;
    public FreezeStatusEffect(Actor actor, float total_duration, float power)
            : base(actor, total_duration, power)
    {
        actor_physics = actor.GetComponent<PhysicsPlatformer>();
        actor_animator = actor.GetComponent<ActorAnimator>();
    }

    public override void on_applied()
    {
        // TODO: 플레이어 대시 등 제한
        actor_physics.fix_velocity(Vector2.zero);
        actor_animator?.stop();
    }

    public override void on_removed()
    {
        actor_physics.unfix_velocity();
        actor_animator?.play();
    }
}
