using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem{
    public static void SavePlayer(PlayerGameData player){
        BinaryFormatter bf = new BinaryFormatter();
        //Creates the binary formatter
        string path = Application.persistentDataPath + "/player.test";
        //path is where the file will be saved
        FileStream stream = new FileStream(path, FileMode.Create);
        //This line creates an empty file at the path location
        PlayerData data = new PlayerData(player);
        //Creates 'data' object of PlayerData which contains data
        bf.Serialize(stream, data);
        //The binary formatter now serialises the data to the 'stream' empty file
        stream.Close();
        //Stream is closed to prevent any potential errors to occur
    }
    
    public static PlayerData LoadPlayer(){

        string path = Application.persistentDataPath + "/player.test";
        //path is created again
        if (File.Exists(path)){//Checks whether there is an existing file

            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            //Instead of creating a file, stream will no open the file
            PlayerData data = bf.Deserialize(stream) as PlayerData;
            /*This will turn the binary back into data that is in the form of the 
            data in the PlayerData Class*/
            stream.Close();
            //Again to close the stream to prevent errors.
            return data;
            //This returns the object 'data' which is in the form of recognisable data
        } else{//When there is no existing save file
            return null;
        }
    }

    public static void DeleteSave(){
        File.Delete(Application.persistentDataPath + "/player.test");
        //Deleted the file at the persistent data path
    }

}
