using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBlock : MonoBehaviour
{
    public GameObject Mushroom;
    public GameObject FireFlower;
    public GameObject Spawn;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Rigidbody2D playerRB = other.gameObject.GetComponent<Rigidbody2D>();

        if (rb.position.y - 0.6f > playerRB.position.y && PlayerBehaviour.playerState == 0) 
        {
            Instantiate(Mushroom, Spawn.transform.position, Quaternion.identity);
            GetComponent<SpriteRenderer>().color = Color.black;
            Destroy(this);
        }
        else if (rb.position.y - 0.6f > playerRB.position.y && PlayerBehaviour.playerState >= 1)
        {
            Instantiate(FireFlower, Spawn.transform.position, Quaternion.identity);
            GetComponent<SpriteRenderer>().color = Color.black;
            Destroy(this);
        } 
    }

    public void OnValidate()
    { 
        if (Mushroom == null)
        {
            Debug.LogWarning("Missing gameobject reference.", this);
        }

        if (FireFlower == null)
        {
            Debug.LogWarning("Missing gameobject reference.", this);
        }

        if (Spawn == null)
        {
            Debug.LogWarning("Missing child gameobject reference.", this);
        }
    }
}
