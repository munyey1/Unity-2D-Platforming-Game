using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerGameData : MonoBehaviour
{
    [HideInInspector]
    public int scene; 
    [HideInInspector]
    public Vector2 position; //Holds the current position of the character.

    public GameMaster gm; //Reference to the GameMaster to grab the last checkpoint.
    void Awake(){
        scene = SceneManager.GetActiveScene().buildIndex;
        //Grabs the current scene.
    }
}
