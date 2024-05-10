using System;
using UnityEngine;

public class TopDownController : MonoBehaviour
{
    /*
     * TopDownController = 각 행동이벤트를 담당하는 클래스
     * 이벤트들을 저장, 불러오는 역할만 함.
     * (실제로 로직은 하나도 구현이 되지 않음. 그냥 메소드들을 모아주는 역할만.)
     * 여러 함수에서 행동에 대한 로직을 만들면, 각 이벤트에 메소드들을 더해준다.
     * 이벤트를 호출하려면, Call함수를 통해 호출.
     * 어떻게 보면, 변수를 전달해주는 느낌이 드는듯..?
     * 각 Controller(변수 입력받음) -> TopDownController(변수 전달) -> Behaviours(행동 로직 구현)
     */

    // 각 행동 이벤트.
    public event Action<Vector2> OnMoveEvent;
    public event Action<Vector2> OnLookEvent;
    public event Action<AttackSO> OnAttackEvent;

    // attack 딜레이 주기 위한 멤버.
    private float timeSinceLastAttack = float.MaxValue;
    protected bool IsAttacking { get; set; }

    /* 
     * protected 프로퍼티를 한 이유 : 나만 바꾸고 싶지만 가져가는 건 내 상속받는 클래스들도 볼 수 있게
     * EnemyController 밑의 자식클래스에서 사용함.
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

    // 공격 딜레이 주기.
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
     * Call 메소드 = 각 이벤트 호출.
     * Invoke를 통해 각 이벤트안의 메소드들 모두 호출함.
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
