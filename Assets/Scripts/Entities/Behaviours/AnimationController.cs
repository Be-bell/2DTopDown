using UnityEngine;

/*
 * Animator�� Controller�� �޾ƿ��� ��..
 * ��� �긦 ����Ͽ� ������ �̷��� �س�����.
 */
public class AnimationController : MonoBehaviour
{
    protected Animator animator;
    protected TopDownController controller;

    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        controller = GetComponent<TopDownController>();
    }
}
