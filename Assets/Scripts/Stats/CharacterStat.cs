using UnityEngine;

public enum StatsChangeType
{
    Add, // 0
    Multiple, // 1
    Override // 2
}

// 데이터 폴더처럼 사용할 수 있게 만들어주는 Attribute
// 유니티 인스펙터 창에서 접었다 폈다 할수 있게 해주는듯.
// 스탯을 저장하는 역할. (인스펙터 창에서 설정이 가능하게끔)
[System.Serializable]

public class CharacterStat
{
    public StatsChangeType statsChangeType;
    [Range(1, 100)] public int maxHealth;
    [Range(1f, 20f)] public float speed;
    public AttackSO attackSO;
}