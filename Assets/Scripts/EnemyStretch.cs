using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStretch : Enemy
{
    private Animator animator;
    private bool right = false;

    protected override void Start()
    {
        base.Start();

        animator = GetComponent<Animator>();
    }
    protected override void Move()
    {
        if (right)
            animator.SetTrigger("StretchLeft");
        else
            animator.SetTrigger("StretchRight");

        right = !right;
    }
}
