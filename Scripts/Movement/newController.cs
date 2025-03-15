using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newController : MonoBehaviour{

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    public float speed;
    public float jumpForce;
    private float moveInput;
    private float yRaw;

    private bool jump;
    private bool isDashing;
    private bool isGrounded;
    public float checkRadius;
    public LayerMask whatIsGround;
    public Transform feetPos;
    public Transform deathParticleLocation;

    public float jumpTime;
    public float dashForce;
    private bool canDash;

    public float dragRate;
    public float dragWait;
    private bool dragChange;

    public ParticleSystem dashEffect;

    void Start(){
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update(){
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);

        moveInput = Input.GetAxisRaw("Horizontal");
        yRaw = Input.GetAxisRaw("Vertical");    
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        animator.SetFloat("Speed", Math.Abs(moveInput));

        if (moveInput > 0) spriteRenderer.flipX = false;
        else if (moveInput < 0) spriteRenderer.flipX = true;

        if (rb.velocity.y < 0){
            animator.SetBool("Falling", true);
            animator.SetBool("isJumping", false);
        } else animator.SetBool("Falling", false);

        if (isGrounded && !isDashing){
            canDash = true;
            animator.SetBool("isJumping", false);
        } else animator.SetBool("isJumping", true);

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash == true){
            if (rb.velocity.y != 0){
                isDashing = true;
                dragChange = true;
                animator.SetTrigger("Dash");
            }
        }

        if (isGrounded && Input.GetKeyDown(KeyCode.Space)){
            jump = true;
            animator.SetTrigger("Jump");
        }

        if (dragChange){
            rb.drag += dragRate;
            //rb.drag += dragRate;
            //Invoke("Drag", dragWait);
            StartCoroutine(DashWait());
        }
    }

    void FixedUpdate(){
        if (isDashing){
            dashEffect.Play();
            Dash(moveInput, yRaw);
            animator.SetBool("isDashing", true);
        } else animator.SetBool("isDashing", false);
         
        if (jump){
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.velocity += Vector2.up * jumpForce;
            animator.SetBool("isJumping", false);
            jump = false;
        }
    }

    private void Dash(float x, float y){
        rb.velocity = Vector2.zero;
        rb.velocity += new Vector2(x, y).normalized * dashForce;
        isDashing = false;
        canDash = false;
    }

    IEnumerator DashWait(){
        
        //rb.drag += dragRate;
        if (rb.drag > 25){
            rb.drag = 25;
            dragChange = false;
        }
        yield return new WaitForSeconds(dragWait);

        rb.drag = 0;
    }
}
