using UnityEngine;
using UnityEngine.Playables;

public class CutsceneNormal : MonoBehaviour
{
    [SerializeField]
    private PlayableDirector scene;

    private BoxCollider2D bc;
    public Player player;
    private void Start(){
        bc = GetComponent<BoxCollider2D>();
    }
    private void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.CompareTag("Player")){
            //When the player collides, play the cutscene.
            scene.Play();
            bc.enabled = false;
        }
    }
}
