using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;

    private Rigidbody2D theRB;

    public Transform groundCheckPoint, groundCheckPoint2;
    public LayerMask whatIsGround;
    private bool isGrounded;


    public Animator anim;
    public SpriteRenderer playerSR;
    // Start is called before the first frame update
    void Start()
    {
        theRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, .1f, whatIsGround) || Physics2D.OverlapCircle(groundCheckPoint2.position, .1f, whatIsGround);

        movement();
        jump();
        flipPlayer();


    }

    private void movement()
    {
        if (Input.GetAxisRaw("Horizontal") > 0 && isGrounded)
        {
            theRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, theRB.velocity.y);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0 && isGrounded)
        {
            theRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, theRB.velocity.y);
        }
    }
    private void jump() 
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
            
        }

        if (Input.GetButtonUp("Jump") && theRB.velocity.y > 0)
        {
            theRB.velocity = new Vector2(theRB.velocity.x, theRB.velocity.y * .5f);
        }

    }
    private void flipPlayer() 
    {
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            playerSR.flipX = false;
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            playerSR.flipX = true;
        }
    }
}
