using System;
using System.Collections.Generic;
using UnityEngine;

/*
 * 스탯 관련 로직
 */
public class CharacterStatHandler : MonoBehaviour
{
    // 기본스텟과 추가스텟들을 계산해서 최종 스텟을 계산하는 로직이 있음.
    // 근데 지금은 그냥 기본 스텟만
    [SerializeField] private CharacterStat baseStat;

    public CharacterStat CurrentStat { get; private set; }

    // 캐릭터 스탯들을 보관하는 용도이지 않을까 싶음.
    public List<CharacterStat> statModifiers = new List<CharacterStat>();

    private void Awake()
    {
        UpdateCharacterStat();
    }


    private void UpdateCharacterStat()
    {
        AttackSO attackSO = null;
        
        // 유니티에서 attackSO를 넣어줬으면, 그 값을 여기 변수에 넣어줌.
        if(baseStat.attackSO != null)
        {
            attackSO = Instantiate(baseStat.attackSO);
        }

        // null이 들어갈 수도 있고, 아닐 수도 있음.
        CurrentStat = new CharacterStat { attackSO = attackSO };
        // TODO : 지금은 기본 능력치만 적용되지만, 앞으로는 능력치 강화 기능이 적용된다.

        CurrentStat.statsChangeType = baseStat.statsChangeType;
        CurrentStat.maxHealth = baseStat.maxHealth;
        CurrentStat.speed = baseStat.speed;
    }
}