using System;
using UnityEngine;

public class TopDownController : MonoBehaviour
{
    /*
     * TopDownController = �� �ൿ�̺�Ʈ�� ����ϴ� Ŭ����
     * �̺�Ʈ���� ����, �ҷ����� ���Ҹ� ��.
     * (������ ������ �ϳ��� ������ ���� ����. �׳� �޼ҵ���� ����ִ� ���Ҹ�.)
     * ���� �Լ����� �ൿ�� ���� ������ �����, �� �̺�Ʈ�� �޼ҵ���� �����ش�.
     * �̺�Ʈ�� ȣ���Ϸ���, Call�Լ��� ���� ȣ��.
     * ��� ����, ������ �������ִ� ������ ��µ�..?
     * �� Controller(���� �Է¹���) -> TopDownController(���� ����) -> Behaviours(�ൿ ���� ����)
     */

    // �� �ൿ �̺�Ʈ.
    public event Action<Vector2> OnMoveEvent;
    public event Action<Vector2> OnLookEvent;
    public event Action<AttackSO> OnAttackEvent;

    // attack ������ �ֱ� ���� ���.
    private float timeSinceLastAttack = float.MaxValue;
    protected bool IsAttacking { get; set; }

    /* 
     * protected ������Ƽ�� �� ���� : ���� �ٲٰ� ������ �������� �� �� ��ӹ޴� Ŭ�����鵵 �� �� �ְ�
     * EnemyController ���� �ڽ�Ŭ�������� �����.
     */
    protected CharacterStatHandler stats { get; private set; }

    protected virtual void Awake()
    {
        stats = GetComponent<CharacterStatHandler>();
    }

    private void Update()
    {
        HandleAttackDelay();
    }

    // ���� ������ �ֱ�.
    private void HandleAttackDelay()
    {
        if(timeSinceLastAttack <= stats.CurrentStat.attackSO.delay)
        {
            timeSinceLastAttack += Time.deltaTime;
        }

        if(IsAttacking && timeSinceLastAttack > stats.CurrentStat.attackSO.delay)
        {
            timeSinceLastAttack = 0;
            CallAttackEvent(stats.CurrentStat.attackSO);
        }
    }

    /*
     * Call �޼ҵ� = �� �̺�Ʈ ȣ��.
     * Invoke�� ���� �� �̺�Ʈ���� �޼ҵ�� ��� ȣ����.
     */
    private void CallAttackEvent(AttackSO attackSO)
    {
        OnAttackEvent?.Invoke(attackSO);
    }

    public void CallMoveEvent(Vector2 direction)
    {
        OnMoveEvent?.Invoke(direction);
    }

    public void CallLookEvent(Vector2 direction)
    {
        OnLookEvent?.Invoke(direction);
    }

    
}
