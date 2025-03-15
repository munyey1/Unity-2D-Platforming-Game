using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class VanishingPlatform : MonoBehaviour
{
    private static VanishingPlatform instance;
    public static VanishingPlatform MyInstance
    {//Singelton pattern
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<VanishingPlatform>();
            }
            return instance;
        }
    }
    private BoxCollider2D bc;
    public float delay;
    private Animator anim;

    private void Awake(){
        bc = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    public void VanishStart(){
        StartCoroutine(PlatformToggle());
        //So the platform respawns when the player is dead.
    }

    public IEnumerator PlatformToggle(){
        yield return new WaitForSeconds(delay);
        anim.Play("VanishPlatform");
        //Play the platform vanish animation.
        bc.enabled = false;
        //Disabled the box collider.
        yield return new WaitForSeconds(delay);
        anim.Play("Platform_Reappear");
        //Play the reappear animation.
        bc.enabled = true;
        //Re-enable the box collider.
    }
}
