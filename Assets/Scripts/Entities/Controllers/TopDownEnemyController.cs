using UnityEngine;
using UnityEngine.UIElements;

public class TopDownEnemyController : TopDownController
{
    // 가까운 target의 위치 가져오기.
    protected Transform ClosestTarget { get; private set; }

    protected override void Awake()
    {
        // controller에서 stats를 가져옴.
        base.Awake();
    }

    protected virtual void Start()
    {
        // 게임 시작 시, 타겟을 player로 잡음.
        ClosestTarget = GameManager.instance.Player;
    }

    // 물리연산엔진인데, 뭐를 넣을까..? 이동속도?
    protected virtual void FixedUpdate()
    {

    }

    // 타겟과의 거리
    protected float DistanceToTarget()
    {
        return Vector3.Distance(transform.position, ClosestTarget.position);
    }

    // 타겟으로의 방향. (Enemy -> target) 벡터
    protected Vector2 DirectionToTarget()
    {
        return (ClosestTarget.position - transform.position).normalized;
    }
}
