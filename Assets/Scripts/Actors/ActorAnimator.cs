using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public interface IActorAnimator
{
    public bool is_stopped { get; }
    public void stop();
    public void play();
}

public abstract class ActorAnimator : MonoBehaviour, IActorAnimator
{
    public bool is_stopped { get; private set; }
    protected Animator animator;
    public void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void play()
    {
        is_stopped = false;
        animator.enabled = true;
    }

    public void stop()
    {
        is_stopped = true;
        animator.enabled = false;
    }
}