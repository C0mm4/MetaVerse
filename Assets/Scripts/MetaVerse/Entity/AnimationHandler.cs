using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    private static readonly int IsMoving = Animator.StringToHash("IsMove");
    private static readonly int IsJump = Animator.StringToHash("IsJump");

    protected Animator animator;

    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void Move(Vector2 dir)
    {
        animator.SetBool(IsMoving, dir.magnitude > .5f);
    }

    public void Jump()
    {
        animator.SetTrigger(IsJump);
    }
}
