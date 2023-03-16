using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystem 
{
    public static void SavePlayer(GameLogic player, SkinManager skinsData)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/kingJump.kj";
        FileStream stream = new FileStream(path, FileMode.Create);


        PlayerData data = new PlayerData(player, skinsData);

        formatter.Serialize(stream, data);
        stream.Position = 0;
        stream.Close();
    }

    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/kingJump.kj";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else
        { 
            return null;
           
        }
    }


}
