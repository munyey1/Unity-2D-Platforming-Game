using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class blockPlayerController : MonoBehaviour
{

    Rigidbody2D rb;

    private float moveInput;
    public float speed;

    private bool canJump;
    public float jumpForce;

    private float yRaw;
    public float dashForce;
    private bool isDashing;
    private bool canDash;

    public float dragRate;
    public float dragWait;

    private bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;

    public ParticleSystem dashEffect;
    void Start(){
        rb = GetComponent<Rigidbody2D>();
    }

    void Update(){
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
        moveInput = Input.GetAxisRaw("Horizontal");
        yRaw = Input.GetAxisRaw("Vertical");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        if (isGrounded){
            canDash = true;
        }

        if (isGrounded && Input.GetKeyDown(KeyCode.Space)){
            canJump = true;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash){
            //dashEffect.Play();
            if (rb.velocity.y != 0){
                isDashing = true;
            }
        }
    }

    void FixedUpdate(){
        if (canJump){
            Jump();
        }

        if (isDashing){
            Dash(moveInput, yRaw);
        }
    }

    private void Jump(){
        rb.velocity = new Vector2(rb.velocity.x, 0); 
        rb.velocity += Vector2.up * jumpForce;
        canJump = false;
    }

    private void Dash(float x, float y){
        rb.velocity = Vector2.zero;
        rb.velocity += new Vector2(x, y).normalized * dashForce;
        rb.drag = dragRate;
        Invoke("Drag", dragWait);
        isDashing = false;
        canDash = false;
    }

    private void Drag(){
        rb.drag = 0;
    }
}
