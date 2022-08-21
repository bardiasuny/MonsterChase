using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public bool isRight;

    public bool shoot;
    // Start is called before the first frame update
    void Start()
    {

    


    }

    // Update is called once per frame
    void Update()
    {
        if(shoot)
        transform.position += 30 * Time.deltaTime * new Vector3(isRight ? -1f : 1f, 0f, 0f);
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }

    }
}
