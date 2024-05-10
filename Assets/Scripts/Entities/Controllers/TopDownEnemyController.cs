using UnityEngine;
using UnityEngine.UIElements;

public class TopDownEnemyController : TopDownController
{
    // ����� target�� ��ġ ��������.
    protected Transform ClosestTarget { get; private set; }

    protected override void Awake()
    {
        // controller���� stats�� ������.
        base.Awake();
    }

    protected virtual void Start()
    {
        // ���� ���� ��, Ÿ���� player�� ����.
        ClosestTarget = GameManager.instance.Player;
    }

    // �������꿣���ε�, ���� ������..? �̵��ӵ�?
    protected virtual void FixedUpdate()
    {

    }

    // Ÿ�ٰ��� �Ÿ�
    protected float DistanceToTarget()
    {
        return Vector3.Distance(transform.position, ClosestTarget.position);
    }

    // Ÿ�������� ����. (Enemy -> target) ����
    protected Vector2 DirectionToTarget()
    {
        return (ClosestTarget.position - transform.position).normalized;
    }
}
