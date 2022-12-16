using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Linq;
using UnityEditor;

public static class JsonReader
{
    public static JsonFile.CardList Cardlist;
    //public JsonFile.NodeList myNodelist = new JsonFile.NodeList();
    public static GameObject origem;
    public static int origemIndex;
    public static int index = 0;
    public static string[] unitII;
 
	//Original -Salva no arquivo .Json tudo o que esstiver no "toSave" e o nome escolhido
	// tipo lista com nome antes
	public static void SaveToJSON<T> (List<T> toSave, string filename) {
		Debug.Log (GetPath (filename));
		string content = JsonHelper.ToJson<T> (toSave.ToArray ());
		WriteFile (GetPath (filename), content);
	}
	//Save para Cards
	public static void SaveToJSONCards (Zyan.CardList[] toSave, string filename) {
		Debug.Log (GetPath (filename));
		string content = GetJson.ToJsonCards (toSave, true);
		WriteFile (GetPath (filename), content);
	}
	//Save para Player info
	public static void SaveToJSONPlayer (Zyan.PlayerInventary toSave, string filename) {
		Debug.Log (GetPath (filename));
		string content = JsonUtility.ToJson (toSave, true);
		WriteFile (GetPath (filename), content);
	}
	//Save para IdIndex
	public static void SaveToJSONIdIndex (List<Zyan.IdIndex> toSave, string filename) {
		Debug.Log (GetPath (filename));
		string content = GetJson.ToJsonId (toSave, true);
		WriteFile (GetPath (filename), content);
	}
	
	//Original -Salva no arquivo .Json tudo o que esstiver no "toSave" e o nome escolhido
	public static void SaveToJSON<T> (T toSave, string filename) {
		string content = JsonUtility.ToJson (toSave);
		WriteFile (GetPath (filename), content);
	}

	//Original - Le o arquivo json e retorna ela como lista
	public static List<T> ReadListFromJSON<T> (string filename) {
		string content = ReadFile (GetPath (filename));

		if (string.IsNullOrEmpty (content) || content == "{}") {
			return new List<T> ();
		}

		List<T> res = JsonHelper.FromJson<T> (content).ToList ();

		return res;

	}
	
	// Cria os AssetsObjects com base no Json
	/*public static void CreateAsset(string type)
	{
		switch (type)
		{
		case "Card":
			var c = JsonReader.ReadListFromJSONCard();
			var indexI = 0;
			foreach (var i in c)
			{
				UnitObj cI = ScriptableObject.CreateInstance<UnitObj>();
				AssetDatabase.CreateAsset( cI , "Assets/Resources/Scripts/Unit/"+i.id+".asset");
				indexI++;
			}
			break;
			
		case "Action":
			var a = JsonReader.ReadListFromJSONAction();
			var indexII = 0;
			foreach (var i in a)
			{
				ActionObj cII = ScriptableObject.CreateInstance<ActionObj>();
				AssetDatabase.CreateAsset( cII , "Assets/Resources/Scripts/Action/"+i.id+".asset");
				indexII++;
			}
			break;
		case "Equip":
			var e = JsonReader.ReadListFromJSONEquip();
			var indexIII = 0;
			foreach (var i in e)
			{
				EquipObj cIII = ScriptableObject.CreateInstance<EquipObj>();
				AssetDatabase.CreateAsset( cIII , "Assets/Resources/Scripts/Equip/"+i.id+".asset");
				indexIII++;
			}
			break;
			
		case "Spell":
			var s = JsonReader.ReadListFromJSONSpellTrap();
			var indexIV = 0;
			foreach (var i in s)
			{
				SpellObj cIV = ScriptableObject.CreateInstance<SpellObj>();
				AssetDatabase.CreateAsset( cIV , "Assets/Resources/Scripts/Spell/"+i.id+".asset");
				indexIV++;
			}
			break;
		}
		
	}*/
	
	//Cards -- Le o arquivo json e retorna ela como lista
	public static Zyan.CardList[] ReadListFromJSONCard (string filename) {
		string content = ReadFile (GetPath (filename));
		if (string.IsNullOrEmpty (content) || content == "{}") {
			return new Zyan.CardList[1];
		}
		Zyan.CardListII cards = JsonUtility.FromJson<Zyan.CardListII> (content);
		return cards.Cards;
	}
	//IDList -- Le o arquivo json e retorna ela como lista
	public static List<Zyan.IdIndex> ReadListFromJSONId (string filename) {
		string content = ReadFile (GetPath (filename));
		if (string.IsNullOrEmpty (content) || content == "{}") {
			return new List<Zyan.IdIndex>{};
		}
		Zyan.IdIndexList id = JsonUtility.FromJson<Zyan.IdIndexList> (content);
		return id.IndexList;
	}
	//Cards -- Le o arquivo json e retorna ela como lista
	public static Zyan.CardList[] ReadListFromJSONCard () {
		var c = Resources.Load<TextAsset>("Scripts/CardsDatabaseJson");
		Zyan.CardListII cards = JsonUtility.FromJson<Zyan.CardListII> (c.text);
		return cards.Cards;
	}
	//Action Cards -- Le o arquivo json e retorna ela como lista
	public static Zyan.ActionCardList[] ReadListFromJSONAction () {
		var a = Resources.Load<TextAsset>("Scripts/ActionDatabaseJson");
		Zyan.ActionNodeList action = JsonUtility.FromJson<Zyan.ActionNodeList> (a.text);
		return action.ActionCard;
	}
	//Spell trap Cards -- Le o arquivo json e retorna ela como lista
	public static Zyan.SpellCardList[] ReadListFromJSONSpellTrap () {
		var s = Resources.Load<TextAsset>("Scripts/SpellTrapDatabaseJson");
		Zyan.SpellNodeList spell = JsonUtility.FromJson<Zyan.SpellNodeList> (s.text);
		return spell.SpellTrap;
	}
	//Cards -- Le o arquivo json e retorna ela como lista
	public static Zyan.EquipCardList[] ReadListFromJSONEquip () {
		var e = Resources.Load<TextAsset>("Scripts/EquipDatabaseJson");
		Zyan.EquipCardList[] equip = GetJson.FromJsonEquipCards(e.text);
		return equip;
	}
	//Dialogo -- Le o arquivo json e retorna ela como lista
	public static Zyan.DialogoList[] ReadListFromJSONDialogo () {
		var z = Resources.Load<TextAsset>("Scripts/DialogosJson");
		Zyan.DialogoNodeList fala = JsonUtility.FromJson<Zyan.DialogoNodeList> (z.text);
		return fala.Dialogo;
	}
	//Item -- Le o arquivo json e retorna ela como lista
	public static Zyan.InfoItem[] ReadListFromJSONInfo () {
		var i = Resources.Load<TextAsset>("Scripts/DataDescriptions");
		Zyan.InfoitemNode item = JsonUtility.FromJson<Zyan.InfoitemNode> (i.text);
		return item.ItensPT;
	}
	// Player - Load arquivo player
	public static Zyan.PlayerInventary ReadFromJSONPlayer (string filename) {
		string content = ReadFile (GetPath (filename));
		Zyan.PlayerInventary res = JsonUtility.FromJson<Zyan.PlayerInventary> (content);
		
		return res;
	}
	
	
	//Original -Le o arquivo json e retorna ela 
	public static T ReadFromJSON<T> (string filename) {
		string content = ReadFile (GetPath (filename));

		if (string.IsNullOrEmpty (content) || content == "{}") {
			return default (T);
		}

		T res = JsonUtility.FromJson<T> (content);

		return res;

	}
	//Original -
	public static string GetPath (string filename) {
		return Application.persistentDataPath + "/" + filename;
	}
	//Original -
	private static void WriteFile (string path, string content) {
		FileStream fileStream = new FileStream (path, FileMode.Create);

		using (StreamWriter writer = new StreamWriter (fileStream)) {
			writer.Write (content);
		}
	}
	//Original -
	public static string ReadFile (string path) {
		if (File.Exists (path)) {
			using (StreamReader reader = new StreamReader (path)) {
				string content = reader.ReadToEnd ();
				return content;
			}
		}
		return "";
	}
}

public static class GetJson {
	
	//Original - Cada load possivel -- original
	public static T[] FromJson<T> (string json ) {
		Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>> (json);
		return wrapper.Items;
	}
	// Load for Cards
	public static Zyan.CardList[] FromJsonCards(string json) {
		Zyan.CardListII cards = JsonUtility.FromJson<Zyan.CardListII> (json);
		return cards.Cards;
	} 
	// Load for Action Cards
	public static Zyan.ActionCardList[] FromJsonActionCards (string json) {
		Zyan.ActionNodeList cards = JsonUtility.FromJson<Zyan.ActionNodeList> (json);
		return cards.ActionCard;
	}
	// Load for Spell/Trap Cards
	public static Zyan.SpellCardList[] FromJsonSpellCards (string json) {
		Zyan.SpellNodeList cards = JsonUtility.FromJson<Zyan.SpellNodeList> (json);
		return cards.SpellTrap;
	}
	// Load for Equip Cards
	public static Zyan.EquipCardList[] FromJsonEquipCards (string json) {
		Zyan.EquipNodeList cards = JsonUtility.FromJson<Zyan.EquipNodeList> (json);
		return cards.Equip;
	}
	// Load for Dialogo
	public static Zyan.DialogoList[] FromJsonDialogo (string json) {
		Zyan.DialogoNodeList fala = JsonUtility.FromJson<Zyan.DialogoNodeList> (json);
		return fala.Dialogo;
	}
	// Load for index
	public static List<Zyan.IdIndex> FromJsonIdIndex (string json) {
		Zyan.IdIndexList id = JsonUtility.FromJson<Zyan.IdIndexList> (json);
		return id.IndexList;
	}
	//Original -
	public static string ToJson<T> (T[] array) {
		Wrapper<T> wrapper = new Wrapper<T> ();
		wrapper.Items = array;
		return JsonUtility.ToJson (wrapper);
	}
	
	// Transforma array CardList para JSON string , formatado
	public static string ToJsonCards (Zyan.CardList[] array, bool prettyPrint) {
		Zyan.CardListII cards = new Zyan.CardListII ();
		cards.Cards = array;
		return JsonUtility.ToJson (cards, prettyPrint);
	}
	/* Transforma array idList para JSON string , formatado
	public static string ToJsonId (Zyan.IdIndex[] array, bool prettyPrint) {
		Zyan.IdIndexList id = new Zyan.IdIndexList ();
		id.IndexList = array;
		return JsonUtility.ToJson(id, prettyPrint);
	}*/
	// Transforma list idList para JSON string , formatado
	public static string ToJsonId (List<Zyan.IdIndex> array, bool prettyPrint) {
		Zyan.IdIndexList id = new Zyan.IdIndexList ();
		id.IndexList = array;
		return JsonUtility.ToJson(id, prettyPrint);
	}
	/*public static string ToJsonPlayer (PlayerInventary player, bool prettyPrint) {
		PlayerInventary pl = new P ();
		wrapper.Items = array;
		return JsonUtility.ToJson (wrapper, prettyPrint);
	}*/
	//Original -
	public static string ToJson<T> (T[] array, bool prettyPrint) {
		Wrapper<T> wrapper = new Wrapper<T> ();
		wrapper.Items = array;
		return JsonUtility.ToJson (wrapper, prettyPrint);
	}
	//Original -
	[Serializable]
	private class Wrapper<T> {
		public T[] Items;
	}
}
