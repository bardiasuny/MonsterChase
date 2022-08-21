using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    private Transform player;
    private Vector3 tempPos;


    [SerializeField]
    private float minX, maxX;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;


    }

    // Update is called once per frame

    private void LateUpdate()


    {
        if (!player) return;

        tempPos = transform.position;
        tempPos.x = player.position.x;
        tempPos.y = player.position.y;

        if (player.position.x < minX)
        {
            tempPos.x = minX;
        }

        if (player.position.x > maxX)
        {
            tempPos.x = maxX;
        }

        transform.position = tempPos;

    }
   
}
