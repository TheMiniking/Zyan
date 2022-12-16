using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveLoad 
{
	public static void Save(PlayerInventaryOBJ player){
		BinaryFormatter bf = new BinaryFormatter();
		FileStream stream = new FileStream(Application.persistentDataPath + "/Player.mk", FileMode.Create);
		
		Zyan.PlayerInventary pl = player.Player;
		bf.Serialize(stream, pl);
		Debug.Log("Data Save: " + Application.persistentDataPath + "/Player.mk");
		stream.Close();
	}
	
	public static Zyan.PlayerInventary Load(){
		if (File.Exists(Application.persistentDataPath + "/Player.mk")){
			BinaryFormatter bf = new BinaryFormatter();
			FileStream stream = new FileStream(Application.persistentDataPath + "/Player.mk", FileMode.Open);
			
			Zyan.PlayerInventary data = bf.Deserialize(stream) as Zyan.PlayerInventary;
			
			stream.Close();
			Debug.Log("Save Loaded");
			return data;
		}
		else{
			Zyan.PlayerInventary vasio = new Zyan.PlayerInventary();
			Debug.Log("Nao tem save!");
			Debug.Log("Criando um novo!");
			//vasio.Name = "Visit" + UnityEngine.Random.Range(0 , 9999);
			//vasio.ID = "" + UnityEngine.Random.Range(0 , 99999999);
			//Save(vasio);
			
			return vasio;
		}
	}
   
}
