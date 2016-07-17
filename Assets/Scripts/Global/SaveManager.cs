using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

/// <summary>
/// Static Class for save file handling.
/// </summary>
public static class SaveManager {
    // list of saved games.
    //public static List<SaveObject> SavedGames = new List<SaveObject>();
    // only one save should suffice;
    public static SaveObject    CurrentSave;

    // private required;
    static BinaryFormatter bf = new BinaryFormatter();

    // Save methods
    public static void SaveGame() {
        //SavedGames.Add(SaveObject.Current);
        CurrentSave = SaveObject.Current;
        FileStream file = File.Create (Application.persistentDataPath + "/savedGame.gd");
        bf.Serialize(file, CurrentSave);
        file.Close();
	}

	public static void Load() {
		if(File.Exists(Application.persistentDataPath + "/savedGames.gd")) {
            FileStream file = File.Open(Application.persistentDataPath + "/savedGame.gd", FileMode.Open);
            //SaveManager.SavedGames = (List<SaveObject>)bf.Deserialize(file);
            CurrentSave = (SaveObject)bf.Deserialize(file);
            file.Close();
		}
        else
        {
            CurrentSave = SaveObject.Current; // means clean save;
        }
	}

}
