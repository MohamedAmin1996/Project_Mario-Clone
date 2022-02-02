using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [Tooltip("The enemy movement speed (Cannot be a negative number.)")]
    public float speed;
    [Tooltip("Check the box and the enemy will start it's movement from the right direction. If left unchecked the enemy will start it's movement from the left direction.")]
    public bool movingRight;
    [Tooltip("The enemy will move to the other direction if it's about to fall down. If left unchecked the enemy will fall down to the pit of void. (Enemy will be blue)")]
    public bool canFlip;

    [Tooltip("An object that's detecting if there's no platform ahead.")]
    public Transform groundDetection;
    [Tooltip("Looking for a ground layer. It's used for the (Ground Detection) object")]
    public LayerMask whatIsGround;
    [Tooltip("Distance between the enemy and the ground (Cannot be a negative number).")]
    public float RaycastDistance;

    private Rigidbody2D rb;
    private bool isDead;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        flip();
        isDead = false;
    }

    void Update()
    {
        if (canFlip == true)
        {
            RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, RaycastDistance, whatIsGround);
            Debug.DrawRay(groundDetection.position, Vector2.down, Color.blue);
            GetComponent<SpriteRenderer>().color = Color.blue;

            if (groundInfo.collider == false)
            {
                flip();
            }
        }

        move();

        if (gameObject.transform.position.y < -9 && !isDead)
        {
            Die();
        }

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == 8 || other.gameObject.tag == "Enemy")
        {
            flip();
        }

        if (other.gameObject.tag == "Player")
        {
            Rigidbody2D playerRB = other.gameObject.GetComponent<Rigidbody2D>();
            if (rb.position.y + 0.3f < playerRB.position.y)
            {
                isDead = true;
                other.gameObject.GetComponent<PlayerScore>().addScore(100);
                speed = 0;
                rb.transform.localScale = new Vector3(1f, 0.5f, 1f);
                rb.transform.localPosition = new Vector2(rb.position.x, rb.position.y - 0.25f);
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
                playerRB.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 500);
                GetComponent<CircleCollider2D>().enabled = false;
                Invoke("Die", 0.5f);
            }
        }

        if (other.gameObject.tag == "Projectile")
        {
            isDead = true;
            other.gameObject.GetComponent<FireBall>().score.addScore(100);
            speed = 0;
            rb.transform.localScale = new Vector3(1f, 0.5f, 1f);
            rb.transform.localPosition = new Vector2(rb.position.x, rb.position.y - 0.30f);
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            GetComponent<CircleCollider2D>().enabled = false;
            Invoke("Die", 0.15f);
        }

    }

    void move()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    void flip()
    {
        if (movingRight == true)
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
            movingRight = false;
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            movingRight = true;
        }
    }

    void Die()
    {
        Destroy(this.gameObject);
    }

    public void OnValidate()
    {
        if (speed < 0.0f)
        {
            speed = 0.0f;
            Debug.LogWarning("Speed must have a non negative value.", this);
        }

        if (whatIsGround == 0)
        {
            Debug.LogWarning("Missing ground layer", this);
        }

        if (groundDetection == null)
        {
            Debug.LogWarning("Missing child gameobject reference.", this);
        }

        if (RaycastDistance < 0.0f)
        {
            RaycastDistance = 0.0f;
            Debug.LogWarning("RaycastDistance must have a non negative value.", this);
        }
    }
}
