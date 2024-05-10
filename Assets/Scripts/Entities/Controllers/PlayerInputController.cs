using UnityEngine;
using UnityEngine.InputSystem;

/*
 * PlayerInputController -> input 받아서 변수 전달
 * InputManager 통해서 변수를 전달받으면
 * 그 변수를 통해 TopDownController 안의 Call 함수 호출
 * TopDownController는 상속받음 => TopDownController에서 stats를 같이 사용하기 때문에
 * 상속받아 사용하는 것이 조금 더 나아보여서 그렇게 한듯?
 * Call 함수 호출하면, 변수를 안에 있는 메소드에 다 보내주고, 그 메소드들 실행시켜줌.
 * OnMove, OnLook, OnFire 통해서 Input받음. (InputManager 내장함수)
 */

public class PlayerInputController : TopDownController
{
    // 마우스 위치 파악(마우스 위치는 카메라의 localPosition)
    private Camera camera;

    protected override void Awake()
    {
        base.Awake();
        camera = Camera.main;
    }

    // Input을 받아서 전달. (안에 있던 함수 호출됨)
    public void OnMove(InputValue value)
    {
        Vector2 moveInput = value.Get<Vector2>().normalized;
        CallMoveEvent(moveInput);
    }

    // Input = mousePosition (LocalPosition)
    public void OnLook(InputValue value)
    {
        // newAim = 마우스의 위치
        Vector2 newAim = value.Get<Vector2>();

        // newAim을 GlobalPosition으로 변환
        Vector2 worldPos = camera.ScreenToWorldPoint(newAim);

        // 방향값만 얻어오면 됨. (벡터의 뺄셈)
        newAim = (worldPos - (Vector2)transform.position).normalized;

        CallLookEvent(newAim);
    }

    // input = 마우스 왼쪽 클릭.
    public void OnFire(InputValue value)
    {
        IsAttacking = value.isPressed;
    }
}
