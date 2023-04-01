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
	//public PlayerDecksInventaryOBJ DeckInventary;
	public Zyan.PlayerDecks AtualDeck;
	[ShowInInspector]public Dictionary<string,Zyan.IdIndex> PlayerInventaryUnit = new Dictionary<string, Zyan.IdIndex>{}; //Contem unitInventary
	[ShowInInspector]public Dictionary<string,Zyan.IdIndex> PlayerInventaryAction = new Dictionary<string, Zyan.IdIndex>{}; //Contem unitInventary
	[ShowInInspector]public Dictionary<string,Zyan.IdIndex> PlayerInventaryEquip = new Dictionary<string, Zyan.IdIndex>{}; //Contem unitInventary
	[ShowInInspector]public Dictionary<string,Zyan.IdIndex> PlayerInventarySpell = new Dictionary<string, Zyan.IdIndex>{}; //Contem unitInventary
	[ShowInInspector]public Dictionary<string,Zyan.PlayerDecks> PlayerInventaryDeck = new Dictionary<string, Zyan.PlayerDecks>{}; //Contem unitInventary
	[ShowInInspector]public Dictionary<string,int> UnitUtilizadas = new Dictionary<string, int>{}; 
	
	void Awake()=> ResetStatus();
	//Terminar de Automatizar - criando cada coisa nescessaria para o jogador nao perder DATA
	/// --------------------------------------------------///
	private int index;
	public void SetUnitsOBJ()
	{
		List<Zyan.IdIndex> card = new List<Zyan.IdIndex>{};
		var c = JsonReader.ReadListFromJSONCard();
		index = 0 ;
		foreach (var i in c)
		{	
			Zyan.IdIndex temp = new Zyan.IdIndex();
			temp.Id = i.id;
			temp.index = index; 
			card.Add(temp);
			index++;	
		}
		Player.InventaryCards = card;
		//List<Zyan.IdIndex> tp = new List<Zyan.IdIndex>{}; 
		//foreach (Zyan.IdIndex p in card){tp.Add(p);}
		//JsonReader.SaveToJSONIdIndex(tp ,"Card.mk");
		#if (UNITY_EDITOR) 
		var d = Resources.LoadAll<UnitObj>("Scripts/Unit");
		if (d.Length < card.Count){CreateAssets.CreateAsset("Card");
		UnitInventaryOBJ orig = ScriptableObject.CreateInstance<UnitInventaryOBJ>();
		AssetDatabase.CreateAsset( orig , "Assets/Resources/Player/UnitInventary.asset");}
		#endif
	}
	/// --------------------------------------------------///
	/// 
	/// --------------------------------------------------///
	public void SetActionOBJ()
	{
		List<Zyan.IdIndex> action = new List<Zyan.IdIndex>{};
		var a = JsonReader.ReadListFromJSONAction();
		index = 0 ;
		foreach (var i in a)
		{	
			Zyan.IdIndex temp = new Zyan.IdIndex();
			temp.Id = i.id;
			temp.index = index; 
			action.Add(temp);
			index++;	
		}
		Player.InventaryActionCards = action;
		//List<Zyan.IdIndex> tp = new List<Zyan.IdIndex>{}; 
		//foreach (Zyan.IdIndex p in action){tp.Add(p);}
		//JsonReader.SaveToJSONIdIndex(tp ,"Action.mk");
		#if (UNITY_EDITOR) 
		var c = Resources.LoadAll<ActionObj>("Scripts/Action");
		if (c.Length < action.Count){CreateAssets.CreateAsset("Action");
		ActionInventaryOBJ orig = ScriptableObject.CreateInstance<ActionInventaryOBJ>();
		AssetDatabase.CreateAsset( orig , "Assets/Resources/Player/ActionInventary.asset");}
		#endif
		
	}
	/// --------------------------------------------------///
	/// 
	/// --------------------------------------------------///
	public void SetEquipOBJ()
	{
		List<Zyan.IdIndex> equip = new List<Zyan.IdIndex>{};
		var e = JsonReader.ReadListFromJSONEquip();
		index = 0 ;
		foreach (var i in e)
		{	
			Zyan.IdIndex temp = new Zyan.IdIndex();
			temp.Id = i.id;
			temp.index = index; 
			equip.Add(temp);
			index++;	
		}
		Player.InventaryEquip = equip;
		//List<Zyan.IdIndex> tp = new List<Zyan.IdIndex>{}; 
		//foreach (Zyan.IdIndex p in equip){tp.Add(p);}
		//JsonReader.SaveToJSONIdIndex(tp ,"Equip.mk");
		#if (UNITY_EDITOR) 
		var c = Resources.LoadAll<EquipObj>("Scripts/Equip");
		if (c.Length < equip.Count){CreateAssets.CreateAsset("Equip");
		EquipInventaryOBJ orig = ScriptableObject.CreateInstance<EquipInventaryOBJ>();
		AssetDatabase.CreateAsset( orig , "Assets/Resources/Player/EquipInventary.asset");}
		#endif
	}
	/// --------------------------------------------------///
	/// 
	/// --------------------------------------------------///
	public void SetSpellOBJ()
	{
		List<Zyan.IdIndex> spell = new List<Zyan.IdIndex>{};
		var s = JsonReader.ReadListFromJSONSpellTrap();
		index = 0 ;
		foreach (var i in s)
		{	
			Zyan.IdIndex temp = new Zyan.IdIndex();
			temp.Id = i.id;
			temp.index = index; 
			spell.Add(temp);
			index++;	
		}
		Player.InventarySpell = spell;
		//List<Zyan.IdIndex> tp = new List<Zyan.IdIndex>{}; 
		//foreach (Zyan.IdIndex p in spell){tp.Add(p);}
		//JsonReader.SaveToJSONIdIndex(tp ,"Spell.mk");
		#if (UNITY_EDITOR) 
		var c = Resources.LoadAll<SpellObj>("Scripts/Spell");
		if (c.Length < spell.Count){CreateAssets.CreateAsset("Spell");
		SpellInventaryOBJ orig = ScriptableObject.CreateInstance<SpellInventaryOBJ>();
		AssetDatabase.CreateAsset( orig , "Assets/Resources/Player/SpellInventary.asset");}
		#endif
	}
	/// --------------------------------------------------///
	/// 
	/// --------------------------------------------------///
	public void DecksInventary(){
		#if (UNITY_EDITOR) 
		PlayerDecksInventaryOBJ deck = ScriptableObject.CreateInstance<PlayerDecksInventaryOBJ>();
		AssetDatabase.CreateAsset( deck , "Assets/Resources/Player/DeckInventary.asset");
		#endif
	}
	/// --------------------------------------------------///
	/// 
	/// --------------------------------------------------///
	public void SetPlayerOBJ(){
		SetActionOBJ();
		SetEquipOBJ();
		SetSpellOBJ();
		SetUnitsOBJ();
	}
	/// --------------------------------------------------///
	/// 
	/// --------------------------------------------------///
	
	/// Dicionarys Funcoes      --------------------------------///
	/// Atualiza dados do player para o dicionario--------------///
	[Button]
	public void DicUpdate(){
		foreach (Zyan.IdIndex i in Player.InventaryCards){if(PlayerInventaryUnit.ContainsKey(i.Id)!= true)PlayerInventaryUnit.Add(i.Id , i);}
		foreach (Zyan.IdIndex i in Player.InventarySpell){if(PlayerInventarySpell.ContainsKey(i.Id)!= true)PlayerInventarySpell.Add(i.Id , i);}
		foreach (Zyan.IdIndex i in Player.InventaryEquip){if(PlayerInventaryEquip.ContainsKey(i.Id)!= true)PlayerInventaryEquip.Add(i.Id , i);}
		foreach (Zyan.IdIndex i in Player.InventaryActionCards ){if(PlayerInventaryAction.ContainsKey(i.Id)!= true)PlayerInventaryAction.Add(i.Id , i);}
		UnitUtilizadas.Clear();
		for (int i = 0; i < 5; i++)
		{
			switch (i){
			case 0:
				if(AtualDeck.Slot1.Civil != ""){UnitUtilizadas[AtualDeck.Slot1.Civil] = UnitUtilizadas.TryGetValue(AtualDeck.Slot1.Civil, out int val) ? val+1 : 1; }
				if(AtualDeck.Slot1.Soldier != ""){UnitUtilizadas[AtualDeck.Slot1.Soldier] = UnitUtilizadas.TryGetValue(AtualDeck.Slot1.Soldier, out int val) ? val+1 : 1; }
				if(AtualDeck.Slot1.Combatant != ""){UnitUtilizadas[AtualDeck.Slot1.Combatant] = UnitUtilizadas.TryGetValue(AtualDeck.Slot1.Combatant, out int val) ? val+1 : 1; }
				if(AtualDeck.Slot1.General != ""){UnitUtilizadas[AtualDeck.Slot1.General] = UnitUtilizadas.TryGetValue(AtualDeck.Slot1.General, out int val) ? val+1 : 1; }
				if(AtualDeck.Slot1.King != ""){UnitUtilizadas[AtualDeck.Slot1.King] = UnitUtilizadas.TryGetValue(AtualDeck.Slot1.King, out int val) ? val+1 : 1; }
				if(AtualDeck.Slot1.God != ""){UnitUtilizadas[AtualDeck.Slot1.God] = UnitUtilizadas.TryGetValue(AtualDeck.Slot1.God, out int val) ? val+1 : 1; }
				break;
			case 1:
				if(AtualDeck.Slot2.Civil != ""){UnitUtilizadas[AtualDeck.Slot2.Civil] = UnitUtilizadas.TryGetValue(AtualDeck.Slot2.Civil, out int val) ? val+1 : 1; }
				if(AtualDeck.Slot2.Soldier != ""){UnitUtilizadas[AtualDeck.Slot2.Soldier] = UnitUtilizadas.TryGetValue(AtualDeck.Slot2.Soldier, out int val) ? val+1 : 1; }
				if(AtualDeck.Slot2.Combatant != ""){UnitUtilizadas[AtualDeck.Slot2.Combatant] = UnitUtilizadas.TryGetValue(AtualDeck.Slot2.Combatant, out int val) ? val+1 : 1; }
				if(AtualDeck.Slot2.General != ""){UnitUtilizadas[AtualDeck.Slot2.General] = UnitUtilizadas.TryGetValue(AtualDeck.Slot2.General, out int val) ? val+1 : 1; }
				if(AtualDeck.Slot2.King != ""){UnitUtilizadas[AtualDeck.Slot2.King] = UnitUtilizadas.TryGetValue(AtualDeck.Slot2.King, out int val) ? val+1 : 1; }
				if(AtualDeck.Slot2.God != ""){UnitUtilizadas[AtualDeck.Slot2.God] = UnitUtilizadas.TryGetValue(AtualDeck.Slot2.God, out int val) ? val+1 : 1; }
				break;
			case 2:
				if(AtualDeck.Slot3.Civil != ""){UnitUtilizadas[AtualDeck.Slot3.Civil] = UnitUtilizadas.TryGetValue(AtualDeck.Slot3.Civil, out int val) ? val+1 : 1; }
				if(AtualDeck.Slot3.Soldier != ""){UnitUtilizadas[AtualDeck.Slot3.Soldier] = UnitUtilizadas.TryGetValue(AtualDeck.Slot3.Soldier, out int val) ? val+1 : 1; }
				if(AtualDeck.Slot3.Combatant != ""){UnitUtilizadas[AtualDeck.Slot3.Combatant] = UnitUtilizadas.TryGetValue(AtualDeck.Slot3.Combatant, out int val) ? val+1 : 1; }
				if(AtualDeck.Slot3.General != ""){UnitUtilizadas[AtualDeck.Slot3.General] = UnitUtilizadas.TryGetValue(AtualDeck.Slot3.General, out int val) ? val+1 : 1; }
				if(AtualDeck.Slot3.King != ""){UnitUtilizadas[AtualDeck.Slot3.King] = UnitUtilizadas.TryGetValue(AtualDeck.Slot3.King, out int val) ? val+1 : 1; }
				if(AtualDeck.Slot3.God != ""){UnitUtilizadas[AtualDeck.Slot3.God] = UnitUtilizadas.TryGetValue(AtualDeck.Slot3.God, out int val) ? val+1 : 1; }
				break;
			case 3:
				if(AtualDeck.Slot4.Civil != ""){UnitUtilizadas[AtualDeck.Slot4.Civil] = UnitUtilizadas.TryGetValue(AtualDeck.Slot4.Civil, out int val) ? val+1 : 1; }
				if(AtualDeck.Slot4.Soldier != ""){UnitUtilizadas[AtualDeck.Slot4.Soldier] = UnitUtilizadas.TryGetValue(AtualDeck.Slot4.Soldier, out int val) ? val+1 : 1; }
				if(AtualDeck.Slot4.Combatant != ""){UnitUtilizadas[AtualDeck.Slot4.Combatant] = UnitUtilizadas.TryGetValue(AtualDeck.Slot4.Combatant, out int val) ? val+1 : 1; }
				if(AtualDeck.Slot4.General != ""){UnitUtilizadas[AtualDeck.Slot4.General] = UnitUtilizadas.TryGetValue(AtualDeck.Slot4.General, out int val) ? val+1 : 1; }
				if(AtualDeck.Slot4.King != ""){UnitUtilizadas[AtualDeck.Slot4.King] = UnitUtilizadas.TryGetValue(AtualDeck.Slot4.King, out int val) ? val+1 : 1; }
				if(AtualDeck.Slot4.God != ""){UnitUtilizadas[AtualDeck.Slot4.God] = UnitUtilizadas.TryGetValue(AtualDeck.Slot4.God, out int val) ? val+1 : 1; }
				break;
			case 4:
				if(AtualDeck.Slot5.Civil != ""){UnitUtilizadas[AtualDeck.Slot5.Civil] = UnitUtilizadas.TryGetValue(AtualDeck.Slot5.Civil, out int val) ? val+1 : 1; }
				if(AtualDeck.Slot5.Soldier != ""){UnitUtilizadas[AtualDeck.Slot5.Soldier] = UnitUtilizadas.TryGetValue(AtualDeck.Slot5.Soldier, out int val) ? val+1 : 1; }
				if(AtualDeck.Slot5.Combatant != ""){UnitUtilizadas[AtualDeck.Slot5.Combatant] = UnitUtilizadas.TryGetValue(AtualDeck.Slot5.Combatant, out int val) ? val+1 : 1; }
				if(AtualDeck.Slot5.General != ""){UnitUtilizadas[AtualDeck.Slot5.General] = UnitUtilizadas.TryGetValue(AtualDeck.Slot5.General, out int val) ? val+1 : 1; }
				if(AtualDeck.Slot5.King != ""){UnitUtilizadas[AtualDeck.Slot5.King] = UnitUtilizadas.TryGetValue(AtualDeck.Slot5.King, out int val) ? val+1 : 1; }
				if(AtualDeck.Slot5.God != ""){UnitUtilizadas[AtualDeck.Slot5.God] = UnitUtilizadas.TryGetValue(AtualDeck.Slot5.God, out int val) ? val+1 : 1; }
				break;
			}
		}
		//if(PlayerInventaryAction.ContainsKey(i.Id)!= true)PlayerInventaryAction.Add(i.Id , i);}
		if (Player.Decks != null) foreach (Zyan.PlayerDecks i in Player.Decks){if(PlayerInventaryDeck.ContainsKey(i.timeName)!= true)PlayerInventaryDeck.Add(i.timeName , i);}}
	/// --------------------------------------------------///
	/// 
	/// Atualiza dados do dicionario para o player e salva ----------///
	[Button]
	public void DicToPlayer(){
		Player.InventaryCards.Clear();
		foreach(KeyValuePair<string, Zyan.IdIndex> i in PlayerInventaryUnit){Player.InventaryCards.Add(i.Value);}
		Player.InventarySpell.Clear();
		foreach(KeyValuePair<string, Zyan.IdIndex> i in PlayerInventarySpell){Player.InventarySpell.Add(i.Value);}
		Player.InventaryEquip.Clear();
		foreach(KeyValuePair<string, Zyan.IdIndex> i in PlayerInventaryEquip){Player.InventaryEquip.Add(i.Value);}
		Player.InventaryActionCards.Clear();
		foreach(KeyValuePair<string, Zyan.IdIndex> i in PlayerInventaryAction){Player.InventaryActionCards.Add(i.Value);}
		//DeckInventary._Decks.Clear();
		//foreach(KeyValuePair<string, Zyan.PlayerDecks> i in PlayerInventaryDeck){DeckInventary._Decks.Add(i.Value);}
		SavePlayerData();}
	/// --------------------------------------------------///
	/// 
	///Adiciona itens ao inventario e salva --------------///
	[Button]
	public void addToPlayer( string card , string cardType){
		switch (cardType){
		case "Card":
			PlayerInventaryUnit[card].Quantidade ++;
			break;
		case "Equip":
			PlayerInventaryEquip[card].Quantidade ++;
			break;
		case "Action":
			PlayerInventaryAction[card].Quantidade ++;
			break;
		case "Spell":
			PlayerInventarySpell[card].Quantidade ++;
			break;}
		DicToPlayer();}
		
	[Button]
	public void addToPlayer( List<string> deck , string cardType){
		switch (cardType){
		case "Card":
			foreach (string i in deck){PlayerInventaryUnit[i].Quantidade ++;}
			break;
		case "Equip":
			foreach (string i in deck){PlayerInventaryEquip[i].Quantidade ++;}
			break;
		case "Action":
			foreach (string i in deck){PlayerInventaryAction[i].Quantidade ++;}
			break;
		case "Spell":
			foreach (string i in deck){PlayerInventarySpell[i].Quantidade ++;}
			break;}
		DicToPlayer();}
		
	[Button]
	public void addToPlayer( PlayerDeckOBJ deck){
		if (deck.Deck.Slot1.Civil != "")PlayerInventaryUnit[deck.Deck.Slot1.Civil].Quantidade ++;
		if (deck.Deck.Slot1.Soldier != "")PlayerInventaryUnit[deck.Deck.Slot1.Soldier].Quantidade ++;
		if (deck.Deck.Slot1.Combatant != "")PlayerInventaryUnit[deck.Deck.Slot1.Combatant].Quantidade ++;
		if (deck.Deck.Slot1.General != "")PlayerInventaryUnit[deck.Deck.Slot1.General].Quantidade ++;
		if (deck.Deck.Slot1.King != "")PlayerInventaryUnit[deck.Deck.Slot1.King].Quantidade ++;
		if (deck.Deck.Slot1.God != "")PlayerInventaryUnit[deck.Deck.Slot1.God].Quantidade ++;
		if (deck.Deck.Slot2.Civil != "")PlayerInventaryUnit[deck.Deck.Slot2.Civil].Quantidade ++;
		if (deck.Deck.Slot2.Soldier != "")PlayerInventaryUnit[deck.Deck.Slot2.Soldier].Quantidade ++;
		if (deck.Deck.Slot2.Combatant != "")PlayerInventaryUnit[deck.Deck.Slot2.Combatant].Quantidade ++;
		if (deck.Deck.Slot2.General != "")PlayerInventaryUnit[deck.Deck.Slot2.General].Quantidade ++;
		if (deck.Deck.Slot2.King != "")PlayerInventaryUnit[deck.Deck.Slot2.King].Quantidade ++;
		if (deck.Deck.Slot2.God != "")PlayerInventaryUnit[deck.Deck.Slot2.God].Quantidade ++;
		if (deck.Deck.Slot3.Civil != "")PlayerInventaryUnit[deck.Deck.Slot3.Civil].Quantidade ++;
		if (deck.Deck.Slot3.Soldier != "")PlayerInventaryUnit[deck.Deck.Slot3.Soldier].Quantidade ++;
		if (deck.Deck.Slot3.Combatant != "")PlayerInventaryUnit[deck.Deck.Slot3.Combatant].Quantidade ++;
		if (deck.Deck.Slot3.General != "")PlayerInventaryUnit[deck.Deck.Slot3.General].Quantidade ++;
		if (deck.Deck.Slot3.King != "")PlayerInventaryUnit[deck.Deck.Slot3.King].Quantidade ++;
		if (deck.Deck.Slot3.God != "")PlayerInventaryUnit[deck.Deck.Slot3.God].Quantidade ++;
		if (deck.Deck.Slot4.Civil != "")PlayerInventaryUnit[deck.Deck.Slot4.Civil].Quantidade ++;
		if (deck.Deck.Slot4.Soldier != "")PlayerInventaryUnit[deck.Deck.Slot4.Soldier].Quantidade ++;
		if (deck.Deck.Slot4.Combatant != "")PlayerInventaryUnit[deck.Deck.Slot4.Combatant].Quantidade ++;
		if (deck.Deck.Slot4.General != "")PlayerInventaryUnit[deck.Deck.Slot4.General].Quantidade ++;
		if (deck.Deck.Slot4.King != "")PlayerInventaryUnit[deck.Deck.Slot4.King].Quantidade ++;
		if (deck.Deck.Slot4.God != "")PlayerInventaryUnit[deck.Deck.Slot4.God].Quantidade ++;
		if (deck.Deck.Slot5.Civil != "")PlayerInventaryUnit[deck.Deck.Slot5.Civil].Quantidade ++;
		if (deck.Deck.Slot5.Soldier != "")PlayerInventaryUnit[deck.Deck.Slot5.Soldier].Quantidade ++;
		if (deck.Deck.Slot5.Combatant != "")PlayerInventaryUnit[deck.Deck.Slot5.Combatant].Quantidade ++;
		if (deck.Deck.Slot5.General != "")PlayerInventaryUnit[deck.Deck.Slot5.General].Quantidade ++;
		if (deck.Deck.Slot5.King != "")PlayerInventaryUnit[deck.Deck.Slot5.King].Quantidade ++;
		if (deck.Deck.Slot5.God != "")PlayerInventaryUnit[deck.Deck.Slot5.God].Quantidade ++;
		if (deck.Deck.Slot1.Equip != "")PlayerInventaryEquip[deck.Deck.Slot1.Equip].Quantidade ++;
		if (deck.Deck.Slot2.Equip != "")PlayerInventaryEquip[deck.Deck.Slot2.Equip].Quantidade ++;
		if (deck.Deck.Slot3.Equip != "")PlayerInventaryEquip[deck.Deck.Slot3.Equip].Quantidade ++;
		if (deck.Deck.Slot4.Equip != "")PlayerInventaryEquip[deck.Deck.Slot4.Equip].Quantidade ++;
		if (deck.Deck.Slot5.Equip != "")PlayerInventaryEquip[deck.Deck.Slot5.Equip].Quantidade ++;
		if (deck.Deck.SpellTrap.Length>0 && deck.Deck.SpellTrap[0].id != "")PlayerInventarySpell[deck.Deck.SpellTrap[0].id].Quantidade ++;
		if (deck.Deck.SpellTrap.Length>0 && deck.Deck.SpellTrap[1].id != "")PlayerInventarySpell[deck.Deck.SpellTrap[1].id].Quantidade ++;
		if (deck.Deck.SpellTrap.Length>1 && deck.Deck.SpellTrap[2].id != "")PlayerInventarySpell[deck.Deck.SpellTrap[2].id].Quantidade ++;
		if (deck.Deck.SpellTrap.Length>2 && deck.Deck.SpellTrap[3].id != "")PlayerInventarySpell[deck.Deck.SpellTrap[3].id].Quantidade ++;
		if (deck.Deck.SpellTrap.Length>3 && deck.Deck.SpellTrap[4].id != "")PlayerInventarySpell[deck.Deck.SpellTrap[4].id].Quantidade ++;
		DicToPlayer();}
	/// --------------------------------------------------///
	/// 
	/// Load Data----------------------------------------///
	[Button]
	public void LoadPlayerDATA(){
		//LoadInventary();
		string content = JsonReader.ReadFile (JsonReader.GetPath ("Player.mk"));
		if (string.IsNullOrEmpty (content) || content == "{}") {
			Debug.Log("Save nao existe, criando um novo");
			ResetStatus();
			JsonReader.SaveToJSONPlayer(Player, "Player.mk");
		}else {
			Player = JsonReader.ReadFromJSONPlayer("Player.mk");
			Debug.Log("Save carregado, jogador: "+ Player.Name);}
		DicUpdate();
	}
	/// --------------------------------------------------///
	/// Save Data ---------------------------------------///
	[Button]
	public void SavePlayerData()=>JsonReader.SaveToJSONPlayer(Player, "Player.mk");
	/// --------------------------------------------------///
	
	/// ----------------------------------------------------///
	[Button]
	public void ResetStatus(){
		Player = new Zyan.PlayerInventary{};
		Player.ID = UnityEngine.Random.Range( 0 , 99999999).ToString();
		Player.Name = "Visit"+ Player.ID;
		SetPlayerOBJ();
		//LoadInventary();
		DicUpdate();
	}
	/// ----------------------------------------------------///
	
}
