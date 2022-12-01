using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
    //walk
    private Rigidbody2D rb;
    public float speed = 5f;
    //run
    public bool isRunning;

    //jump
    [Header("Jump")]
    public bool isGrounded;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float jumpForce;
    //doublejump
    [Header("Double jump")]
    public bool canDoubleJump;
    public int jumpCount;
    private bool noJump;

    //dash
    [Header("Dash")]
    public bool canDash = true;
    public bool isDashing;
    public float dashForce;
    public float dashCooldown;
    Vector2 dashDirection;
    //TrailRenderer dashTrail;

    //perks
    //was originally planning to have them as unlockable perks
    //but i did not have the time to do what i originally planned
    [Header("Perks")]
    private bool doubleJumpUnlocked = true;
    private bool dashUnlocked = true;

    [Header("Animation")]
    public Animator animator;

    void Start()
    {
        //get the player rigidbody
        rb = gameObject.GetComponent<Rigidbody2D>();
        //dashTrail = gameObject.GetComponent<TrailRenderer>();
        jumpCount = 1;
    }

    void Update()
    {
        Ground();
    }

    //this update is for physics based stuff
    void FixedUpdate()
    {
        Walk();
        Sprint();

        
        
        //see if the player is grounded by checking the layer and the groundCheck
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, .4f, groundLayer);
        Jump();
        if(doubleJumpUnlocked == true) canDoubleJump = true;
        if(dashUnlocked == true && isGrounded == true) canDash = true;

        //dash
        if(dashUnlocked == true)
        {
            Dash();
        }
    }

    //if you want a more in depth explanation of the walk function
    //watch this https://www.youtube.com/watch?v=C70QxpI9F5Y
    //it helped me understand this stuff better(no its not me)

    void Walk()
    {
        Vector2 axis = new Vector2(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal")) * speed;
        Vector3 forward = new Vector3(-Camera.main.transform.right.z, 0f, Camera.main.transform.right.x);
        Vector3 wishDirection = (forward * axis.x + Camera.main.transform.right * axis.y + Vector3.up * rb.velocity.y);
        rb.velocity = wishDirection;

        //activate the walk animation if the horizontal axis returns a value different than one
        //and only do it when on ground
        if(Input.GetAxis("Horizontal") != 0 && isGrounded == true)
        {
            animator.SetBool("isWalking", true);
            FindObjectOfType<AudioController>().Play("walking");
        } 
        else
        {
            animator.SetBool("isWalking", false);
            FindObjectOfType<AudioController>().Pause("walking");
        }
    }

    void Sprint()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            animator.SetBool("isRunning", true);
            isRunning = true;
        }
        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            animator.SetBool("isRunning", false);
            isRunning = false;
        }
    }

    void Ground()
    {
        //set the speed if the player is on ground, running, in air
        if(isGrounded == true && isRunning == false)
        {
            speed = 5f;
            canDash = true;
            animator.SetBool("isJumping", false);
        }
        if(isGrounded == true && isRunning == true)
        {
            speed = 7f;
            canDash = true;
            animator.SetBool("isJumping", false);
        }
        if(isGrounded == false) 
        {
            speed = 2.5f;
            animator.SetBool("isJumping", true);
        }

        //activate double jump
        if(canDoubleJump == false) return;
        //doublejump
        if(!isGrounded)
        {
            //if the player is not on ground he can press the jump button again to double jump
            canDoubleJump = true;
            if(jumpCount <= 0)
            {
                jumpForce = 0f;
                noJump = true;
            }
        }
        else
        {
            noJump = false;
            canDoubleJump = false;
            jumpForce = 8f;
            if(jumpCount <= 0)
            {
                jumpCount = 1;
            }
        }
    }

    void Jump()
    {
        //there is a bug that happens when you are in air and press w to jump
        //if you have no jumps left the player gets stuck in air for a few frames
        if(noJump == true) return;
        //if pressing jump button when on ground jump
        if(Input.GetKeyDown(KeyCode.W) && isGrounded == true || Input.GetKeyDown(KeyCode.W) && canDoubleJump == true)
        {
            rb.velocity = new Vector2(0f, jumpForce);
            if(canDoubleJump == true) jumpCount--;
        }
    }

    void Dash()
    {
        if(Input.GetKeyDown(KeyCode.Space) && canDash == true)
        {
            isDashing = true;
            canDash = false;
            dashDirection = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));
            //dashTrail.emitting = true;
            FindObjectOfType<AudioController>().Play("dash");
            if(dashDirection == Vector2.zero)
            {
                dashDirection = new Vector2(transform.localScale.x, 0);
            }
            StartCoroutine(StopDashing());
            
        }
        if(isDashing == true)
        {
            rb.velocity = dashDirection.normalized * dashForce;
            return;            
        }

        IEnumerator StopDashing()
        {
            rb.gravityScale = 0;
            //dashTrail.emitting = false;
            yield return new WaitForSeconds(dashCooldown);
            isDashing = false;
            rb.gravityScale = 1;
        }
    }

    //unlockables
    public void ActivateDash()
    {
        dashUnlocked = true;
    }
    public void ActivateDoubleJump()
    {
        doubleJumpUnlocked = true;
    }
}
