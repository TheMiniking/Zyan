using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Linq;
using UnityEditor;
using Sirenix.OdinInspector;
using UnityEngine.Events;
using System.Reflection;


public class Zyan : MonoBehaviour
{
	
	/// ------------------------------------------------------- ///
	/// Exemplo de Event funcionando
	/// ------------------------------------------------------- ///
	
	public delegate void OtherEvents(); 			// Variavel Master do Evento
	public event OtherEvents MensagemV1;			// Variavel do Evento
	[ShowInInspector] public UnityEvent MensagemV2;	// Variavel da UnityEvent , pode usar Inspetor para adicionar Eventos.
	[Button] public void EventMensage() { 
		MensagemV2?.Invoke();				// Como chamar os eventos, usado por outros scripts.
		MensagemV1?.Invoke();
	}
	void OnEnable(){
		MensagemV1 += Test2;				// Adiciona para ultilizaçao do evento
		MensagemV2.AddListener(Test);		// Adiciona para ultilizaçao do Unityevent via Script [obs: Nao pode ter argumentos]
	} 
	void OnDisable(){
		MensagemV1 -= Test2;				// Remove para evitar erros
		MensagemV2.RemoveAllListeners();	// Remove para evitar erros
	} 
	/// ------------------------------------------------------- ///
	/// Exemplo de Funçao vindo de uma string
	/// ------------------------------------------------------- ///
	/// 
	[Button]
	void StringFuntion(string funtionSTR){
		MethodInfo funtion = typeof(Zyan).GetMethod(funtionSTR);
		Action del = (Action)Delegate.CreateDelegate(typeof(Action), funtion);
		del();
	}
	// Cria uma delegação para o método
	
	
	/// ------------------------------------------------------- ///
	/// Evento do teste
	/// ------------------------------------------------------- ///

	public static void Test(string txt) => Debug.Log("Funcionou ! : " + txt);
	public static void Test() => Debug.Log("Funcionou !");
	public static void Test2() => Debug.Log("Funcionou ! Outra versao!");
	
	/// ------------------------------------------------------- ///
	
	[Serializable]
	public class PlayerInventary
	{
		public string Name;
		public string ID;
		public InventaryTesouros iTesouros; 
		public InventaryModifications[] iModifications;
		public List<IdIndex> InventaryCards;
		public List<IdIndex> InventaryActionCards;
		public List<IdIndex> InventarySpell;
		public List<IdIndex> InventaryEquip;
		public List<string> HistoryMode;
		public List<PlayerDecks> Decks;
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
		public List<IdIndex> IndexList; 
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
		public SpellClass[] SpellTrap; 
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
	
	[Serializable]
	public class BoosterItem{
		public string Nome;
		public string[] List;
	}
	
	[Serializable]
	public class BoosterItemNode{
		public BoosterItem[] Booster;
	}
}
