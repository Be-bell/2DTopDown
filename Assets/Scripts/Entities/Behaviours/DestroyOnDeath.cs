using System;
using UnityEngine;

/*
 * DestroyOnDeath
 * -> Entity�� ������ ����.
 * �Ƹ� ���⼭ Destroy���� ������Ʈ Ǯ���ٰ� �ٽ� ������ ���� ������..? ����
 */
public class DestroyOnDeath : MonoBehaviour
{
    private HealthSystem healthSystem;
    private Rigidbody2D rigidbody;

    private void Start()
    {
        healthSystem = GetComponent<HealthSystem>();
        rigidbody = GetComponent<Rigidbody2D>();
        // healthSystem�� OnDeath �̺�Ʈ �ȿ� �־��.
        // healthSystem�� OnDeath�� ����Ǹ� ������ OnDeath ����.
        healthSystem.OnDeath += OnDeath;
    }

    
    // ��� �޼ҵ�
    private void OnDeath()
    {
        // ������ ����
        rigidbody.velocity = Vector2.zero;

        // �ڽ� ������Ʈ��(���̾��Űâ���� �ڽ����� �ִ� ������Ʈ��) ��� ����, ���� ��ȭ.
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

        // 2�� �� ����
        Destroy(gameObject, 2f);
    }
}