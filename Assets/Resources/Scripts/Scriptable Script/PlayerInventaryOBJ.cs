using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using Sirenix.OdinInspector;


[CreateAssetMenu (fileName = "Player", menuName = "Zyan Assets/Create Player Inventary Object")]
public class PlayerInventaryOBJ : ScriptableObject
{
	public Zyan.PlayerInventary Player;
	public PlayerDecksInventaryOBJ DeckInventary;
	public ActionInventaryOBJ ActionInventary;
	public EquipInventaryOBJ EquipInventary;
	public SpellInventaryOBJ SpellInventary;
	public UnitInventaryOBJ UnitInventary ;
	
	void OnValidate() => LoadInventary();
	
	public void LoadInventary()
	{
		UnitInventary = Resources.Load<UnitInventaryOBJ>("Player/UnitInventary");
		ActionInventary = Resources.Load<ActionInventaryOBJ>("Player/ActionInventary");
		EquipInventary = Resources.Load<EquipInventaryOBJ>("Player/EquipInventary");
		SpellInventary = Resources.Load<SpellInventaryOBJ>("Player/SpellInventary");
		DeckInventary = Resources.Load<PlayerDecksInventaryOBJ>("Player/DeckInventary");
	}
	//Terminar de Automatizar - criando cada coisa nescessaria para o jogador nao perder DATA
	//
	private int index;
	public void SetUnitsOBJ()
	{
		Zyan.IdIndex[] card = new Zyan.IdIndex[0];
		var c = JsonReader.ReadListFromJSONCard();
		index = 0 ;
		foreach (var i in c)
		{	
			Zyan.IdIndex temp = new Zyan.IdIndex();
			temp.Id = i.id;
			temp.index = index; 
			ArrayUtility.Add<Zyan.IdIndex>( ref card , temp);
			index++;	
		}
		Player.InventaryCards = card;
		JsonReader.SaveToJSONIdIndex(card ,"Card.mk");
		var d = Resources.LoadAll<UnitObj>("Scripts/Unit");
		if (d.Length < card.Length){JsonReader.CreateAsset("Card");}
		UnitInventaryOBJ orig = ScriptableObject.CreateInstance<UnitInventaryOBJ>();
		AssetDatabase.CreateAsset( orig , "Assets/Resources/Player/UnitInventary.asset");
		
	}
	
	public void SetActionOBJ()
	{
		Zyan.IdIndex[] action = new Zyan.IdIndex[0];
		var a = JsonReader.ReadListFromJSONAction();
		index = 0 ;
		foreach (var i in a)
		{	
			Zyan.IdIndex temp = new Zyan.IdIndex();
			temp.Id = i.id;
			temp.index = index; 
			ArrayUtility.Add<Zyan.IdIndex>( ref action , temp);
			index++;	
		}
		Player.InventaryActionCards = action;
		JsonReader.SaveToJSONIdIndex(action ,"Action.mk");
		var c = Resources.LoadAll<ActionObj>("Scripts/Action");
		if (c.Length < action.Length){JsonReader.CreateAsset("Action");}
		ActionInventaryOBJ orig = ScriptableObject.CreateInstance<ActionInventaryOBJ>();
		AssetDatabase.CreateAsset( orig , "Assets/Resources/Player/ActionInventary.asset");
		
	}
	
	public void SetEquipOBJ()
	{
		Zyan.IdIndex[] equip = new Zyan.IdIndex[0];
		var e = JsonReader.ReadListFromJSONEquip();
		index = 0 ;
		foreach (var i in e)
		{	
			Zyan.IdIndex temp = new Zyan.IdIndex();
			temp.Id = i.id;
			temp.index = index; 
			ArrayUtility.Add<Zyan.IdIndex>( ref equip , temp);
			index++;	
		}
		Player.InventaryEquip = equip;
		JsonReader.SaveToJSONIdIndex(equip ,"Equip.mk");
		var c = Resources.LoadAll<EquipObj>("Scripts/Equip");
		if (c.Length < equip.Length){JsonReader.CreateAsset("Equip");}
		EquipInventaryOBJ orig = ScriptableObject.CreateInstance<EquipInventaryOBJ>();
		AssetDatabase.CreateAsset( orig , "Assets/Resources/Player/EquipInventary.asset");
	}
	
	public void SetSpellOBJ()
	{
		Zyan.IdIndex[] spell = new Zyan.IdIndex[0];
		var s = JsonReader.ReadListFromJSONSpellTrap();
		index = 0 ;
		foreach (var i in s)
		{	
			Zyan.IdIndex temp = new Zyan.IdIndex();
			temp.Id = i.id;
			temp.index = index; 
			ArrayUtility.Add<Zyan.IdIndex>( ref spell , temp);
			index++;	
		}
		Player.InventarySpell = spell;
		JsonReader.SaveToJSONIdIndex(spell ,"Spell.mk");
		var c = Resources.LoadAll<SpellObj>("Scripts/Spell");
		if (c.Length < spell.Length){JsonReader.CreateAsset("Spell");}
		SpellInventaryOBJ orig = ScriptableObject.CreateInstance<SpellInventaryOBJ>();
		AssetDatabase.CreateAsset( orig , "Assets/Resources/Player/SpellInventary.asset");
	}
	
	[Button]
	public void LoadPlayerDATA()
	{
		string content = JsonReader.ReadFile (JsonReader.GetPath ("Player.mk"));
		if (string.IsNullOrEmpty (content) || content == "{}") {
			Debug.Log("Save nao existe, criando um novo");
			JsonReader.SaveToJSONPlayer(Player, "Player.mk");
			Zyan.PlayerInventary playerData = JsonReader.ReadFromJSONPlayer("Player.mk");
			Player.ID = UnityEngine.Random.Range( 0 , 99999999).ToString();
			Player.Name = "Visitante";
			SetActionOBJ();
			SetEquipOBJ();
			SetSpellOBJ();
			SetUnitsOBJ();
			JsonReader.SaveToJSONPlayer(Player, "Player.mk");
		}else 
		{
			Zyan.PlayerInventary playerData = JsonReader.ReadFromJSONPlayer("Player.mk");
			Debug.Log("Save carregado, jogador: "+ playerData.Name);
			Player = playerData;
			SetActionOBJ();
			SetEquipOBJ();
			SetSpellOBJ();
			SetUnitsOBJ();
			JsonReader.SaveToJSONPlayer(Player, "Player.mk");
		}
		PlayerDecksInventaryOBJ deck = ScriptableObject.CreateInstance<PlayerDecksInventaryOBJ>();
		AssetDatabase.CreateAsset( deck , "Assets/Resources/Player/DeckInventary.asset");
	}
}
