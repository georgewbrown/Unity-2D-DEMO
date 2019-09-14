using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour {

    Animator animator;
    Rigidbody2D rb2d;
    SpriteRenderer spriteRenderer;
    Object bulletRef;
    bool isGrounded;
    bool isShooting;


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
        bulletRef = Resources.Load("Bullet");

    }


    private void SpawnBullets(float spawnPositionY, float spawnPositionX, string animationTitle)
    {   isShooting = true;
        animator.Play(animationTitle);
        GameObject bullet = (GameObject)Instantiate(bulletRef);
        bullet.transform.position = new Vector3(transform.position.x + spawnPositionY, transform.position.y + spawnPositionX, -1);
    }

    // Update is called once per frame
    private void Update() 
    {
        if(Input.GetKey("space")) 
        {  
            isShooting = true;
        }
        else
        {
            isShooting = false;
        }
    }

    // FixedUpdate used because frame rate does not impact physics as much
    private void FixedUpdate() 
    {
        
        if(
        (Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"))) ||
        (Physics2D.Linecast(transform.position, groundCheckL.position, 1 << LayerMask.NameToLayer("Ground"))) ||
        (Physics2D.Linecast(transform.position, groundCheckR.position, 1 << LayerMask.NameToLayer("Ground")))
        )
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
            animator.Play("Player_Jump");

        }

        if((Input.GetKey("d") && Input.GetKey("space")) || (Input.GetKey("right") && Input.GetKey("space")))
        {

            rb2d.velocity = new Vector2(runSpeed, rb2d.velocity.y);


            if(isGrounded && isShooting) {
                SpawnBullets(.4f, .2f, "Player_Run_Shoot");
            }

            spriteRenderer.flipX = false;

            return;

        } 
        else if ((Input.GetKey("a") && Input.GetKey("space")) || (Input.GetKey("left") && Input.GetKey("space")))
        {
            rb2d.velocity = new Vector2(-runSpeed, rb2d.velocity.y);


            if(isGrounded  && isShooting) {
                SpawnBullets(- .20f, - .2f, "Player_Run_Shoot");
            }

            spriteRenderer.flipX = true;
            return;

        } else if(Input.GetKey("d") || Input.GetKey("right"))
        {
            
            rb2d.velocity = new Vector2(runSpeed, rb2d.velocity.y);

            if(isGrounded && !isShooting)
                animator.Play("Player_Run");
            

            spriteRenderer.flipX = false;
        }
        else if(Input.GetKey("a") || Input.GetKey("left")) 
        {

            rb2d.velocity = new Vector2(-runSpeed, rb2d.velocity.y);

            if(isGrounded && !isShooting)
                animator.Play("Player_Run");

            spriteRenderer.flipX = true;

        } 
        else 
        {
         if(isGrounded && !isShooting) 
         {
            animator.Play("Player_Idle");
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
         } 
         else if(isShooting) 
         {
            SpawnBullets(.4f, .2f, "Player_Idle_Shoot");
         }

        }
        
        if((Input.GetKey("w") || Input.GetKey("up")) && isGrounded) 
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpSpeed);
            animator.Play("Player_Jump");

         }
    }
}
