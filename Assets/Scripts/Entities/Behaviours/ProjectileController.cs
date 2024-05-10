using System;
using UnityEngine;

/*
 * ProjectileController
 * 투사체에 대한 로직 수행
 * 투사체가 벽에 부딪히거나, 오브젝트에 닿으면 수행되는 로직
 * 오브젝트에 부딪히면 파괴, 대상이 적이면 넉백주기.
 */
public class ProjectileController : MonoBehaviour
{
    // 레벨의 layerMask 담음.
    [SerializeField] private LayerMask levelCollisionLayer;

    // 투사체의 값들을 getComponent로 담아옴.
    private Rigidbody2D rigidbody;
    private SpriteRenderer spriteRenderer;
    private TrailRenderer trailRenderer;

    // 투사체 데이터
    private RangedAttackSO attackData;
    private Vector2 direction;  // 투사체의 벡터값(투사체 발사 방향 + 속도)

    private float currentDuration;
    private bool isReady;

    private bool fxOnDestroy = true;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
        trailRenderer = GetComponent<TrailRenderer>();
    }

    private void Update()
    {
        if(!isReady)
        {
            return;
        }

        currentDuration += Time.deltaTime;

        
        if(currentDuration > attackData.duration)
        {
            // 현재 화살의 지속시간이 attackData에 담겨있는 지속시간보다 크면, 투사체 삭제시킴.
            DestroyProjectile(transform.position, false);
        }

        // 지속시간이 괜찮으면 그냥 그대로 speed값에 따라 발사.
        rigidbody.velocity = direction * attackData.speed;
    }

    // 투사체 파괴 로직
    private void DestroyProjectile(Vector3 position, bool createFx)
    {
        //시각효과가 있는지 판단.
        if(createFx)
        {
            // TODO : ParticleSystem에 대해서 배우고, 무기 NameTag로 해당하는 Fx 가져오기.
        }

        //오브젝트 비활성화
        gameObject.SetActive(false);
    }

    // TopDownShooting에서 투사체의 벡터값(투사체가 나가는 방향)과 투사체의 OS를 받아옴.
    public void InitializeAttack(Vector2 direction, RangedAttackSO attackData)
    {
        this.attackData = attackData;
        this.direction = direction;

        // 발사하자마자 초기값 설정해주는 부분인듯.
        UpdateProjectileSprite();
        trailRenderer.Clear();
        currentDuration = 0;
        spriteRenderer.color = attackData.projectileColor;

        // 근데 왜 right지? 
        // -> 화살 촉이 방향 오른쪽이고, 그 방향을 발사뱡향과 맞춰줌.
        // right -> 빨간색 축 -> x축
        transform.right = this.direction;

        // 발사 되었으면 isReady -> true로
        isReady = true;
    }

    // 이미지를 추가하는 부분.. (추후에 다시 물어봐야겠음.)
    private void UpdateProjectileSprite()
    {
        transform.localScale = Vector3.one * attackData.size;
    }

    // 충돌되었을 때
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 만약에 바닥 LayerMask와 부딪힌 물체의 layerMask가 같다면
        if(IsLayerMatched(levelCollisionLayer.value, collision.gameObject.layer))
        {
            // destroyPosition에 collision의 위치값을 저장함.
            Vector2 destroyPosition = collision.ClosestPoint(transform.position) - direction * 0.2f;
            DestroyProjectile(destroyPosition, fxOnDestroy);
        }

        // 부딪힌 물체의 layer가 attackData에 저장된 target 값과 같으면
        else if(IsLayerMatched(attackData.target.value, collision.gameObject.layer))
        {
            HealthSystem healthSystem = collision.GetComponent<HealthSystem>();
            if(healthSystem != null)
            {
                // 데미지를 받을 수 있는 상태면 true값 저장됨 (받을 수 있는 상태는 ChangeHealth 내부적으로 관리)
                // -> 그냥 쉽게 보면 무적시간이면 false, 무적시간이 아니면 true
                bool isAttackApplied = healthSystem.ChangeHealth(-attackData.power);

                // 무적시간이 아니고, 넉백가능한 공격이면
                if(isAttackApplied && attackData.isOnKnockBack)
                {
                    // 넉백시킴
                    ApplyKnockback(collision);
                }
            }
            // 모든 로직 후 삭제
            DestroyProjectile(collision.ClosestPoint(transform.position), fxOnDestroy);
        }
    }

    // 넉백 적용하는거. 실제 넉백에 대한 로직은 아님.
    private void ApplyKnockback(Collider2D collision)
    {
        TopDownMovement movement = collision.GetComponent<TopDownMovement>();
        if(movement != null)
        {
            movement.ApplyKnockback(transform, attackData.knockbackPower, attackData.knockbackTime);
        }
    }

    // layerMask가 매치되는지 판단하는 거.
    private bool IsLayerMatched(int value, int layer)
    {
        return value == (value | 1 << layer);
    }
}