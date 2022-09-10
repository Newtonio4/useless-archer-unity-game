using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public GameObject brotherPortal;

    private float lastHit = 0;
    private float hitCooldown = 0.5f;

    protected virtual void ReceiveDamage(Damage dmg)
    {
        if (Time.time - lastHit > hitCooldown)
        {
            lastHit = Time.time;

            var arrow = dmg.damageHandler.transform;
            var dist = arrow.position - transform.position;

            arrow.position += brotherPortal.transform.position - transform.position - dist * 2.1f;
        }
    }
}
