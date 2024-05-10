using UnityEngine;

/*
 * RangedAttackSO = 원거리 대상 Attack 특성 설정
 * AttackSO 상속받으면 AttackSO 값도 같이 받아와짐.
 * 특정 개체 특성값을 다르게 설정할 때 유용할듯.
 */

[CreateAssetMenu(fileName = "RangedAttackSO", menuName = "TopDownController/Attacks/Ranged", order = 1)]
public class RangedAttackSO : AttackSO
{
    // 원거리 공격에 관한 특성
    [Header("Ranged Attack Info")]
    public string bulletNameTag;
    public float duration;
    public float spread;
    public int numberOfProjectilesPerShot;
    public float multipleProjectilesAngle;
    public Color projectileColor;
}
