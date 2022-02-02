using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other) 
    {
        if (PlayerBehaviour.playerState == 0)
        {
            PlayerBehaviour.playerState = 1;
            other.gameObject.GetComponent<PlayerScore>().addScore(1000);
        }
        if (PlayerBehaviour.playerState == 1)
        {
            other.gameObject.GetComponent<PlayerScore>().addScore(1000);
        }
        Destroy(this.gameObject);
    }
}
