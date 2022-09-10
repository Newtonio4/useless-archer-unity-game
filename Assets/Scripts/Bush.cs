using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush : MonoBehaviour
{
    private float lastHit = 0;
    private float hitCooldown = 0.5f;
   
    protected virtual void ReceiveDamage(Damage dmg)
    {
        if (Time.time - lastHit > hitCooldown)
        {
            lastHit = Time.time;
            dmg.damageHandler.speed = dmg.damageHandler.speed / 2;
        }
    }
}
