using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class Movement : Character
{
    private float xRaw;

    private bool canJump;
    public float jumpForce;

    private float yRaw;
    private float moveInput;

    private bool canDash;
    private bool isDashing;
    public float dashForce;

    public float dragRate;
    public float dragWait;

    private bool isGrounded;
    public Transform feetPos; // a transform is simply a position
    public float checkRadius;
    public LayerMask whatIsGround; // a layermask is a layer for objects to reside on

    public GameMaster gm;
    [HideInInspector]
    public Vector2 position;
    [HideInInspector]
    public int scene;
    private void Awake(){
        scene = SceneManager.GetActiveScene().buildIndex;
    }

    // update is called once per frame
    void Update(){
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
        /* this line of code will form an imaginary circle at the bottome of the character
         *and if it detects any collision it will return true. it is set to the boolean isGrounded 
         *to check whether the user can jump. this will be needed as if this wasn't added the user 
         *will be able to infinitely jump.
         */

        if (Input.GetKey(keybindManager.MyInstance.Keybinds["RIGHT"])){
            xRaw = 1;
            moveInput = 1;
            rb.velocity = new Vector2(speed, rb.velocity.y);
            sp.flipX = false;
        }
        else if (Input.GetKey(keybindManager.MyInstance.Keybinds["LEFT"])){
            xRaw = -1;
            moveInput = 1;
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            sp.flipX = true;
        } else{
            xRaw = 0;
            moveInput = 0;
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        if (rb.velocity.y < 0)
        {
            anim.SetBool("Falling", true);
            anim.SetBool("Jumping", false);
        }
        else anim.SetBool("Falling", false);



        if (Input.GetKey(keybindManager.MyInstance.Keybinds["UP"])){
            yRaw = 1;
        }
        else if (Input.GetKey(keybindManager.MyInstance.Keybinds["DOWN"])){
            yRaw = -1;
        } else{
            yRaw = 0;
        }

        if (isGrounded && !isDashing)
        {
            canDash = true; // the player can only dash if they have landed on the ground once
            rb.velocity = new Vector2(rb.velocity.x, 0);

            anim.SetFloat("Speed", moveInput);
            anim.SetBool("Grounded", true);
            anim.SetBool("Falling", false);
            anim.SetBool("Jumping", false);
            anim.SetBool("Dashing", false);
        }
        else anim.SetBool("Grounded", false);

        if (Input.GetKeyDown(keybindManager.MyInstance.Keybinds["JUMP"]) && isGrounded && Time.timeScale == 1){ 
            // will check if the user is grounded, if they have pressed jump, and if the menu is closed.
            canJump = true; // set canjump = true so the player enters the jump sequence.
            anim.SetTrigger("Jump");
            anim.SetBool("Jumping", true);
        }

        if (Input.GetKeyDown(keybindManager.MyInstance.Keybinds["DASH"]) && canDash == true){
            // will check if the user has landed on the ground first and if they have pressed dash
            if (rb.velocity.y != 0 ){
                // if the character is in mid-air
                isDashing = true; // enters the if statement
                anim.SetTrigger("Dash");
                anim.SetBool("Dashing", true);
            }
        }
    }

    private void FixedUpdate(){ // fixedupdate is called every fixed frame for physics
        if (canJump){
            jump(); // will enter the jump function.
        }
        if (isDashing)
        {
            Dash(xRaw, yRaw); // enters the dashing function
        }

    }

    private void jump(){
        rb.velocity = new Vector2(rb.velocity.x, 0); // keep x-velocity the same as we don't need
        rb.velocity += Vector2.up * jumpForce; // increases the players y-position by the jumpforce
        canJump = false; // exits the function
        anim.SetBool("Jumping", false);
    }

    private void Dash(float x, float y){
        rb.velocity = Vector2.zero;
        //Vector2 dir = new Vector2(x, y);

        //rb.velocity += dir.normalized * dashForce;
        //StartCoroutine(DashWait());
        // reset the velocity of the player to 0 to accurately get the direction of dash
        rb.velocity = new Vector2(x, y).normalized * dashForce;
        /* the users input will correspond to the direction of dash
        the players velocity components will be multiplied by the dash force, to simulate a dash
        */
        rb.drag = dragRate; // changes the players drag to a value chosen
        Invoke("Drag", dragWait); // enters the Drag function in a given time
        canDash = false;
        isDashing = false;
        anim.SetBool("Dashing", false);
        // both set to false to reset the dash
    }

    public void Drag(){
        rb.drag = 0; // resets drag component
    }

    IEnumerator DashWait(){
        rb.drag += dragRate;
        rb.gravityScale = 0;

        GetComponent<betterJump>().enabled = false;

        yield return new WaitForSeconds(dragWait);

        rb.gravityScale = 5.5f;
        rb.drag = 0;
        GetComponent<betterJump>().enabled = true;

        isDashing = false;
    }
}
