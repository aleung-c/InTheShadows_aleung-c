using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveManager {
	// list of saved games.
	public static List<SaveObject> SavedGames = new List<SaveObject>();


	// Save methods
	public static void SaveGame() {
		SavedGames.Add(SaveObject.Current);
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create (Application.persistentDataPath + "/savedGames.gd");
		bf.Serialize(file, SaveManager.SavedGames);
		file.Close();
	}

	public static void Load() {
		if(File.Exists(Application.persistentDataPath + "/savedGames.gd")) {
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/savedGames.gd", FileMode.Open);
			SaveManager.SavedGames = (List<SaveObject>)bf.Deserialize(file);
			file.Close();
		}
	}

}
