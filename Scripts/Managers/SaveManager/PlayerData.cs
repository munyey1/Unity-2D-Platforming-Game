[System.Serializable]
public class PlayerData {

    public float[] position;
    public float[] lastCheckpoint;
    public int savescene;
    public PlayerData (PlayerGameData player){

        position = new float[2];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        //Saves the position of the character.

        lastCheckpoint = new float[2];
        lastCheckpoint[0] = player.gm.lasCheckPointPos.x;
        lastCheckpoint[1] = player.gm.lasCheckPointPos.y;
        //Saves the last checkpoint of the player.

        savescene = player.scene;
        //Saves the last scene of the player.
    }
}
