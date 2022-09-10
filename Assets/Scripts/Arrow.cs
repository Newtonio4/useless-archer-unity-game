using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    //Particles
    public ParticleSystem stoneParticle;
    public ParticleSystem bushParticle;
    public ParticleSystem boxParticle;
    public ParticleSystem bloodParticle;
    public ParticleSystem potionParticle;

    //Sounds
    public AudioClip hitStone;
    public AudioClip hitEnemy;
    public AudioClip hitBox;
    public AudioClip hitBush;
    public AudioClip arrowStick;
    public AudioClip collect;
    private AudioSource audioSource;

    public SpriteRenderer spriteRenderer;
    public Vector3 direction;
    public int hits = 0;
    public int maxHits = 3;
    public float speed = 2;
    public bool invulnerable = false;

    protected BoxCollider2D boxCollider;
    protected RaycastHit2D hit;
    protected float lastImmune = 0;
    protected float immuneTime = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = Camera.main.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var moveDelta = direction * speed;

        hit = Physics2D.BoxCast(new Vector2(transform.position.x + boxCollider.offset.x, transform.position.y + boxCollider.offset.y), boxCollider.size, 0, new Vector2(moveDelta.x, moveDelta.y), Vector3.Magnitude(moveDelta * Time.deltaTime), LayerMask.GetMask("Blocking"));
        if (hit.collider == null || Time.time - lastImmune < immuneTime || hits == maxHits)
        {
            transform.position += new Vector3(moveDelta.x * Time.deltaTime, moveDelta.y * Time.deltaTime, 0);
        }
        else if (invulnerable)
        {
            transform.position += new Vector3(moveDelta.x * Time.deltaTime, moveDelta.y * Time.deltaTime, 0);

            if (hit.collider != null && hit.collider.gameObject.tag == "Enemy")
            {
                SendDamage(1.0f);
                //GotHit(hit.normal, hit.collider.gameObject.tag);
                audioSource.PlayOneShot(hitEnemy, GameManager.effectsVolume);

                if (GameManager.bloodParticle)
                    PlayParticle(bloodParticle);
            }
        }
        else
        {
            if (hit.collider.gameObject.tag == "Enemy")
            {
                SendDamage(1.0f);
                GotHit(hit.normal, hit.collider.gameObject.tag);

                if (GameManager.bloodParticle)
                    PlayParticle(bloodParticle);
            }
            else if (hit.collider.gameObject.tag == "Box")
            {
                SendDamage(1.0f);
                GotHit(hit.normal, hit.collider.gameObject.tag);

                PlayParticle(boxParticle);
            }
            else if (hit.collider.gameObject.tag == "Bush")
            {
                SendDamage(0.0f);
                GotHit(hit.normal, hit.collider.gameObject.tag);

                PlayParticle(bushParticle);
            }
            else if (hit.collider.gameObject.tag == "Potion")
            {
                SendDamage(0.0f);
                PlayParticle(potionParticle);
                audioSource.PlayOneShot(collect, GameManager.effectsVolume);
            }
            else if (hit.collider.gameObject.tag == "Portal")
            {
                SendDamage(0.0f);
                audioSource.PlayOneShot(collect, GameManager.effectsVolume);
            }
            else
            {
                GotHit(hit.normal, " ");

                PlayParticle(stoneParticle);
            }
        }
    }

    private void PlayParticle(ParticleSystem particlePrefab)
    {
        var angle = Mathf.Atan2(hit.normal.y, hit.normal.x);
        var rotation = Quaternion.Euler(Mathf.Rad2Deg * Mathf.Atan2(hit.normal.y, hit.normal.x), 90, -90);

        if (angle - 90 < 1 || angle + 90 < 1)
            rotation = Quaternion.Euler(-Mathf.Rad2Deg * Mathf.Atan2(hit.normal.y, hit.normal.x), 90, -90);

        var particle = Instantiate(particlePrefab, new Vector3(hit.point.x, hit.point.y, 0), rotation);
        Destroy(particle, 2);
    }

    private void SendDamage(float damage)
    {
        Damage dmg = new Damage
        {
            damageAmount = damage,
            pushForce = 0,
            damagePosition = transform.position,
            damageHandler = gameObject.GetComponent<Arrow>()
        };

        hit.collider.SendMessage("ReceiveDamage", dmg);
    }

    protected void GotHit(Vector2 normal, string tag)
    {
        if (invulnerable)
            return;

        hits++;

        if (hits < maxHits)
        {
            lastImmune = Time.time;
            direction = Vector3.Reflect(direction, normal).normalized;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            switch (tag)
            {
                case "Enemy":
                    audioSource.PlayOneShot(hitEnemy, GameManager.effectsVolume);
                    break;
                case "Box":
                    audioSource.PlayOneShot(hitBox, GameManager.effectsVolume);
                    break;
                case "Bush":
                    audioSource.PlayOneShot(hitBush, GameManager.effectsVolume);
                    break;
                default:
                    audioSource.PlayOneShot(hitStone, GameManager.effectsVolume);
                    break;
            }
        }
        else
        {
            direction = Vector3.zero;
            audioSource.PlayOneShot(arrowStick, GameManager.effectsVolume);
        }
    }
}
