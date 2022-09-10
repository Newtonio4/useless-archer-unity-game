using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArmored : Enemy
{
    public Sprite damaged;

    private SpriteRenderer spriteRenderer;

    protected override void Start()
    {
        base.Start();

        health = 2;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    protected override void ReceiveDamage(Damage dmg)
    {
        base.ReceiveDamage(dmg);

        spriteRenderer.sprite = damaged;
    }
}
