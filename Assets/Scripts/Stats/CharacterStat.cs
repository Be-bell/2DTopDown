using UnityEngine;

public enum StatsChangeType
{
    Add, // 0
    Multiple, // 1
    Override // 2
}

// ������ ����ó�� ����� �� �ְ� ������ִ� Attribute
// ����Ƽ �ν����� â���� ������ ��� �Ҽ� �ְ� ���ִµ�.
// ������ �����ϴ� ����. (�ν����� â���� ������ �����ϰԲ�)
[System.Serializable]

public class CharacterStat
{
    public StatsChangeType statsChangeType;
    [Range(1, 100)] public int maxHealth;
    [Range(1f, 20f)] public float speed;
    public AttackSO attackSO;
}