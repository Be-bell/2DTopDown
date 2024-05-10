using System;
using UnityEngine;

/*
 * ü�°����� ���� Ŭ����
 * ChangeHealth�� ���� ���� ������, �� ���� ����
 * ��������, ������ �Ǵ��Ͽ� ó����.
 * (���������� �� �� �ֳ�?)
 */
public class HealthSystem : MonoBehaviour
{
    // �۵��ϴ� �� ����
    [SerializeField] private float healthChangeDelay = 0.5f;

    // ĳ���� ����
    private CharacterStatHandler statHandler;

    // ������ �޴� ��Ÿ���̶� �����ϸ� ���ҵ�.
    private float timeSinceLastChange = float.MaxValue;

    // false�� ������ ���� �� ����. true�� ������ ���޴� ����
    private bool isAttacked = false;

    // �̺�Ʈ
    public event Action OnDamage;
    public event Action OnHeal;
    public event Action OnDeath;
    public event Action OnInvincibilityEnd;

    // ���� ü��
    public float CurrentHealth { get; private set; }

    // statHandler���� maxHealth�� �����ͼ� ���Խ�Ŵ.
    public float MaxHealth => statHandler.CurrentStat.maxHealth;


    private void Awake()
    {
        statHandler = GetComponent<CharacterStatHandler>();
    }

    // ���� �����ϸ� ����ü�� = Ǯ��
    private void Start()
    {
        CurrentHealth = MaxHealth;
    }

    private void Update()
    {
        // damage�� ���Դµ� ���� ��Ÿ���� �ȵǾ�����,
        if(isAttacked && timeSinceLastChange < healthChangeDelay)
        {
            // ��Ÿ�� ��� ����.
            timeSinceLastChange += Time.deltaTime;

            // ��Ÿ���� ������
            if(timeSinceLastChange >= healthChangeDelay)
            {
                // ���� ��, �ٽ� ������ �޴°� ����.
                OnInvincibilityEnd?.Invoke();
                isAttacked = false;
            }
        }
    }

    /* 
     * projectileController���� OS�� power���� �޾ƿ�.(������)
     * ���� ������ ���Ǵ� ����. �Ƹ� ü���� ���̴��� �ȱ��̴��� �Ǵ��ϴµ�.
     * � ��ü�� �ε����� �� ü���� ��� ���ϴ��� ���̴� ����.
     * ���𰡿� �ε����� ��, ��Ÿ��(�����ð�) �ȿ� ������ false�� return�ϰ�,
     * �װ� �ƴϸ� true�� return. ���ο��� ��ȣ�ۿ��� �˾Ƽ� �۵���.
     */
    public bool ChangeHealth(float change)
    {
        // ���ݹ����� �����̽ð����� ª�� �ð��̸� �������� �ȵ��� ������ ��Ȳ
        if (timeSinceLastChange < healthChangeDelay)
        {
            return false;
        }

        timeSinceLastChange = 0f;
        CurrentHealth += change;

        // Clamp -> �ִ� �ּڰ� ����.
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);

        // ���� ü���� 0���� �۰ų� ������ ����.
        if(CurrentHealth <= 0f)
        {
            CallDeath();
            return true;
        }

        // ���� �������� 0���� ũ�ų� ������ ��
        if(change>= 0)
        {
            OnHeal?.Invoke();
        }

        // 0���� ������ ������ ��.
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