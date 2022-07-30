using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class JsonFile 
{	
	public TextAsset txtJson;
	public TextAsset txtJsonActionSpell;
	public TextAsset txtJsonDialogo;
	public int countNodes;
	public static int index;
	public int indexAction;
	public int countNodesAction;
	public int indexSpell;
	public int countNodesSpell;
	public int indexEquip;
	public int countNodesEquip;
	public int indexDialogo;
	public int countNodesDialogo;
	public static string[] Units;
	
	public string id;
	public string name;
	public string type;
	public string rank;
	public string element;
	public string special;
	public int atk;
	public int def;
	public int life;
	public string status;
	public string effect1;
	public string effect2;
	public string evolution;
	public string evolCust1;
	public string evolCust2;
	public string evolCust3;
	public string evol1;
	public string evol2;
	public string evol3;
	public string rarityUp;
	public string rarity;
	
	public int cust;
	public string activation;
	public string time;
	public string description;
	public string script;
	
	public string subType;
	public string restriction;
	
	public string boost;
	public string deboost;
	public string hability;
	
	public string A0;
	public string A1;
	public string A2;
	public string A3;
	public string A4;
	public string A5;
	public string A6;
	public string A7;
	public string A8;
	public string A9;
	public string A10;

	[System.Serializable]
	public class JsonFileData
    {
		public List<CardList> Cards;
		public List<ActionCardList> ActionCard;
		public List<SpellCardList> SpellTrap;
		public List<EquipCardList> Equip;
		public List<DialogoList> Dialogo;
	}

	[System.Serializable]
	public class CardList
	{
		public string id;
		public string Name;
		public string Type;
		public string Rank;
		public string Element;
		public string Special;
		public int ATK;
		public int DEF;
		public int Life;
		public string Status;
		public string Effect1;
		public string Effect2;
		public string Evolution;
		public string EvolCust1;
		public string EvolCust2;
		public string EvolCust3;
		public string Evol1;
		public string Evol2;
		public string Evol3;
		public string RarityUp;
		public string Rarity;
	} 

	[System.Serializable]
	public class CardListII
	{
		public CardList[] Cards; 
	}
	//public NodeList myNodeList = new NodeList();
	
	[System.Serializable]
	public class ActionCardList
	{
		public string id;
		public string Name;
		public int Cust;
		public string Activation;
		public string Time;
		public string Description;
		public string Script;
		public string Rarity;
	} 
	[System.Serializable]
	public class ActionNodeList
	{
		public ActionCardList[] ActionCard; 
	}
	//public ActionNodeList myActionNodeList = new ActionNodeList();
	
	[System.Serializable]
	public class SpellCardList
	{
		public string id;
		public string Name;
		public string Type;
		public string SubType;
		public string Restriction;
		public string Description;
		public string Script;
		public string Rarity;
	} 
	[System.Serializable]
	public class SpellNodeList
	{
		public SpellCardList[] SpellTrap; 
	}
	
	//public SpellNodeList mySpellNodeList = new SpellNodeList();
	
	[System.Serializable]
	public class EquipCardList
	{
		public string id;
		public string Name;
		public string Type;
		public string Restriction;
		public string Description;
		public string Boost;
		public string Deboost;
		public string Hability;
		public string Rarity;
	} 
	[System.Serializable]
	public class EquipNodeList
	{
		public EquipCardList[] Equip; 
	}
	
	//public EquipNodeList myEquipNodeList = new EquipNodeList();
	
	[System.Serializable]
	public class DialogoList
	{
		public string A0;
		public string A1;
		public string A2;
		public string A3;
		public string A4;
		public string A5;
		public string A6;
		public string A7;
		public string A8;
		public string A9;
		public string A10;
	} 
	[System.Serializable]
	public class DialogoNodeList
	{
		public DialogoList[] Dialogo; 
	}
	
	//public DialogoNodeList myDialogoNodeList = new DialogoNodeList();

	public CardList myCardList = new CardList();
	public ActionCardList myActionNodeList = new ActionCardList();
	public SpellCardList mySpellNodeList = new SpellCardList();
	public EquipCardList myEquipNodeList = new EquipCardList(); 
	public DialogoList myDialogoNodeList = new DialogoList();
	void Start()
	{
		//myNodeList = JsonUtility.FromJson<PlayerList>(txtJson.text);
		myCardList = JsonUtility.FromJson<CardList>(txtJson.text);
		index = 0;

		myActionNodeList = JsonUtility.FromJson<ActionCardList>(txtJsonActionSpell.text);
		indexAction = 0;

		mySpellNodeList = JsonUtility.FromJson<SpellCardList>(txtJsonActionSpell.text);
		indexSpell = 0;

		myEquipNodeList = JsonUtility.FromJson<EquipCardList>(txtJsonActionSpell.text);
		indexEquip = 0;

		myDialogoNodeList = JsonUtility.FromJson<DialogoList>(txtJsonDialogo.text);
		indexDialogo = 0;
		/*
		foreach (CardList x in myNodeList.people)
		{
			Debug.Log(x.lastName);
			Debug.Log(x.number);
		}*/
		//Debug.Log(myNodeList.people[2].lastName);
	}
	
	/*
	public void GetUnit(int z)
	{
		id		= myNodeList.cards[z].id;
		name	= myNodeList.cards[z].Name;
		type	= myNodeList.cards[z].Type;
		rank	= myNodeList.cards[z].Rank;
		element = myNodeList.cards[z].Element;
		special = myNodeList.cards[z].Special;
		life	= myNodeList.cards[z].Life;
		atk		= myNodeList.cards[z].ATK;
		def		= myNodeList.cards[z].DEF;
		status	= myNodeList.cards[z].Status;
		effect1 = myNodeList.cards[z].Effect1;
		effect2 = myNodeList.cards[z].Effect2;
		evolution = myNodeList.cards[z].Evolution;
		evolCust1 = myNodeList.cards[z].EvolCust1;
		evolCust2 = myNodeList.cards[z].EvolCust2;
		evolCust3 = myNodeList.cards[z].EvolCust3;
		evol1	= myNodeList.cards[z].Evol1;
		evol2	= myNodeList.cards[z].Evol2;
		evol3	= myNodeList.cards[z].Evol3;
		rarityUp = myNodeList.cards[z].RarityUp;
		rarity	= myNodeList.cards[z].Rarity;
		string[] unit = { id, name,type,rank,element,special,""+ life , "" + atk , "" + def ,status,effect1,effect2,evolution,evol1,evol2,evol3,rarityUp,rarity};
		Units = unit;
		//return unit;
	}

	public void UpdateNodes()
	{
		id = myNodeList.cards[index].id;
		name = myNodeList.cards[index].Name;
		type = myNodeList.cards[index].Type;
		rank = myNodeList.cards[index].Rank;
		element = myNodeList.cards[index].Element;
		special = myNodeList.cards[index].Special;
		life = myNodeList.cards[index].Life;
		atk = myNodeList.cards[index].ATK;
		def = myNodeList.cards[index].DEF;
		status = myNodeList.cards[index].Status;
		effect1 = myNodeList.cards[index].Effect1;
		effect2 = myNodeList.cards[index].Effect2;
		evolution = myNodeList.cards[index].Evolution;
		evolCust1 = myNodeList.cards[index].EvolCust1;
		evolCust2 = myNodeList.cards[index].EvolCust2;
		evolCust3 = myNodeList.cards[index].EvolCust3;
		evol1 = myNodeList.cards[index].Evol1;
		evol2 = myNodeList.cards[index].Evol2;
		evol3 = myNodeList.cards[index].Evol3;
		rarityUp = myNodeList.cards[index].RarityUp;
		rarity = myNodeList.cards[index].Rarity;
		
	}
	
	public void UpdateNodesAction()
	{
		id = myActionNodeList.actionCard[indexAction].id;
		name = myActionNodeList.actionCard[indexAction].name;
		cust = myActionNodeList.actionCard[indexAction].cust;
		activation = myActionNodeList.actionCard[indexAction].activation;
		time = myActionNodeList.actionCard[indexAction].time;
		description = myActionNodeList.actionCard[indexAction].description;
		script = myActionNodeList.actionCard[indexAction].script;
		rarity = myActionNodeList.actionCard[indexAction].rarity;
		
	}
	
	public void UpdateNodesSpell()
	{
		id = mySpellNodeList.spellTrap[indexSpell].id;
		name = mySpellNodeList.spellTrap[indexSpell].name;
		type = mySpellNodeList.spellTrap[indexSpell].type;
		subType	= mySpellNodeList.spellTrap[indexSpell].subType;
		restriction = mySpellNodeList.spellTrap[indexSpell].restriction;
		description = mySpellNodeList.spellTrap[indexSpell].description;
		script = mySpellNodeList.spellTrap[indexSpell].script;
		rarity = mySpellNodeList.spellTrap[indexSpell].rarity;
		
	}
	
	public void UpdateNodesEquip()
	{
		id = myEquipNodeList.equip[indexEquip].id;
		name = myEquipNodeList.equip[indexEquip].name;
		type = myEquipNodeList.equip[indexEquip].type;
		restriction = myEquipNodeList.equip[indexEquip].restriction;
		description = myEquipNodeList.equip[indexEquip].description;
		boost	= myEquipNodeList.equip[indexEquip].boost;
		deboost = myEquipNodeList.equip[indexEquip].deboost;
		hability = myEquipNodeList.equip[indexEquip].hability;
		rarity = myEquipNodeList.equip[indexEquip].rarity;
		
	}
	
	public void UpdateNodesDialogo()
	{
		a0 = myDialogoNodeList.dialogo[indexDialogo].a0;
		a1 = myDialogoNodeList.dialogo[indexDialogo].a1;
		a2 = myDialogoNodeList.dialogo[indexDialogo].a2;
		a3 = myDialogoNodeList.dialogo[indexDialogo].a3;
		a4 = myDialogoNodeList.dialogo[indexDialogo].a4;
		a5 = myDialogoNodeList.dialogo[indexDialogo].a5;
		a6 = myDialogoNodeList.dialogo[indexDialogo].a6;
		a7 = myDialogoNodeList.dialogo[indexDialogo].a7;
		a8 = myDialogoNodeList.dialogo[indexDialogo].a8;
		a9 = myDialogoNodeList.dialogo[indexDialogo].a9;
		a10 = myDialogoNodeList.dialogo[indexDialogo].a10;
	}
	*/

	public string path = "Assets/sample4.txt";
	public void Save()
	{
		var conteudo = JsonUtility.ToJson(this, true);
		File.WriteAllText(path, conteudo);
	}
	
	public void Load()
	{	
		var conteudo = File.ReadAllText(path);
		var x = JsonUtility.FromJson<JsonFile>(conteudo);
	}	
}