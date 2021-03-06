﻿using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public Dictionary<string, float> leaderboard;
}

public static class SaveGameManager
{
    public static string savePath = Application.persistentDataPath + "/player.sf";

    public static void Save(SaveData saveData)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();

        FileStream stream = new FileStream(savePath, FileMode.Create);

        binaryFormatter.Serialize(stream, saveData);
        stream.Close();
    }

    public static SaveData Load()
    {
        if (!File.Exists(savePath)) { return new SaveData(); }

        BinaryFormatter binaryFormatter = new BinaryFormatter();

        FileStream stream = new FileStream(savePath, FileMode.Open);

        SaveData saveData = binaryFormatter.Deserialize(stream) as SaveData;
        stream.Close();

        return saveData;
    }
}