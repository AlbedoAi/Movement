using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float walkSpeed;
    private float moveInput;

    private Rigidbody2D rb;
    private BoxCollider2D boxCollider2D;
    public LayerMask ground;
    public PhysicsMaterial2D bounceMat, normalMat;

    public bool canJump = true;
    public float jumpValue = 0.0f;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        boxCollider2D = transform.GetComponent<BoxCollider2D>();
    }

    
    void Update()
    {
        movement();
    }

    private void movement() 
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        if (jumpValue == 0.0f && isGrounded())
        {
            rb.velocity = new Vector2(moveInput * walkSpeed, rb.velocity.y);
        }
        if (jumpValue > 0)
        {
            rb.sharedMaterial = bounceMat;
        }
        else
        {
            rb.sharedMaterial = normalMat;
        }

        if (Input.GetKey("space") && isGrounded() && canJump)
        {
            jumpValue += 0.04f;
        }

        if (Input.GetKeyDown("space") && isGrounded() && canJump)
        {
            rb.velocity = new Vector2(0.0f, rb.velocity.y);
        }
        if (jumpValue >= 20f && isGrounded())
        {
            float tempx = moveInput * walkSpeed;
            float tempy = jumpValue;
            rb.velocity = new Vector2(tempx, tempy);
            Invoke("resetJump", 0.2f);
        }

        if (Input.GetKeyUp("space"))
        {
            if (isGrounded())
            {
                rb.velocity = new Vector2(moveInput * walkSpeed, jumpValue);
                jumpValue = 0.0f;
            }
            canJump = true;
        }
    }
    void resetJump()
    {
        canJump = false;
        jumpValue = 0;
    }

    public bool isGrounded() 
    {
        float extraHi = .05f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, extraHi, ground);
        Color rayColor;
        if (raycastHit.collider != null)
        {
            rayColor = Color.green;
        }
        else 
        {
            rayColor = Color.red;
        }

        Debug.DrawRay(boxCollider2D.bounds.center + new Vector3(boxCollider2D.bounds.extents.x, 0), Vector2.down * (boxCollider2D.bounds.extents.y + extraHi), rayColor);
        Debug.DrawRay(boxCollider2D.bounds.center - new Vector3(boxCollider2D.bounds.extents.x, 0), Vector2.down * (boxCollider2D.bounds.extents.y + extraHi), rayColor);
        Debug.DrawRay(boxCollider2D.bounds.center - new Vector3(boxCollider2D.bounds.extents.x, boxCollider2D.bounds.extents.y + extraHi), Vector2.right * (boxCollider2D.bounds.extents.x + extraHi), rayColor);
        return raycastHit.collider != null;
    }
}
