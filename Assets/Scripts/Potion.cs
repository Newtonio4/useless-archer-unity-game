using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    public ArrowType potionType;

    private Player player;

    //Event
    public delegate void PotionAction();
    public static event PotionAction OnCollect;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected virtual void ReceiveDamage(Damage dmg)
    {
        player.arrowType = potionType;

        if (OnCollect != null)
            OnCollect();

        DestroyPotion();
    }

    private void DestroyPotion()
    {
        Destroy(gameObject);
    }
}
