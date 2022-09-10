using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;
using Sirenix.OdinInspector;

public class SceneVariables_Battle : MonoBehaviour
{
	[Title("Scene Variables")]
    [ShowInInspector] public GameObject uiUnitsManager;
	[ShowInInspector] public GameObject battleManager;
    [ShowInInspector] public GameObject anuncio;
	[ShowInInspector] public TMP_Text debugText;
	[ShowInInspector] private GameObject scenesVariables;
    
	[Title("Player Variables")]
	[ShowInInspector] public static Zyan.PlayerInventary playerData = new Zyan.PlayerInventary(){};
	[ShowInInspector] public static string playerID;
	[ShowInInspector] public static Zyan.PlayerDecks _PlayerDeck ;
	[ShowInInspector] public static PlayerInventaryOBJ playerOBJ ;
	
	[Title("Enemy Variables")]
	[ShowInInspector] public static string enemyName;
	[ShowInInspector] public static string enemyID;
	[ShowInInspector] public static Zyan.PlayerDecks _EnemyDeck ;
	
	[Title("Battle Variables")]
    [ShowInInspector] public static bool vsAI = true;
	[ShowInInspector] public static BattlePlayerData p1 ;
	[ShowInInspector] public static BattlePlayerData p2 ;
	[ShowInInspector] public static string atualTurn;
	[ShowInInspector] public static string atualTurnID;
    [ShowInInspector] public static string atualTurnDisplay;
    [ShowInInspector] public static int turnCount;
    [ShowInInspector] public static GameObject _LastTerreno;
	[ShowInInspector] public static GameObject _TerrenoAnterior;
	[ShowInInspector] public static Unit _LastSummonUnit;
	[ShowInInspector] public static UnitField _UnitToMove;
	[ShowInInspector] public static int PlayerEnergy;
	[ShowInInspector] public static int EnemyEnergy;
	
	[Title("Battle Variables")]
	[ShowInInspector] public static string[] DebugLogList = new string[] {};
	
	[Title("In test - Não apagar")]
    [ShowInInspector] public static Dictionary<string, string> idList = new Dictionary<string, string>();
    
	
	public static string filenamePlayer = "Player.mk" ;
	public static string filenameIdCards = "Card.mk" ;
	public static string filenameIdSpell = "Spell.mk" ;
	public static string filenameIdAction = "Action.mk" ;
	public static string filenameIdEquip = "Equip.mk" ;

	public Manger_JsonUnits json;
    //// Start is called before the first frame update
    void Start()
	{
		p1 = new BattlePlayerData();
		p2 = new BattlePlayerData();
		if (scenesVariables != null) 
		{GameObject.Destroy(this.gameObject);}
		else {scenesVariables = this.gameObject;
			DontDestroyOnLoad(scenesVariables);}
		LoadAssetPlayer();
		playerOBJ.LoadPlayerDATA();
		playerOBJ.LoadInventary();
		playerData = playerOBJ.Player;
		playerID = playerData.ID;
		enemyID = ""+ Random.Range(0,99999999);
		enemyName = "AI Lv 1";
		p1.ID = playerID;
		p1.IsPlayer1 = true;
		p1.Name = playerData.Name;
		p2.ID = enemyID;
		p2.IsPlayer1 = false;
		p2.Name = enemyName;
		p1.Rank = "Civil";
		p2.Rank = "Civil";
    }

	void Awake()
	{
		var t = Resources.Load<PlayerDeckOBJ>("Player/Deck/Dragons");
		_PlayerDeck = t.Deck;
		var r = Resources.Load<PlayerDeckOBJ>("Player/Deck/Warriors");
		_EnemyDeck = r.Deck;
		var terrenoMisturados = FindObjectsOfType<Terreno>();
		Terreno[] terrenosNormal = new Terreno[0];
		foreach ( Terreno tr in terrenoMisturados)
		{
			if (tr._HexType == "Normal")
			{ ArrayUtility.Add<Terreno>( ref terrenosNormal , tr);}
		}
		foreach ( Zyan.SpellClass sp in t.SpellTrap)
		{
			int w = Random.Range(0,terrenosNormal.Length);
			terrenosNormal[w]._SpellData = sp;
			ArrayUtility.Remove<Terreno>(ref terrenosNormal, terrenosNormal[w]);
		}
		foreach ( Zyan.SpellClass sp2 in r.SpellTrap)
		{
			int w2 = Random.Range(0,terrenosNormal.Length);
			terrenosNormal[w2]._SpellData = sp2;
			ArrayUtility.Remove<Terreno>(ref terrenosNormal, terrenosNormal[w2]);
		}
	}
	
	[Button]
	private static void LoadAssetPlayer()
	{
		playerOBJ = AssetDatabase.LoadAssetAtPath<PlayerInventaryOBJ>("Assets/Resources/Player/Player.asset");
	}
	
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
	
	public void DebugText(string tx){
		ArrayUtility.Add<string>(ref DebugLogList, tx);
		debugText.text = tx;
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
