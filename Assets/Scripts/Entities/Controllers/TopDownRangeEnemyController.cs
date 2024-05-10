using System;
using UnityEngine;

public class TopDownRangeEnemyController : TopDownEnemyController
{
    [SerializeField][Range(0f, 100f)] private float followRange = 15f;
    [SerializeField][Range(0f, 100f)] private float shootRange = 15f;

    private int layermaskTarget;

    protected override void Start()
    {
        base.Start();
        layermaskTarget = stats.CurrentStat.attackSO.target;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        float distanceToTarget = DistanceToTarget();
        Vector2 directionTotarget = DirectionToTarget();

        UpdateEnemyState(distanceToTarget, directionTotarget);
    }

    private void UpdateEnemyState(float distanceToTarget, Vector2 directionTotarget)
    {
        IsAttacking = false;
        if(distanceToTarget<followRange)
        {
            CheckIfNear(distanceToTarget, directionTotarget);
        }
    }

    private void CheckIfNear(float distance, Vector2 direction)
    {
        if(distance <= shootRange)
        {
            TryShootAtTarget(direction);
        }
        else
        {
            CallMoveEvent(direction);   // 사정거리 밖이지만 추적 범위 내일 경우, 타겟쪽으로 이동.
        }
    }

    private void TryShootAtTarget(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, shootRange, layermaskTarget);

        if(hit.collider != null)
        {
            PerformAttackAction(direction);
        }
        else
        {
            CallMoveEvent(direction);
        }
    }

    private void PerformAttackAction(Vector2 direction)
    {
        CallLookEvent(direction);
        CallMoveEvent(Vector2.zero);
        IsAttacking = true;
    }
}