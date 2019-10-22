using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move_Update : MonoBehaviour {

    public AudioSource running;
    public AudioSource jumping;

    public int playerSpeed = 10;
    public int playerJumpPower = 1250;
    private float moveX;
    private float moveY;
    public int jumpCount = 0;

    [Tooltip("Only change this if your character is having problems jumping when they shouldn't or not jumping at all.")]
    public float distToGround = 0;
    private bool inControl = true;

    [Tooltip("Everything you jump on should be put in a ground layer. Without this, your player probably* is able to jump infinitely")]
    public LayerMask GroundLayer;

    public float ycord = -5.892916f;

    private void Start()
    {
        //running.Play();
        //running.Pause();
    }


    

    // Update is called once per frame
    void Update()
    {
        
        if (inControl)
        {
            PlayerMove();
        }
        ycord = gameObject.transform.position.y;
    }

    void PlayerMove()
    {
        //CONTROLS
        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");
        if (Input.GetButtonDown("Jump") && IsGrounded() && jumpCount < 2)
        {
            //running.Pause();
            jumping.Play();
            Jump();
        }

        //ANIMATIONS
        if (moveX != 0)
        {
            if(IsGrounded() && gameObject.transform.position.y < (ycord + 0.005) && gameObject.transform.position.y > (ycord - 0.005))
            {
                Debug.Log("test");
                //running.UnPause();
                running.Play();
            }
            else
            {
                running.Stop();
                Debug.Log("smash");
                //running.Pause();
            }
            GetComponent<Animator>().SetBool("IsRunning", true);
        }
        else
        {
            //running.Pause();
            running.Stop();
            GetComponent<Animator>().SetBool("IsRunning", false);
        }
        if (Input.GetButtonDown("Jump"))
        {
            GetComponent<Animator>().SetBool("IsJumping", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("IsJumping", false);
        }

        //PLAYER DIRECTION
        if (moveX < 0.0f)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (moveX > 0.0f)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }

        //PHYSICS
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveX * playerSpeed, gameObject.GetComponent<Rigidbody2D>().velocity.y);
    }

    void Jump()
    {
        //JUMPING CODE
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * playerJumpPower);
        jumpCount++;
    }

    public bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, distToGround, GroundLayer);
        if (hit.collider != null && hit.collider.tag == "Player")
        {
            return true;
        }
        return false;

    }

    public void SetControl(bool b)
    {
        inControl = b;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Platform"))
        {
            jumpCount = 0;
        }
    }
}
