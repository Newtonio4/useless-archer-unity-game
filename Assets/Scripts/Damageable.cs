using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    public Sprite dmg1;
    public Sprite dmg2;

    private SpriteRenderer spriteRenderer;
    private int health = 3;
    private int maxHealth = 3;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected virtual void ReceiveDamage(Damage dmg)
    {
        health--;

        if (health <= 0)
            Die();

        if (health < maxHealth)
        {
            if ((float)health/maxHealth > 0.35f)
                spriteRenderer.sprite = dmg1;
            else
                spriteRenderer.sprite = dmg2;
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
