using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    [Tooltip("Left side of the camera")]
    public float xMin;
    [Tooltip("Right side of the camera")]
    public float xMax;
    [Tooltip("Top side of the camera")]
    public float yMin;
    [Tooltip("Bottom side of the camera")]
    public float yMax;
    [Tooltip("How far back the player can go (Cannot be a negative number.)(Is used for Mario Camera)")]
    public float offset;
    [Tooltip("If checked, the player cannot go back like in Super Mario Bros.")]
    public bool marioCamera;

    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void LateUpdate()
    {
        float x = Mathf.Clamp(player.transform.position.x, xMin, xMax);
        float y = Mathf.Clamp(player.transform.position.y, yMin, yMax);
        gameObject.transform.position = new Vector3(x, y, gameObject.transform.position.z);

        if(marioCamera == true)
        {
            xMin = gameObject.transform.position.x;
            if(player.transform.position.x < xMin - offset)
            {
                player.transform.position = new Vector3(xMin - offset, player.transform.position.y, player.transform.position.y);
            }
        }
  
    }

    public void OnValidate()
    {
        if (offset < 0.0f)
        {
            offset = 0.0f;
            Debug.LogWarning("Offset must have a non negative value.", this);
        }
    }
}
