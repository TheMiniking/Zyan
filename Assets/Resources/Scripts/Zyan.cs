using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Linq;
using UnityEditor;
using Sirenix.OdinInspector;
using UnityEngine.Events;


public class Zyan : MonoBehaviour
{
	
	/// ------------------------------------------------------- ///
	/// Exemplo de Event funcionando
	/// ------------------------------------------------------- ///
	
	//public delegate void BattleEvents();
	//
	// Adicionar eventos do tipo UnityEvent
	//[Button] public void AddEventTest()
	//{ var up = FindObjectOfType<Zyan>();
	//	up.Summon.AddListener(TestEvent);}
	//	
	
	[ShowInInspector] public UnityEvent Summon;
	[ShowInInspector] public UnityEvent Moviment;
	
	[Button]
	public void EventSummon() => Summon?.Invoke();
	
	[Button]
	public void EventMoviment() => Moviment?.Invoke();
	
	/// ------------------------------------------------------- ///
	/// Evento do teste
	/// ------------------------------------------------------- ///

	public static void Test(string txt) => Debug.Log("Funcionou ! : " + txt);
	public static void Test() => Debug.Log("Funcionou !");
	
	/// ------------------------------------------------------- ///
	
	[Serializable]
	public class PlayerInventary
	{
		public string Name;
		public string ID;
		public InventaryTesouros iTesouros; 
		public InventaryModifications[] iModifications;
		public IdIndex[] InventaryCards;
		public IdIndex[] InventaryActionCards;
		public IdIndex[] InventarySpell;
		public IdIndex[] InventaryEquip;
		public int[] HistoryMode;
	}
	
	[Serializable]
	public class InventaryTesouros
	{
		public int coin;
		public int gem;
		public int upgradeCuponsC;
		public int upgradeCuponsUC;
		public int upgradeCuponsR;
		public int upgradeCuponsE;
		public int upgradeCuponsL;
		public int materialC;
		public int materialUC;
		public int materialR;
		public int materialE;
		public int materialL;
	}
	
	[Serializable]
	public class InventaryModifications
	{
		public string id;
		public string newModificationLocal;
		public string newModificarionEffect;
		public int newBuffATK;
		public int newBuffDEF;
		public int newBuffLife;
		public string newRarity;
	}
	
	[System.Serializable]
	public class IdIndex
	{
		public int index;
		public string Id;
		public int Quantidade;
	}
	
	[System.Serializable]
	public class IdIndexList
	{
		public IdIndex[] IndexList; 
	}
	
	
	[Serializable]
	public class PlayerDecks
	{
		public string timeName;
		public TimeUnits Slot1;
		public TimeUnits Slot2;
		public TimeUnits Slot3;
		public TimeUnits Slot4;
		public TimeUnits Slot5;
		public PlayerDeckOBJ OBJ;
	}
	
	[Serializable]
	public class TimeUnits
	{
		public string Civil;
		public string Soldier;
		public string Combatant;
		public string General;
		public string King;
		public string God;
		public string Equip;
	}
	
	[Serializable]
	public class EvolutionCheck
	{
		public bool ForbiddenRitual;
		public bool ForbiddenCalice;
		public bool EmergencyEvolution;
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
	
	[System.Serializable]
	public class ActionCardList
	{
		public string id;
		public string Name;
		public int Cust;
		public string Activation;
		public string Time;
		public string Description;
		public string Rarity;
		public string Script;
	} 
	[System.Serializable]
	public class ActionNodeList
	{
		public ActionCardList[] ActionCard; 
	}
	
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
	
	[System.Serializable]
	public class EquipCardList
	{
		public string id;
		public string Name;
		public string Type;
		public string ExclusiveType;
		public string TypeBonus1;
		public string TypeBonus2;
		public string TypeBonus3;
		public string TypeBonus4;
		public string TypeBonus5;
		public string TypeExcesao1;
		public string TypeExcesao2;
		public string TypeExcesao3;
		public string Status;
		public string BoostType;
		public string DeboostBool;
		public string BoostQnt;
		public string Description;
		public string ScriptID;
		public string Rarity;
	} 
	[System.Serializable]
	public class EquipNodeList
	{
		public EquipCardList[] Equip; 
	}
	
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
	
	[Serializable]
	public class ActionClass
	{
		public string id;
		public string Name;
		public int Cust;
		public string Activation;
		public string Time;
		public string Description;
		public string Rarity;
		public string Script;
		public ActionObj OBJ;
	}
	
	[Serializable]
	public class SpellClass
	{
		public string id;
		public string Name;
		public string Type;
		public string SubType;
		public string Restriction;
		public string Description;
		public string Script;
		public string Rarity;
		public SpellObj OBJ;
	}
	
	[Serializable]
	public class UnitClass
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
		public UnitObj OBJ;
	}
	
	[Serializable]
	public class EquipClass{
		public string id ;
		public string Name;
		public string Type;
		public string ExclusiveType;
		public string TypeBonus1;
		public string TypeBonus2;
		public string TypeBonus3;
		public string TypeBonus4;
		public string TypeBonus5;
		public string TypeExcesao1;
		public string TypeExcesao2;
		public string TypeExcesao3;
		public string Status;
		public string BoostType;
		public string DeboostBool;
		public string BoostQnt;
		public string Description;
		public string ScriptID;
		public string Rarity;
		public EquipObj Obj;
	}
	
	[Serializable]
	public class InfoItem{
		public string ID;
		public string Name;
		public string Description;
	}
	
	[Serializable]
	public class InfoitemNode{
		public InfoItem[] ItensPT;
	}
	
}
