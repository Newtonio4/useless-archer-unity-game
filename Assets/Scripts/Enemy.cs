using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    protected float health = 1;
    protected float maxHealth = 1;

    protected virtual void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Player.OnShot += Move;
    }
    private void OnDisable()
    {
        Player.OnShot -= Move;
    }


    protected virtual void ReceiveDamage(Damage dmg)
    {
        health--;

        if (health <= 0)
            Die();
    }

    protected virtual void Die()
    {
        Player.OnShot -= Move;
        Destroy(gameObject);
        gameObject.transform.parent.GetComponent<EnemyCollector>().CheckChildren();
    }

    protected virtual void Move()
    {
        if (gameObject.GetComponent<Enemy>() != null)
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, 0);
    }
}
