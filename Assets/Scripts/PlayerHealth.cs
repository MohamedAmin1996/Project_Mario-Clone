using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [Range(0.0f, 1.0f)]
    [Tooltip("How long the player can stay alive after being hit")]
    public float InvincibilitySeconds;
    private Rigidbody2D rb;
    private Color regularColor = new Color(0.66f, 0.66f, 0.66f, 1f);
    private bool invincible;
    public string resetSceneName;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        invincible = false;
    }

    void Update()
    {
        if (gameObject.transform.position.y < -9)
        {
            Die();
        }
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Rigidbody2D enemyRB = other.gameObject.GetComponent<Rigidbody2D>();
            if (invincible == true)
            {
                return;
            }
            else
            {
                if (rb.position.y > enemyRB.position.y + 0.3f)
                {
                    return;
                }
                
                if (PlayerBehaviour.playerState == 0)
                {
                    Die();
                }

                if (PlayerBehaviour.playerState == 2)
                {
                    invincible = true;
                    PlayerBehaviour.playerState = 0;
                    GetComponent<SpriteRenderer>().color = regularColor;
                    Invoke("StayAlive", InvincibilitySeconds);
                }

                else if (PlayerBehaviour.playerState == 1)
                {
                    invincible = true;
                    PlayerBehaviour.playerState = 0;
                    GetComponent<SpriteRenderer>().color = regularColor;
                    Invoke("StayAlive", InvincibilitySeconds);
                }
            }
        }
    }

    void StayAlive()
    {
        invincible = false;
    }

    void Die()
    {
        SceneManager.LoadScene(resetSceneName);
    }
}
