using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class DashTutorial : MonoBehaviour{

    [SerializeField]
    private PlayableDirector scene;
    [SerializeField]
    private Animator anim;

    private bool canChange;

    private Player player;

    private void Awake(){
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void Update(){
        if(player.isDashing){
            anim.enabled = true;
            //Re-enable the animations
            Time.timeScale = 1;
            //Set the game time back to normal
        }
        if (canChange)
        {
            player.canDash = true;
            //Access canDash from player and set it to true.
            //Allow the user to dash.
        }
        else player.canDash = false; 
        //Disable the user dash.
    }

    private void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.CompareTag("Player")){
            anim.enabled = false;
            //Stop the players animation.
            scene.Play();
            //Play the cutscene.
        }
    }

    private void OnTriggerExit2D(Collider2D other){
        if (other.gameObject.CompareTag("Player")){
            //Enter if the user fails the jump
            Time.timeScale = 1f;
            //Reset the game time scale.
            anim.enabled = true;
            //Re-enable the animator.
        }
    }

    public void TLfinish(){
        canChange = true;
        //Allow the user to dash.
        Time.timeScale = 0.5f;
        //Game time now in slow motion to help the user.
    }


}
