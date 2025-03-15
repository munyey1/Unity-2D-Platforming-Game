using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour {
    private Animator anim;

    public float speed;
    public float distance;
    private bool movingRight = true;

    private bool canMove = true;
    public float idleWait;

    public Transform objectDetection;
    public LayerMask LM;

    void Start() {
        anim = GetComponent<Animator>();
    }

    void Update() {
        if (canMove) {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            //Move the NPC to the right.
        } else {
            transform.Translate(0, 0, 0);
            //Stop the NPC.
        }
        RaycastHit2D objectinfo = Physics2D.Raycast(objectDetection.position, Vector2.right, distance, LM);
        //Detect any collisions coming from the NPC.
        if (objectinfo.collider == true){
            anim.SetBool("stop", true);
            //Play the idle animation.
            canMove = false;
            //Stop the NPC.
            Invoke("Wait", idleWait);
            //Enter the Wait() function with a delay.
            if (movingRight == true) {
                movingRight = false;
                //Change direction.
            } else {
                movingRight = true;
            }
        }
    }

    private void Wait() {
        anim.SetBool("stop", false);
        //Play NPC walking animation.
        if (movingRight){
            transform.eulerAngles = new Vector3(0, -180, 0);
            //Flip the entire NPC.
        } else{
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        canMove = true;
        //Move the NPC again.
    }
}
