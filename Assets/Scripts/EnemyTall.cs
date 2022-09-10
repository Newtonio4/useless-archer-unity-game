using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTall : Enemy
{
    private Animator animator;
    private bool top = false;

    protected override void Start()
    {
        base.Start();

        animator = GetComponent<Animator>();
    }
    protected override void Move()
    {
        if (top)
            animator.SetTrigger("StretchBottom");
        else
            animator.SetTrigger("StretchTop");

        top = !top;
    }
}
