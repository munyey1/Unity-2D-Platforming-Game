using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class StoryIntroMovement : Character{

    /*private SpriteRenderer sp;
    public Animator anim;

    void Start(){
        sp = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }*/

    private float moveInput;
    //Float used for the animator.

    void Update(){
        if (Input.GetKey(keybindManager.MyInstance.Keybinds["RIGHT"])){
            //Get the key assigned to "RIGHT" in the keybind dictionary.
            moveInput = 1;
            sp.flipX = false;
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        else if (Input.GetKey(keybindManager.MyInstance.Keybinds["LEFT"])){
            //Get the key assigned to "LEFT" in the keybind dictionary.
            moveInput = 1;
            sp.flipX = true;
            //Flip character.
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
        else{
            //If the player is not moving.
            moveInput = 0;
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        anim.SetFloat("speed", moveInput);
        //Handling the player animations.

        if (rb.constraints == RigidbodyConstraints2D.FreezePosition)
        {
            /*Used for the dialogue sequence of the game. 
             *Pauses the characters animation while dialogue is playing.
             */
            anim.SetFloat("speed", 0);
        }

    }
}
