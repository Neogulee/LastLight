using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IStatusEffect
{
    public Actor actor { get; }
    public float total_duration { get; }
    public float remain_duration { get; }
    public float age { get; }
    public float power { get; }
    public void update();
    public void remove();
    public void on_applied();
}

public abstract class StatusEffect : IStatusEffect
{
    public Actor actor { get; private set; }
    public float total_duration { get; protected set; }
    public float remain_duration { get; protected set; }
    public float age { get; protected set; }
    public float power { get; protected set; }

    public StatusEffect(Actor actor, float total_duration, float power)
    {
        this.actor = actor;
        this.total_duration = total_duration;
        this.remain_duration = total_duration;
        this.power = power;
    }

    public virtual void update()
    {
        remain_duration -= Time.fixedDeltaTime;
        age += Time.fixedDeltaTime;
        if (remain_duration <= 0.0f) {
            actor.on_status_effect_finished(this);
            remove();
        }
    }

    public void remove()
    {
        on_removed();
    }

    public abstract void on_applied();
    public abstract void on_removed();
}
