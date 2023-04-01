using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;
using Sirenix.OdinInspector;

[CreateAssetMenu (fileName = "SceneVariable", menuName = "Zyan Assets/Create SceneVarable")]
public class SceneVariables_Battle : ScriptableObject
{
	[Title("Scene Variables")]
	[ShowInInspector] public TMP_Text debugText;
	[ShowInInspector] private GameObject scenesVariables;
    
	[Title("Player Variables")]
	[ShowInInspector] public string playerID;
	[ShowInInspector] public Zyan.PlayerDecks _PlayerDeck ;
	[ShowInInspector] public PlayerInventaryOBJ playerOBJ ;
	
	[Title("Enemy Variables")]
	[ShowInInspector] public string enemyName;
	[ShowInInspector] public string enemyID;
	[ShowInInspector] public Zyan.PlayerDecks _EnemyDeck ;
	
	[Title("Battle Variables")]
    [ShowInInspector] public bool vsAI = true;
	[ShowInInspector] public BattlePlayerData p1 = new BattlePlayerData();
	[ShowInInspector] public BattlePlayerData p2 = new BattlePlayerData();
	[ShowInInspector] public string atualTurn;
	[ShowInInspector] public string atualTurnID;
    [ShowInInspector] public string atualTurnDisplay;
    [ShowInInspector] public int turnCount;
    [ShowInInspector] public GameObject _LastTerreno;
	[ShowInInspector] public GameObject _TerrenoAnterior;
	[ShowInInspector] public Unit _LastSummonUnit;
	[ShowInInspector] public UnitField _UnitToMove;
	[ShowInInspector] public int PlayerEnergy;
	[ShowInInspector] public int EnemyEnergy;
	
	[Title("Battle Variables")]
	[ShowInInspector] public Dictionary<string, SVBLog> DebugLogList = new Dictionary<string, SVBLog> {};
	
	[Title("In test - Não apagar")]
    [ShowInInspector] public Dictionary<string, string> idList = new Dictionary<string, string>();
    
	public class SVBLog{
		public string Visual;
		public string Detalhado;
	} 
	
	public string filenamePlayer = "Player.mk" ;
	public string filenameIdCards = "Card.mk" ;
	public string filenameIdSpell = "Spell.mk" ;
	public string filenameIdAction = "Action.mk" ;
	public string filenameIdEquip = "Equip.mk" ;

	public Manger_JsonUnits json;
	//// Start is called before the first frame update
	[Button]
	public void Start()
	{
		playerOBJ.LoadPlayerDATA();
		_PlayerDeck = playerOBJ.AtualDeck;
		var r = Resources.Load<PlayerDeckOBJ>("Player/Deck/Warriors");
		_EnemyDeck = r.Deck;
		var terrenoMisturados = FindObjectsOfType<Terreno>();
		List<Terreno> terrenosNormal = new List<Terreno>{};
		foreach ( Terreno tr in terrenoMisturados)
		{
			if (tr._HexType == "Normal")
			{ terrenosNormal.Add( tr);}
		}
		foreach ( Zyan.SpellClass sp in _PlayerDeck.SpellTrap)
		{
			int w = Random.Range(0,terrenosNormal.Count);
			terrenosNormal[w]._SpellData = sp;
			terrenosNormal.Remove(terrenosNormal[w]);
		}
		foreach ( Zyan.SpellClass sp2 in _EnemyDeck.SpellTrap)
		{
			int w2 = Random.Range(0,terrenosNormal.Count);
			terrenosNormal[w2]._SpellData = sp2;
			terrenosNormal.Remove(terrenosNormal[w2]);
		}
		playerID = playerOBJ.Player.ID;
		enemyID = ""+ Random.Range(0,99999999);
		enemyName = "AI Lv 1";
		p1.ID = playerID;
		p1.IsPlayer1 = true;
		p1.Name = playerOBJ.Player.Name;
		p2.ID = enemyID;
		p2.IsPlayer1 = false;
		p2.Name = enemyName;
		p1.Rank = "Civil";
		p2.Rank = "Civil";
		turnCount = 0;
		PlayerEnergy = 0;
		EnemyEnergy= 0;
	}
	#if (UNITY_EDITOR) 
	[Button]
	private void LoadAssetPlayer()
	{
		playerOBJ = AssetDatabase.LoadAssetAtPath<PlayerInventaryOBJ>("Assets/Resources/Player/Player.asset");
	}
	#endif
	public void LoadID()
	{
		p1.ID = playerID;
		p2.ID = enemyID;
		var x = Random.Range(0, 2);
		if (x > 0)
		{
			atualTurn = "P1";
			atualTurnID = playerID;
			// Ativar Anuncio - P1 Start Game!
			Debug.Log(atualTurn);
		}
		else
		{
			atualTurn = "P2";
			atualTurnID = enemyID;
			// Ativar Anuncio - P2 Start Game!
			Debug.Log(atualTurn);
		}
		ManagerTurn v = FindObjectOfType<ManagerTurn>();
		v.NextTurn();
	}
	
	public void DebugText(SVBLog tx){
		DebugLogList.Add(DebugLogList.Count.ToString(),tx);
		debugText.text = tx.Visual;
	}
	
	[System.Serializable]
	public class BattlePlayerData{
		public string ID;
		public string Name;
		public string Rank;
		public bool IsPlayer1= false;
		public bool onSummon = false;
		public bool onMove = false;
		public bool canSummon = true;
	}
	
}
