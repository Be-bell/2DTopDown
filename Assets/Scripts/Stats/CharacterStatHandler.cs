using System;
using System.Collections.Generic;
using UnityEngine;

/*
 * ���� ���� ����
 */
public class CharacterStatHandler : MonoBehaviour
{
    // �⺻���ݰ� �߰����ݵ��� ����ؼ� ���� ������ ����ϴ� ������ ����.
    // �ٵ� ������ �׳� �⺻ ���ݸ�
    [SerializeField] private CharacterStat baseStat;

    public CharacterStat CurrentStat { get; private set; }

    // ĳ���� ���ȵ��� �����ϴ� �뵵���� ������ ����.
    public List<CharacterStat> statModifiers = new List<CharacterStat>();

    private void Awake()
    {
        UpdateCharacterStat();
    }


    private void UpdateCharacterStat()
    {
        AttackSO attackSO = null;
        
        // ����Ƽ���� attackSO�� �־�������, �� ���� ���� ������ �־���.
        if(baseStat.attackSO != null)
        {
            attackSO = Instantiate(baseStat.attackSO);
        }

        // null�� �� ���� �ְ�, �ƴ� ���� ����.
        CurrentStat = new CharacterStat { attackSO = attackSO };
        // TODO : ������ �⺻ �ɷ�ġ�� ���������, �����δ� �ɷ�ġ ��ȭ ����� ����ȴ�.

        CurrentStat.statsChangeType = baseStat.statsChangeType;
        CurrentStat.maxHealth = baseStat.maxHealth;
        CurrentStat.speed = baseStat.speed;
    }
}