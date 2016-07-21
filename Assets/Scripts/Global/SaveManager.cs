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
    // One save should suffice;
    public static SaveObject    CurrentSave;

    // private required;
    static BinaryFormatter bf = new BinaryFormatter();

    // Save methods
    public static void SaveGameFile() {
        FileStream file = File.Create (Application.persistentDataPath + "/savedGame.gd");
        bf.Serialize(file, CurrentSave);
        file.Close();
	}

	public static void LoadGameFile() {
		if (File.Exists(Application.persistentDataPath + "/savedGame.gd")) {
            FileStream file = File.Open(Application.persistentDataPath + "/savedGame.gd", FileMode.Open);
            CurrentSave = (SaveObject)bf.Deserialize(file);
            file.Close();
			Debug.Log("SaveManager: File found and loaded!");
		}
        else
        {
			Debug.Log("SaveManager: Reseting save file");
            CurrentSave = new SaveObject(); // means clean save;
			SaveGameFile();
        }
	}

}
