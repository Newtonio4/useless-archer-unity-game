using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : Enemy
{
    public bool up;

    private Animator animator;

    protected override void Start()
    {
        base.Start();

        animator = GetComponent<Animator>();

        if (up)
            animator.SetTrigger("WizardUp");
    }
    protected override void Move()
    {
        if (up)
            animator.SetTrigger("WizardDown");
        else
            animator.SetTrigger("WizardUp");

        up = !up;
    }

    protected override void Die()
    {
        base.Die();
    }
}
