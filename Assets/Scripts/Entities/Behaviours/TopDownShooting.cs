using UnityEngine;
using Random = UnityEngine.Random;

/*
 * TopDownShooting
 * 쏘는 것에 대한 로직 수행.
 * 화살 날라가는 로직 수행 (화살 생성부터 회전, 움직임까지 구현)
 */
public class TopDownShooting : MonoBehaviour
{
    // 변수 받아오기 위한 TopDownController
    private TopDownController controller;

    // 화살이 spawn되는 위치 저장(외부에서 끌어옴)
    [SerializeField] private Transform projectileSpawnPosition;

    // aim 방향
    private Vector2 aimDirection = Vector2.right;

    private void Awake()
    {
        controller = GetComponent<TopDownController>();
    }

    // controller에 메소드 대입해서 변수를 받아옴.
    private void Start()
    {
        controller.OnAttackEvent += OnShoot;
        controller.OnLookEvent += OnAim;
    }

    // aimDirection을 받아와서 대입해놓음.
    private void OnAim(Vector2 newAimDirection)
    {
        aimDirection = newAimDirection;
    }

    // attackSO(attack 특징들)을 가져와서 발사하는 로직 구현
    private void OnShoot(AttackSO attackSO)
    {
        // attackSo를 RangedAttackSO로 바꿔줌 (발사하는 로직이므로)
        RangedAttackSO rangedAttackSO = attackSO as RangedAttackSO;

        // 만약에 rangedAttackSO가 없으면 (원거리 공격이 아니면)
        // return해서 그냥 바로 나옴. 그게 아니면 밑의 로직 실행.
        if (rangedAttackSO == null) return;

        // 투사체 간의 각도 Angle을 받아옴.
        float projectilesAngleSpace = rangedAttackSO.multipleProjectilesAngle;

        // 한번 쏠 때의 투사체 개수
        int numberOfProjectilesPerShot = rangedAttackSO.numberOfProjectilesPerShot;

        // 첫 투사체의 위치를 판단함. 만약 투사체 개수가 1이면 minAngle = 0
        float minAngle = -(numberOfProjectilesPerShot / 2f) * projectilesAngleSpace + 0.5f * rangedAttackSO.multipleProjectilesAngle;

        // 투사체 개수에 따라 일정 각도로 투사체 생성
        for(int i = 0; i< numberOfProjectilesPerShot; i++)
        {
            float angle = minAngle + i * projectilesAngleSpace;

            // 랜덤으로 spread함. 이 부분은 없어도 될듯? (샷건같은 느낌인가?)
            float randomSpread = Random.Range(-rangedAttackSO.spread, rangedAttackSO.spread);
            angle += randomSpread;

            // rangedAttackSO와 angle을 받아와서 투사체를 생성.
            CreateProjectile(rangedAttackSO, angle);
        }

    }

    // 투사체 생성
    private void CreateProjectile(RangedAttackSO rangedAttackSO, float angle)
    {
        // 오브젝트 풀에서 오브젝트를 하나 가져옴.
        GameObject obj = GameManager.instance.ObjectPool.SpawnFromPool(rangedAttackSO.bulletNameTag);

        // 생성한 오브젝트의 위치를 spawn포지션으로 옮겨놓음.
        obj.transform.position = projectileSpawnPosition.position;

        // Attack을 위한 데이터를 전달하기 위해 투사체 controller를 가져옴.
        ProjectileController attackController = obj.GetComponent<ProjectileController>();

        // 투사체 회전한 벡터값과 투사체의 SO를 전달.
        attackController.InitializeAttack(RotateVector2(aimDirection, angle), rangedAttackSO);
    }

    // OnShoot에서 가져온 각도와 OnAim에서 가져온 direction을 통해 투사체를 회전시킴.
    private static Vector2 RotateVector2(Vector2 v, float angle)
    {
        return Quaternion.Euler(0f, 0f, angle) * v;
    }

}
