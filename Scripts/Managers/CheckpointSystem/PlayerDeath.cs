using UnityEngine;

public class PlayerDeath : MonoBehaviour{

    private GameMaster gm;
    public float spawnDelay;
    public ParticleSystem deathParticle;
    private Animator anim;

    public AudioSource sound;

    private void Start(){
        gm = GameObject.Find("GameMaster").GetComponent<GameMaster>();
        anim = GameObject.Find("Panel").GetComponent<Animator>();
        //This animator is from the panel, which is a UI element that covers the screen.
    }

    private void OnCollisionEnter2D(Collision2D other){
        //Used for collision with enemies/objects.
        if (other.gameObject.CompareTag("Enemy")){
            gameObject.SetActive(false);
            //Set character to false, to imitate death.
            sound.Play();
            //Play the death sound.
            Instantiate(deathParticle, transform.position, deathParticle.transform.rotation);
            //Play the death particle at the location of the player.
            anim.SetTrigger("StartFade");
            //Start death transition
            Invoke("Respawn", spawnDelay);
            //Enter the Respawn() function with a delay.
        }
    }

    private void OnTriggerEnter2D(Collider2D other){
        //Used for player falling off the level.
        if (other.gameObject.CompareTag("Enemy")){
            gameObject.SetActive(false);
            anim.SetTrigger("StartFade");
            Invoke("Respawn", spawnDelay);
        }
    }
    private void Respawn(){
        anim.SetTrigger("EndFade");
        //Finish transiiton
        transform.position = gm.lasCheckPointPos;
        //Set the player to their last checkpoint.
        gameObject.SetActive(true);
    } // teleport the player at the lastcheckpoint and then activate the player again

}
