using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class Sistema_De_Salvamento 
{
    public static void SaveGameManager(GameManager gameManager)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.fun";
        FileStream fileStream = new FileStream(path, FileMode.Create);

        GameData data = new GameData(gameManager);

        formatter.Serialize(fileStream, data);
        fileStream.Close();
    }

    public static GameData LoadGame()
    {
        string path = Application.persistentDataPath + "/player.fun";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            GameData data = formatter.Deserialize(stream) as GameData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("SAve Not Found in " + path);
            return null;
        }
    }

}
