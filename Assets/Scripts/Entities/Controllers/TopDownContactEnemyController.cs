using System;
using UnityEngine;
/*
 * ContactEnemyController
 * 근접유닛 controller, enemyController를 상속받아옴.
 * 근접유닛에 대한 로직이 들어있지 않을까?
 */
public class TopDownContactEnemyController : TopDownEnemyController
{
    // 쫒아오는 거리(아마 플레이어 인식하는 거리인듯)
    [SerializeField][Range(0f, 100f)] private float followRange;

    // target tag
    [SerializeField] private string targetTag = "Player";

    // 그냥 근접유닛 renderer
    [SerializeField] private SpriteRenderer characterRenderer;

    // target과 부딪혔는지 아닌지 판단.
    private bool isCollidingWithTarget;

    // 이건 enemy꺼
    private HealthSystem healthSystem;

    // 이건 부딪힌 target꺼
    private HealthSystem collidingTargetHealthSystem;
    private TopDownMovement collidingMovement;

    protected override void Start()
    {
        // 타겟 가져옴.(closestTarget)
        base.Start();

        healthSystem = GetComponent<HealthSystem>();
        healthSystem.OnDamage += OnDamage;
    }

    // 근접유닛 데미지 받으면 실행되는 거
    private void OnDamage()
    {
        followRange = 100f;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        // 부딪혔을 경우
        if (isCollidingWithTarget)
        {
            ApplyHealthChange();
        }

        
        Vector2 direction = Vector2.zero;
        if(DistanceToTarget() < followRange)
        {
            direction = DirectionToTarget();
        }

        CallMoveEvent(direction);
        Rotate(direction);
    }

    // 데미지를 주고, 넉백 가능하면 넉백시킴.
    private void ApplyHealthChange()
    {
        AttackSO attackSO = stats.CurrentStat.attackSO;
        bool isAttackable = collidingTargetHealthSystem.ChangeHealth(-attackSO.power);

        if(isAttackable && attackSO.isOnKnockBack && collidingMovement != null)
        {
            collidingMovement.ApplyKnockback(transform, attackSO.knockbackPower, attackSO.knockbackTime);
        }
    }

    // 회전해줌.
    private void Rotate(Vector2 direction)
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        characterRenderer.flipX = Mathf.Abs(rotZ) > 90f;

    }

    // target과 부딪히면 collidingMovement 부여.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject receiver = collision.gameObject;

        if(!receiver.CompareTag(targetTag))
        {
            return;
        }

        collidingTargetHealthSystem = collision.GetComponent<HealthSystem>();
        if(collidingTargetHealthSystem != null )
        {
            isCollidingWithTarget = true;
        }

        collidingMovement = collision.GetComponent<TopDownMovement>();
    }

    // 부딪히는게 끝나면 부딪혔다는 것을 false로 바꿔줌.
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(!collision.CompareTag(targetTag)) { return; }

        isCollidingWithTarget = false;
    }
}