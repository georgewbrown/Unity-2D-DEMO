using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2D : MonoBehaviour {

    Animator animator;
    Rigidbody2D rb2d;
    SpriteRenderer spriteRenderer;
    bool isGrounded;

    [SerializeField]
    Transform groundCheck = null; // stop err: warning(CS0649) default value = null

    [SerializeField]
    Transform groundCheckL = null; // stop err: warning(CS0649) default value = null

    [SerializeField]
    Transform groundCheckR = null; // stop err: warning(CS0649) default value = null

    [SerializeField]
    private float runSpeed = 1.5f;

    [SerializeField]
    private float jumpSpeed = 5f;

    // Start is called before the first frame update
    void Start() {

        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    private void FixedUpdate() 
    {
        
        if((Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"))) ||
        (Physics2D.Linecast(transform.position, groundCheckL.position, 1 << LayerMask.NameToLayer("Ground")))   ||
        (Physics2D.Linecast(transform.position, groundCheckR.position, 1 << LayerMask.NameToLayer("Ground"))))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
            animator.Play("Player_Jump");

        }

        if(Input.GetKey("d") || Input.GetKey("right")) {

            rb2d.velocity = new Vector2(runSpeed, rb2d.velocity.y);

            if(isGrounded)
                animator.Play("Player_Run");

            spriteRenderer.flipX = false;

        } 
        else if(Input.GetKey("a") || Input.GetKey("left")) 
        {

            rb2d.velocity = new Vector2(-runSpeed, rb2d.velocity.y);

            if(isGrounded)
                animator.Play("Player_Run");

            spriteRenderer.flipX = true;

        } 
        else 
        {
         if(isGrounded)
            animator.Play("Player_Idle");
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
        }
        
        if(Input.GetKey("space") && isGrounded) 
        {

            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpSpeed);
            animator.Play("Player_Jump");

         }
    }
}
