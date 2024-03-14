using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Actor
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpPower;
    [SerializeField]private EventManager event_manager;
    private Rigidbody2D rigid;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private PlayerController playerController;
    private PlayerAttack playerAttack;
    private BoxCollider2D boxCollider;

    private void Start() 
    {
        Locator.player = this;
        
        playerController = GetComponent<PlayerController>();
        playerAttack = GetComponent<PlayerAttack>();
        playerAttack.Init(this);
        playerController.Init(this);

        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
    public Rigidbody2D GetRigidbody()
    {
        return rigid;
    }
    public BoxCollider2D GetBoxCollider()
    {
        return boxCollider;
    }
    public SpriteRenderer GetSpriteRenderer()
    {
        return spriteRenderer;
    }
    public Animator GetAnimator()
    {
        return animator;
    }
    public void EventRegister<T>(Action<IEventParam> action)
    {
        event_manager.register<T>(action);
    }
    public float GetMoveSpeed()
    {
        return moveSpeed;
    }
    public float GetJumpPower()
    {
        return jumpPower;
    }
}
