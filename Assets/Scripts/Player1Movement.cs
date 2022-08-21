using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Movement : MonoBehaviour
{
    public float speed = 5f;
    public float moveForce = 5f;
    public float JumpForce = 100f;
    public float maxVelocity = 20f;
    private float movementX;

    float smooth = 100f;
    private SpriteRenderer sr;
    private SpriteRenderer handGunSpriteRendere;

    private Rigidbody2D myBody;
    private Animator anim;

    private readonly string WALK_ANIMATION = "Walk";
    private readonly string JUMP_ANIMATION = "Jump";
    private readonly string KICK_ANIMATION = "Kick";
    private readonly string TAG_GROUND = "Ground";
    private readonly string TAG_ENEMY = "Enemy";
    private bool isGrounded;

    private bool isRightSide;

    [SerializeField]
    private GameObject handGun;

    [SerializeField]
    private GameObject bullet;

    private GameObject spawnBullet;

    private float minWalkX = -7f;

    private bool isShooting = false;






    private void Awake()
    {

        myBody = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();

        sr = GetComponent<SpriteRenderer>();

        handGunSpriteRendere = handGun.GetComponent<SpriteRenderer>();

    }

    // Start is called before the first frame update
    void Start()
    {




    }

    // Update is called once per frame
    void Update()
    {
        PlayerMoveKeyboard();
        AnimatePlayer();
        PlayerJump();
        PlayerDrawGun();

    }


    private void FixedUpdate()
    {
        // PlayerJump();
    }




    void PlayerMoveKeyboard()
    {
        movementX = Input.GetAxisRaw("Horizontal");
        transform.position += Time.deltaTime * moveForce * new Vector3(movementX, 0f, 0f);
    }


    void AnimatePlayer()
    {

        if (movementX > 0)
        {
            anim.SetBool(WALK_ANIMATION, true);
            sr.flipX = false;
            handGunSpriteRendere.flipX = false;
            isRightSide = true;
           
        }
        else if (movementX < 0)
        {
            anim.SetBool(WALK_ANIMATION, true);
            sr.flipX = true;
            handGunSpriteRendere.flipX = true;
            isRightSide = false;
        }
        else
        {
            anim.SetBool(WALK_ANIMATION, false);
        }

        if(!isGrounded)
        {
            Quaternion target = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
        }

        if(transform.position.x < minWalkX)
        {
            transform.position = new Vector3(minWalkX, transform.position.y, transform.position.z);
        }

    }



    void PlayerJump()
    {

        float v = Input.GetAxisRaw("Vertical");


        if (v > 0 && isGrounded)
        {
            isGrounded = false;
            myBody.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
        }
        else
        {
            anim.SetBool(JUMP_ANIMATION, false);
        }
    }

    void PlayerDrawGun()
    {
        if (Input.GetButtonDown("Jump"))
        {
            float sideDegree = !sr.flipX ? 90 : -90;
            Quaternion target = Quaternion.Euler(0, 0, sideDegree);
            handGun.transform.rotation = Quaternion.Slerp(handGun.transform.rotation, target, Time.deltaTime * 1000);
            isShooting = true;
            StartCoroutine(SpawnBullet());
        }
        if (Input.GetButtonUp("Jump"))
        {
            Quaternion target = Quaternion.Euler(0, 0, 0);
            handGun.transform.rotation = Quaternion.Slerp(handGun.transform.rotation, target, Time.deltaTime * 1000);
            isShooting = false;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
       if(collision.gameObject.CompareTag(TAG_GROUND))
        {
            isGrounded = true;

        }

        if (collision.gameObject.CompareTag(TAG_ENEMY))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(TAG_ENEMY))
        {
            Destroy(gameObject);
        }
    }


    IEnumerator SpawnBullet()
    {
        while(isShooting)
        {
          

          spawnBullet = Instantiate(bullet);


            if (sr.flipX)
            {
                spawnBullet.GetComponent<Bullet>().isRight = true;
                spawnBullet.GetComponent<Bullet>().shoot = true;
            }
            else
            {
                spawnBullet.GetComponent<Bullet>().isRight = false;
                spawnBullet.GetComponent<Bullet>().shoot = true;
            }


            spawnBullet.transform.position = transform.position;




            yield return new WaitForSeconds(0.2f);


        }



    }
}
