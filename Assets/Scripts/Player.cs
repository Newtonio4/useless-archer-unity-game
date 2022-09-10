using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum ArrowType
{
    Default,
    Tripple,
    Long,
    Invulnerable
}

public class Player : MonoBehaviour
{
    public Arrow arrowPrefab;
    public Sprite arrow1;
    public Sprite arrow2;
    public Sprite arrow3;
    public ArrowType arrowType = ArrowType.Default;
    public int arrows = 0;

    //Sounds
    public AudioClip shotSound;
    private AudioSource audioSource;

    //Event
    public delegate void ShotAction();
    public static event ShotAction OnShot;

    private Animator animator;
    private Transform cross;
    private BoxCollider2D boxCollider;
    private RaycastHit2D hit;

    //Shot
    private float shootingCooldown = 0.5f;
    private float lastShoot;

    private void Start()
    {
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        cross = transform.GetChild(0);
        audioSource = Camera.main.GetComponent<AudioSource>();

        Cursor.visible = false;

        GameManager.isGameActive = true;
    }

    void Update()
    {
        //Movement
        Vector3 movement = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);

        //Animation
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Magnitude", movement.magnitude);

        //Collision
        hit = Physics2D.BoxCast(new Vector2(transform.position.x + boxCollider.offset.x, transform.position.y + boxCollider.offset.y), boxCollider.size, 0, new Vector2(movement.x, 0), Mathf.Abs(movement.x * Time.deltaTime), LayerMask.GetMask("Fence", "Blocking"));
        if (hit.collider == null)
        {
            transform.position += new Vector3(movement.x * Time.deltaTime, 0, 0);
        }

        hit = Physics2D.BoxCast(new Vector2(transform.position.x + boxCollider.offset.x, transform.position.y + boxCollider.offset.y), boxCollider.size, 0, new Vector2(0, movement.y), Mathf.Abs(movement.y * Time.deltaTime), LayerMask.GetMask("Fence", "Blocking"));
        if (hit.collider == null)
        {
            transform.position += new Vector3(0, movement.y * Time.deltaTime, 0);
        }

        //Fire
        if (Input.GetButton("Fire") && Time.time - lastShoot > shootingCooldown && GameManager.isGameActive)
        {
            lastShoot = Time.time;

            Fire();
        }

        //Cross positioning
        var mousePos = Input.mousePosition;
        cross.position = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Camera.main.nearClipPlane));
    }

    private void Fire()
    {
        //Count arrows used on the level
        arrows++;

        if (arrowType == ArrowType.Default)
        {
            var aiming = new Vector2(cross.position.x - transform.position.x, cross.position.y - transform.position.y).normalized;

            Arrow arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
            arrow.transform.position = new Vector2(transform.position.x, transform.position.y) + aiming / 7;
            arrow.transform.Rotate(0, 0, Mathf.Atan2(aiming.y, aiming.x) * Mathf.Rad2Deg);

            arrow.direction = aiming;

            Destroy(arrow.gameObject, 10);
        }
        else if (arrowType == ArrowType.Tripple)
        {
            var firstAiming = new Vector2(cross.position.x - transform.position.x, cross.position.y - transform.position.y).normalized;
            var angle = Mathf.Atan2(firstAiming.y, firstAiming.x);
            var angles = new float[3] { angle - 0.2f, angle, angle + 0.2f };

            for (int i = 0; i < 3; i++)
            {
                var aiming = new Vector2(Mathf.Cos(angles[i]), Mathf.Sin(angles[i]));
                Arrow arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
                arrow.transform.position = new Vector2(transform.position.x, transform.position.y) + aiming / 7;
                arrow.transform.Rotate(0, 0, Mathf.Atan2(aiming.y, aiming.x) * Mathf.Rad2Deg);
                arrow.direction = aiming;
                arrow.spriteRenderer.sprite = arrow1;
                Destroy(arrow.gameObject, 10);
            }
        }
        else if (arrowType == ArrowType.Long)
        {
            var aiming = new Vector2(cross.position.x - transform.position.x, cross.position.y - transform.position.y).normalized;

            Arrow arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
            arrow.transform.position = new Vector2(transform.position.x, transform.position.y) + aiming / 7;
            arrow.transform.Rotate(0, 0, Mathf.Atan2(aiming.y, aiming.x) * Mathf.Rad2Deg);

            arrow.direction = aiming;
            arrow.spriteRenderer.sprite = arrow2;
            arrow.speed = 6;
            arrow.maxHits = 5;

            Destroy(arrow.gameObject, 15);
        }
        else if (arrowType == ArrowType.Invulnerable)
        {
            var aiming = new Vector2(cross.position.x - transform.position.x, cross.position.y - transform.position.y).normalized;

            Arrow arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
            arrow.transform.position = new Vector2(transform.position.x, transform.position.y) + aiming / 7;
            arrow.transform.Rotate(0, 0, Mathf.Atan2(aiming.y, aiming.x) * Mathf.Rad2Deg);

            arrow.direction = aiming;
            arrow.spriteRenderer.sprite = arrow3;
            arrow.invulnerable = true;
            arrow.speed = 1;
            arrow.maxHits = 2;

            Destroy(arrow.gameObject, 10);
        }

        //Play sound
        audioSource.PlayOneShot(shotSound, GameManager.effectsVolume);

        //Return arrow type to default after a shot
        arrowType = ArrowType.Default;

        if (OnShot != null)
            OnShot();
    }
}
