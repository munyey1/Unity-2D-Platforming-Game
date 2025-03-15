using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
    public float speed;
    public float distance;
    private bool movingUp = true;

    public GameObject objectDetection;
    public LayerMask LM;
    private Vector2 dir;

    private void Update(){
        if (movingUp)
        {
            dir = Vector2.up;
            transform.Translate(Vector2.up * speed * Time.deltaTime);
            //Moves the enemy upwards.
        }
        else
        {
            dir = Vector2.down;
            transform.Translate(Vector2.down * speed * Time.deltaTime);
            //Moves the enemy downwards.
        }

        RaycastHit2D objectinfo = Physics2D.Raycast(objectDetection.transform.position, dir, distance, LM);
        //Has the raycast collided.
        if (objectinfo.collider == true){
            if (movingUp == true){
                objectDetection.transform.eulerAngles = new Vector3(180, 0, 0);
                //Flip direction of the object detection.
                movingUp = false;
                //Enemy will now move downwards.
            }else{
                objectDetection.transform.eulerAngles = new Vector3(0, 0, 0);
                //Flip the object detection back.
                movingUp = true;
                //The enemy will now move upwards.
            }
        }
    }
}
