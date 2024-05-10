using UnityEngine;

/*
 * 에셋 메뉴 만들기.
 * CreateAssetMenu 키워드는 유니티에서 오른쪽 클릭을 통해 메뉴창에 띄울 수 있음.
 * 여기서 AttackSO 클래스가 ScriptableObject를 상속받아서 사용되는데,
 * ScriptableObject는 Object를 스크립트처럼 쓸 수 있는 거인듯.
 * 약간 캐릭터 특성이나 그런거 설정할 때 좋아보임.
 */

[CreateAssetMenu(fileName = "DefaultAttackSO", menuName = "TopDownController/Attacks/Default", order = 0)]
public class AttackSO : ScriptableObject
{
    // 공격에 관한 설정
    [Header("Attack Info")]
    public float size;
    public float delay;
    public float power;
    public float speed;
    public LayerMask target;

    // 넉백에 관한 설정
    [Header("Knock Back Info")]
    public bool isOnKnockBack;
    public float knockbackPower;
    public float knockbackTime;
    
}
