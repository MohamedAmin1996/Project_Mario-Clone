using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    [Tooltip("How fast the fireball can move (Cannot be a negative number.)")]
    public float speed;
    private GameObject player;
    private int direction;
    [HideInInspector]
    public PlayerScore score;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        move();
        deleteFireBall();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Wall")
        {
            Destroy(this.gameObject);
        }

        if (other.gameObject.tag == "Enemy")
        {
            Destroy(this.gameObject);
        }
    }

    void move() 
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime * direction);
    }

    void deleteFireBall()
    {
        if (gameObject.transform.position.x > player.transform.position.x + 13)
        {
            Destroy(this.gameObject);
        }

        if (gameObject.transform.position.x < player.transform.position.x - 13)
        {
            Destroy(this.gameObject);
        }

        if (gameObject.transform.position.y < -9) 
        {
            Destroy(this.gameObject);
        }
    }

    public void setDirection(int direction)
    {
        this.direction = direction;
    }

    public void OnValidate()
    {
        if (speed < 0.0f)
        {
            speed = 0.0f;
            Debug.LogWarning("Speed must have a non negative value.", this);
        }
    }
}
