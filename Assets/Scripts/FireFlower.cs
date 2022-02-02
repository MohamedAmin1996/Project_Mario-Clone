using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFlower : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other) 
    { 
        if (PlayerBehaviour.playerState == 1)
        {
            PlayerBehaviour.playerState = 2;
            other.gameObject.GetComponent<PlayerScore>().addScore(1000);
        }
        if (PlayerBehaviour.playerState == 2)
        {
            other.gameObject.GetComponent<PlayerScore>().addScore(1000);
        }
        Destroy(this.gameObject);
    }
}
