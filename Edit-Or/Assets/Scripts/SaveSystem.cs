using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
   
    public static void SavePlayer(Player player) // Save the player's data.
    {
        BinaryFormatter formatter = new BinaryFormatter(); // Creates a new formatter.

        string path = Application.persistentDataPath + "/player.plan"; // Creates path to write to.

        FileStream stream = new FileStream(path, FileMode.Create); // Creates a file stream that will create a file

        PlayerData data = new PlayerData(player); // Creates the data and fills it in.

        formatter.Serialize(stream, data); // Passes it into the formatter.

        stream.Close(); // Closes the stream.
    }

    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.plan"; // Creates path to write to.

        if (File.Exists(path)) // If there is a file
        {
            BinaryFormatter formatter = new BinaryFormatter(); // Creates a new formatter.

            FileStream stream = new FileStream(path, FileMode.Open); // Creates a new stream that will open the file.

            PlayerData data = formatter.Deserialize(stream) as PlayerData; // Reads the file.

            stream.Close(); // Closes the stream.

            return data; // Returns the data.
        }
        else // If there isn't
        {
            Debug.Log("Save File Not Found!");
            return null;
        }
    }
}
