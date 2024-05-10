using System;
using UnityEngine;

/*
 * TopDownAimRotation = 회전 로직 관여
 * Vector값을 이벤트를 통해 가져오고
 * Renderer를 inspector에서 대입받음.
 * Renderer를 vector값에 따라 회전시킴.
 */
public class TopDownAimRotation : MonoBehaviour
{
    // arm -> 무기
    [SerializeField] private SpriteRenderer armRenderer;
    [SerializeField] private Transform armPivot;

    // character -> 캐릭터 renderer
    [SerializeField] private SpriteRenderer characterRenderer;

    // TopDownController를 Awake를 통해서 받아옴. (굳이 stats를 안써서 받아오는듯)
    private TopDownController controller;

    private void Awake()
    {
        controller = GetComponent<TopDownController>();
    }

    private void Start()
    {
        controller.OnLookEvent += OnAim;
    }

    // OnAim -> 변수를 받아와서 RotateArm 메소드에 대입함.
    private void OnAim(Vector2 newAimDirection)
    {
        RotateArm(newAimDirection);
    }

    // RotateArm -> 변수 받아와서 회전시켜줌.
    private void RotateArm(Vector2 direction)
    {
        // 먼저 받아온 Vector값으로 이뤄진 각을 구함.
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // 각에 따라 무기나 캐릭터의 renderer를 뒤집어줌.
        characterRenderer.flipX = Mathf.Abs(rotZ) > 90f;
        armRenderer.flipY = characterRenderer.flipX;

        // pivot값을 회전시켜줌.
        armPivot.rotation = Quaternion.Euler(0, 0, rotZ);
    }
}
