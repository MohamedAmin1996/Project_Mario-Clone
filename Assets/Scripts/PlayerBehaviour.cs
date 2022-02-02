using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [Tooltip("How fast the player can move (Cannot be a negative number.)")]
    public float speed;
    [Tooltip("How high the player can jump (Cannot be a negative number.)")]
    public float jumpForce;
    private Rigidbody2D rb;
    public float moveInput;

    private RaycastHit2D isGrounded;
    private RaycastHit2D isGroundedLeft;
    private RaycastHit2D isGroundedRight;
    [Tooltip("Looking for a ground layer. It's used for the (feetpos) object")]
    public LayerMask whatIsGround;
    [Tooltip("An object that's detecting if the player's on the ground")]
    public Transform feetPos;
    [Tooltip("Distance between the player and the ground (Cannot be a negative number).")]
    public float RaycastDistance;

    [Tooltip("How long the player can jump (Cannot be a negative number.)")]
    public float jumpTime;
    private float jumpTimeCounter;
    private bool isJumping;

    public static int playerState;
    public GameObject Spawn;
    public GameObject fireBall;
    [Range(0.0f, 1.0f)]
    [Tooltip("Fireball cooldown")]
    public float FireTimeCounter;
    private float currentTime;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerState = 0;
    }

    void FixedUpdate()
    {
        moveInput = Input.GetAxis("Horizontal");

        move();

        if (Input.GetKey(KeyCode.Z))
        {
            run();
        }
    }

    void Update()
    {
        flip();
        jump();
        state();
        shoot();
    }

    void state()
    {
        if (playerState == 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            GetComponent<TrailRenderer>().enabled = false;
        }
        else if (playerState == 1)
        {
            transform.localScale = new Vector3(1f, 1.5f, 1f);
            GetComponent<TrailRenderer>().enabled = true;
            GetComponent<TrailRenderer>().startColor = new Color(0.66f, 0.66f, 0.66f, 1f);
            GetComponent<TrailRenderer>().endColor = new Color(0.66f, 0.66f, 0.66f, 0f);
            GetComponent<TrailRenderer>().startWidth = 0.75f;
            GetComponent<TrailRenderer>().endWidth = 0f;
            GetComponent<TrailRenderer>().time = 0.25f;
        }
        else if (playerState == 2)
        {
            transform.localScale = new Vector3(1f, 1.5f, 1f);
            GetComponent<SpriteRenderer>().color = new Color(1f, 0.63f, 0f, 1f);

            GetComponent<TrailRenderer>().startColor = new Color(1f, 0.63f, 0f, 1f);
            GetComponent<TrailRenderer>().endColor = new Color(1f, 0.63f, 0f, 0f);
            GetComponent<TrailRenderer>().startWidth = 0.75f;
            GetComponent<TrailRenderer>().endWidth = 0f;
            GetComponent<TrailRenderer>().time = 0.25f;
        }
    }

    void move()
    {
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
    }

    void run()
    {
        rb.velocity = new Vector2(moveInput * (speed * 1.5f), rb.velocity.y);
    }

    void flip()
    {
        if (moveInput > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (moveInput < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }

    void jump()
    {
        Vector2 offset = new Vector2(0.4f, 0f);

        isGrounded = Physics2D.Raycast(feetPos.position, Vector2.down, RaycastDistance, whatIsGround);
        Debug.DrawRay(feetPos.position, Vector2.down * RaycastDistance, Color.red);

        isGroundedLeft = Physics2D.Raycast((Vector2)feetPos.position + (offset * -1), Vector2.down, RaycastDistance, whatIsGround);
        Debug.DrawRay((Vector2)feetPos.position + (offset * -1), Vector2.down * RaycastDistance, Color.green);

        isGroundedRight = Physics2D.Raycast((Vector2)feetPos.position + (offset), Vector2.down, RaycastDistance, whatIsGround);
        Debug.DrawRay((Vector2)feetPos.position + (offset), Vector2.down * RaycastDistance, Color.blue);

        if ((isGrounded.collider != null || isGroundedLeft.collider != null || isGroundedRight.collider != null) && Input.GetKeyDown(KeyCode.X))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
        }

        if (Input.GetKey(KeyCode.X) && isJumping == true)
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.X))
        {
            isJumping = false;
        }
    }

    void shoot()
    {
        if (playerState == 2 && Input.GetKeyDown(KeyCode.Z) && currentTime > FireTimeCounter)
        {
            int temp = 0;

            if (transform.eulerAngles.y == 0)
            {
                temp = 1;
            }
            else if (transform.eulerAngles.y == 180)
            {
                temp = -1;
            }

            GameObject fire = Instantiate(fireBall, Spawn.transform.position, Quaternion.identity);
            FireBall fireball = fire.GetComponent<FireBall>();
            fireball.setDirection(temp);
            fireball.score = GetComponent<PlayerScore>();
            currentTime = 0;
            
        }
        currentTime += Time.deltaTime;
    }

    public void OnValidate()
    {
        if (speed < 0.0f)
        {
            speed = 0.0f;
            Debug.LogWarning("Speed must have a non negative value.", this);
        }

        if (jumpForce < 0.0f)
        {
            jumpForce = 0.0f;
            Debug.LogWarning("JumpForce must have a non negative value.", this);
        }
        if (whatIsGround == 0)
        {
            Debug.LogWarning("Missing ground layer", this);
        }
        if (feetPos == null)
        {
            Debug.LogWarning("Missing child gameobject reference.", this);
        }

        if (RaycastDistance < 0.0f)
        {
            RaycastDistance = 0.0f;
            Debug.LogWarning("RaycastDistance must have a non negative value.", this);
        }

        if (jumpTime < 0.0f)
        {
            jumpTime = 0.0f;
            Debug.LogWarning("JumpTime must have a non negative value.", this);
        }

        if (Spawn == null)
        {
            Debug.LogWarning("Missing gameobject reference.", this);
        }

        if (fireBall == null)
        {
            Debug.LogWarning("Missing gameobject reference.", this);
        }
    }
}
