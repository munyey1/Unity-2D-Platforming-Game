using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public Vector2 lasCheckPointPos;
    //Stores the last checkpoint

    public static GameMaster instance;

    private void Awake(){ 
        //Singelton pattern, to keep the Game master the same in all scenes.
        if (instance == null){
            /*If this is the first instance of this script,
            * make all references of this the Game Master to this.
            */
            instance = this;
            DontDestroyOnLoad(instance);
            //Do not destroy the gameobject that holds this script on load.
        }else
        {
            Destroy(this);
            //If there is an exisiting Game Master script, destroy this. 
        }
    }

}
