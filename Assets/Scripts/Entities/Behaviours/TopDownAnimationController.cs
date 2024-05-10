using System;
using UnityEngine;
/*
 * 진짜 animation 관리하는 곳.
 * 이런 느낌으로 관리하는구나 정도만 알아두면 매우 좋을듯.
 */
public class TopDownAnimationController : AnimationController
{
    // 읽기전용. StringToHash로 해서 해시값으로 만듬.
    private static readonly int isWalk = Animator.StringToHash("isWalk");
    private static readonly int isHit = Animator.StringToHash("isHit");
    private static readonly int Attack = Animator.StringToHash("attack");

    private readonly float magnituteThreshold = 0.5f;
    private HealthSystem healthSystem;

    protected override void Awake()
    {
        // controller와 animator 받아옴.
        base.Awake();

        // healthsystem 받아옴.
        healthSystem = GetComponent<HealthSystem>();
    }

    private void Start()
    {
        controller.OnAttackEvent += Attacking;
        controller.OnMoveEvent += Move;

        if(healthSystem != null)
        {
            healthSystem.OnDamage += Hit;
            healthSystem.OnInvincibilityEnd += InvincibilityEnd;
        }
    }


    private void Move(Vector2 vector)
    {
        animator.SetBool(isWalk, vector.magnitude > magnituteThreshold);
    }

    // attackSo를 사용하진 않지만 가지고 있자.. -> 함수(메소드) 시그니처를 맞추는 것
    private void Attacking(AttackSO attackSO)
    {
        animator.SetTrigger(Attack);
    }

    private void Hit()
    {
        animator.SetBool(isHit, true);
    }

    private void InvincibilityEnd()
    {
        animator.SetBool(isHit, false);
    }
}