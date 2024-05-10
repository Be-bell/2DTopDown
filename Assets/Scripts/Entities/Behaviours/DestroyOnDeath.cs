using System;
using UnityEngine;

/*
 * DestroyOnDeath
 * -> Entity의 죽음을 관리.
 * 아마 여기서 Destroy보단 오브젝트 풀에다가 다시 넣으면 되지 않을까..? 싶음
 */
public class DestroyOnDeath : MonoBehaviour
{
    private HealthSystem healthSystem;
    private Rigidbody2D rigidbody;

    private void Start()
    {
        healthSystem = GetComponent<HealthSystem>();
        rigidbody = GetComponent<Rigidbody2D>();
        // healthSystem의 OnDeath 이벤트 안에 넣어둠.
        // healthSystem의 OnDeath가 실행되면 여기의 OnDeath 실행.
        healthSystem.OnDeath += OnDeath;
    }

    
    // 사망 메소드
    private void OnDeath()
    {
        // 움직임 멈춤
        rigidbody.velocity = Vector2.zero;

        // 자식 오브젝트들(하이어라키창에서 자식으로 있는 오브젝트들) 모두 정지, 색깔 변화.
        foreach(SpriteRenderer renderer in GetComponentsInChildren<SpriteRenderer>())
        {
            Color color = renderer.color;
            color.a = 0.3f;
            renderer.color = color;
        }

        foreach(Behaviour behaviour in GetComponentsInChildren<Behaviour>())
        {
            behaviour.enabled = false;
        }

        // 2초 뒤 삭제
        Destroy(gameObject, 2f);
    }
}