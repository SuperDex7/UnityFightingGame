using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private Animator anim;
    [SerializeField] private LayerMask jumpground;
    private int jumpcount;
    private float dirx = 0f;
    private SpriteRenderer sprite;
    [SerializeField] public float movespeed = 4f;
    [SerializeField] private float jumpforce = 8f;
    [SerializeField] private float runspeed = 12f;
    public static bool isSprinting;
    private bool shiftPressed = false;
    private bool shiftdPressed = false;
    private float doubleTapTimeThreshold = 0.2f; // Adjust as needed
    private float lastShiftPressTime = 0f;
    public float speedBoostMultiplier = 2f; // Adjust as needed
    private float normalSpeed;
    public bool isAttacking;

    private enum MovementState { idle, running, jumping, falling, sprinting, supersprint }
    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        jumpcount = 0;
        normalSpeed = GetComponent<PlayerMovement>().runspeed;
        isAttacking = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!isAttacking)
        {
            dirx = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(dirx * movespeed, rb.velocity.y);
        }

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpforce);
            
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (!shiftPressed)
            {
                shiftPressed = true;

                
                if (Time.time - lastShiftPressTime <= doubleTapTimeThreshold)
                {
                    
                    
                    shiftdPressed = true;
                    runspeed *= speedBoostMultiplier;
                }

                lastShiftPressTime = Time.time;
            }
        }
        
        // Reset the shiftPressed flag and speed boost when the key is released
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            shiftPressed = false;
            shiftdPressed = false;
            runspeed = normalSpeed;
        }

        // Apply sprinting speed if Left Shift is held down

        if (Input.GetKey(KeyCode.LeftShift))
        {
            Sprinting();
            
        }
        else
        {
            isSprinting = false;
        }
        
        UpdateAnimationUpdate();
        
    }
    
    public bool Sprinting()
        {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            rb.velocity = new Vector2(dirx * runspeed, rb.velocity.y);
        return isSprinting = true;

        }
        else
        {
            return isSprinting = false;
        }
        
        }
    public float GetMoveSpeed()
    {
        return movespeed;
    }
    private void UpdateAnimationUpdate()
    {
        MovementState state;
        if (dirx > 0f && isSprinting == false)
        {
            state = MovementState.running;
            //sprite.flipX = false;
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (dirx < 0f && isSprinting == false)
        {
            state = MovementState.running;
            //sprite.flipX = true;
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (isSprinting == true && dirx > 0f && shiftdPressed == false)
        {
            state = MovementState.sprinting;
            //sprite.flipX = false;
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (dirx < 0f && isSprinting == true && shiftdPressed == false)
        {
            state = MovementState.sprinting;
            //sprite.flipX = true;
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (shiftdPressed == true && dirx > 0f)
        {
            state = MovementState.supersprint;
            //sprite.flipX = false;
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (shiftdPressed == true && dirx < 0f)
        {
            state = MovementState.supersprint;
            //sprite.flipX = true;
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else
        {
            state = MovementState.idle;

        }
        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        
anim.SetInteger("state", (int)state);
    }
    public bool IsGrounded()
    {
       return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpground);
    }

}
