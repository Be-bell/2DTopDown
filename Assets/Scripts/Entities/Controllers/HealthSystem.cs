using System;
using UnityEngine;

/*
 * 체력관리에 관한 클래스
 * ChangeHealth를 통해 값이 들어오고, 이 값에 따라
 * 공격인지, 힐인지 판단하여 처리함.
 * (독데미지도 될 수 있나?)
 */
public class HealthSystem : MonoBehaviour
{
    // 작동하는 쿨 길이
    [SerializeField] private float healthChangeDelay = 0.5f;

    // 캐릭터 스탯
    private CharacterStatHandler statHandler;

    // 데미지 받는 쿨타임이라 생각하면 편할듯.
    private float timeSinceLastChange = float.MaxValue;

    // false면 데미지 받을 수 있음. true면 데미지 못받는 상태
    private bool isAttacked = false;

    // 이벤트
    public event Action OnDamage;
    public event Action OnHeal;
    public event Action OnDeath;
    public event Action OnInvincibilityEnd;

    // 현재 체력
    public float CurrentHealth { get; private set; }

    // statHandler에서 maxHealth를 가져와서 대입시킴.
    public float MaxHealth => statHandler.CurrentStat.maxHealth;


    private void Awake()
    {
        statHandler = GetComponent<CharacterStatHandler>();
    }

    // 게임 시작하면 현재체력 = 풀피
    private void Start()
    {
        CurrentHealth = MaxHealth;
    }

    private void Update()
    {
        // damage가 들어왔는데 아직 쿨타임이 안되었으면,
        if(isAttacked && timeSinceLastChange < healthChangeDelay)
        {
            // 쿨타임 계속 진행.
            timeSinceLastChange += Time.deltaTime;

            // 쿨타임이 끝나면
            if(timeSinceLastChange >= healthChangeDelay)
            {
                // 무적 끝, 다시 데미지 받는거 가능.
                OnInvincibilityEnd?.Invoke();
                isAttacked = false;
            }
        }
    }

    /* 
     * projectileController에서 OS의 power값을 받아옴.(음수로)
     * 공격 받을때 사용되는 로직. 아마 체력이 깎이는지 안깎이는지 판단하는듯.
     * 어떤 물체와 부딪혔을 때 체력이 어떻게 변하는지 쓰이는 로직.
     * 무언가와 부딪혔을 때, 쿨타임(무적시간) 안에 들어오면 false를 return하고,
     * 그게 아니면 true를 return. 내부에서 상호작용은 알아서 작동됨.
     */
    public bool ChangeHealth(float change)
    {
        // 공격받을때 딜레이시간보다 짧은 시간이면 데미지가 안들어가고 끝나는 상황
        if (timeSinceLastChange < healthChangeDelay)
        {
            return false;
        }

        timeSinceLastChange = 0f;
        CurrentHealth += change;

        // Clamp -> 최대 최솟값 설정.
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);

        // 현재 체력이 0보다 작거나 같으면 죽음.
        if(CurrentHealth <= 0f)
        {
            CallDeath();
            return true;
        }

        // 받은 데미지가 0보다 크거나 같으면 힐
        if(change>= 0)
        {
            OnHeal?.Invoke();
        }

        // 0보다 작으면 데미지 들어감.
        else
        {
            OnDamage?.Invoke();
            isAttacked = true;
        }

        return true;
    }

    private void CallDeath()
    {
        OnDeath?.Invoke();
    }
}