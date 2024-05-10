using System;
using UnityEngine;

/*
 * TopDownMovement = 움직임 로직 구현
 * 움직이는 것을 담당함.
 * getComponent를 통해 여러 값들을 받아옴. (근데 statHandler 왜받아왔을까?)
 * 입력값에 따라 knockback도 구현할 수 있음.
 * 아마 움직이는 모든 것을 여기다가 넣을 수도 있을듯.. (넉백 말고도 슬로우라던지.. 속박이라던지..)
 */
public class TopDownMovement : MonoBehaviour
{
    private TopDownController controller;
    private Rigidbody2D movementRigidbody;
    private CharacterStatHandler characterStatHandler;

    private Vector2 movementDirection = Vector2.zero;
    private Vector2 knockback = Vector2.zero;
    private float knockbackDuration = 0.0f;

    private void Awake()
    {
        controller = GetComponent<TopDownController>();
        movementRigidbody = GetComponent<Rigidbody2D>();
        characterStatHandler = GetComponent<CharacterStatHandler>();
    }

    // OnMoveEvent에 Move를 넣어줌.
    private void Start()
    {
        controller.OnMoveEvent += Move;
    }

    // 변수를 받아와서 필드에 등록해주는 역할
    private void Move(Vector2 direction)
    {
        movementDirection = direction;
    }
    
    // 물리연산은 FixedUpdate에서
    private void FixedUpdate()
    {
        ApplyMovement(movementDirection);

        if(knockbackDuration > 0.0f)
        {
            knockbackDuration -= Time.fixedDeltaTime;
        }
    }

    // 실제 넉백 설정.
    public void ApplyKnockback(Transform Other, float power, float duration)
    {
        knockbackDuration = duration;
        knockback = -(Other.position - transform.position).normalized * power;
    }

    // 변수 벡터(direction)를 캐릭터(movementRigidbody)에게 적용시킴.
    private void ApplyMovement(Vector2 direction)
    {
        direction = direction * characterStatHandler.CurrentStat.speed;

        if (knockbackDuration > 0.0f)
        {
            direction += knockback;
        }

        movementRigidbody.velocity = direction;
    }

}
