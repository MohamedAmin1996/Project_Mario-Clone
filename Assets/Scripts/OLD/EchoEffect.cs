using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoEffect : MonoBehaviour
{
    private float timeBtwSpawn;
    public float startTimeBtwSpawn;

    public GameObject echo;
    private PlayerBehaviour player;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {

        if(player.moveInput != 0)
        {
            if (timeBtwSpawn <= 0)
            {
                GameObject instance = (GameObject)Instantiate(echo, transform.position, Quaternion.identity);
                Destroy(instance, 8f);
                timeBtwSpawn = startTimeBtwSpawn;
            }
            else
            {
                timeBtwSpawn -= Time.deltaTime;
            }
        }

        
    }
}
