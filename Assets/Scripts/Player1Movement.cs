using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Movement : MonoBehaviour
{
    public float speed = 5f;
    public float moveForce = 5f;
    public float JumpForce = 11f;
    public float maxVelocity = 20f;
    private float movementX;
    private SpriteRenderer sr;

    private Rigidbody2D myBody;
    private Animator anim;

    private readonly string WALK_ANIMATION = "Walk";
    private readonly string JUMP_ANIMATION = "Jump";
    private readonly string KICK_ANIMATION = "Kick";



    private void Awake()
    {

        myBody = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();

        sr = GetComponent<SpriteRenderer>();

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
        }
        else if (movementX < 0)
        {
            anim.SetBool(WALK_ANIMATION, true);
            sr.flipX = true;
        }
        else
        {
            anim.SetBool(WALK_ANIMATION, false);
        }

    }



    void PlayerJump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            myBody.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
        }
        else
        {
            anim.SetBool(JUMP_ANIMATION, false);
        }
    }
}
