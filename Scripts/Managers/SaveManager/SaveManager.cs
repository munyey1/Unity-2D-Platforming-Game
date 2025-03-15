using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour{

    public GameMaster gm;
    public PlayerGameData GameData;

    public GameObject player;

    /*public void Update(){
        if (Input.GetKeyDown(KeyCode.E)){
            SavePlayer();//Enter the subroutine once E has been pressed.
            Debug.Log("Saved!");
        }
        if (Input.GetKeyDown(KeyCode.Q)){
            LoadPlayer();//Enter LoadPlayer() once Q is pressed.
            Debug.Log("Loaded!");
        }
    }*/
     
    private void Start(){
        PlayerData data = SaveSystem.LoadPlayer();

        if(data == null){
            //If there is no save data, then start the level normally. 
            player.transform.position = new Vector2(gm.lasCheckPointPos.x, gm.lasCheckPointPos.y);
        }
        else if(PlayerPrefs.GetInt("Saved") == 0){
            //Load the saved position of the player. 
            player.transform.position = new Vector2(data.position[0], data.position[1]);

            gm.lasCheckPointPos.x = data.lastCheckpoint[0];//lasCheckPointPos' x and y are updated
            gm.lasCheckPointPos.y = data.lastCheckpoint[1];

            PlayerPrefs.SetInt("Saved", 1);
            PlayerPrefs.Save();
        }
        else if(PlayerPrefs.GetInt("Saved") != 0) {
            //Start the next scene normally.
            player.transform.position = new Vector2(gm.lasCheckPointPos.x, gm.lasCheckPointPos.y);
        }
    }

    public void SavePlayer(){
        SaveSystem.SavePlayer(GameData);
        /*This will enter the SaveSystem class and pass the player 
         * argument as a parameter in the SavePlayer subroutine. 
         */
        PlayerPrefs.SetInt("Saved", 1);
        PlayerPrefs.Save();
    }

    public void LoadPlayer(){
        PlayerData data = SaveSystem.LoadPlayer();
        /*Enters the SaveSystem class and enters LoadPlayer()
        * where the players save data is returned and can be accessed
        */

        SceneManager.LoadScene(data.savescene);//Loads scene that was saved

    }
}
