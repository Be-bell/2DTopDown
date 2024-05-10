using UnityEngine;

/*
 * Animator와 Controller만 받아오는 거..
 * 사실 얘를 상속하여 쓰려고 이렇게 해놓은듯.
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
