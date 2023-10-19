using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;
using Sirenix.OdinInspector;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

[CreateAssetMenu (fileName = "SceneVariable", menuName = "Zyan Assets/Create SceneVarable")]
public class SceneVariables_Battle : ScriptableObject
{
	[Title("Scene Variables")]
	[ShowInInspector] public TMP_Text debugText;
	[ShowInInspector] private GameObject scenesVariables;
    
	[Title("Player Variables")]
	[ShowInInspector] public PlayerInventaryOBJ playerOBJ ;
	
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
	public List<PlayerDeckOBJ> decksTest = new List<PlayerDeckOBJ>();
    
    
	public class SVBLog{
		public string Visual;
		public string Detalhado;
	} 
	
	public string filenamePlayer = "Player.mk" ;
	public string filenameIdCards = "Card.mk" ;
	public string filenameIdSpell = "Spell.mk" ;
	public string filenameIdAction = "Action.mk" ;
	public string filenameIdEquip = "Equip.mk" ;

	[Title("Result Battle Variables")]
	public bool endDuel = false;
	public string winningName;
	public string winningID;
	public List<string> rewards = new List<string>();
	
	public Manger_JsonUnits json;
	//// Start is called before the first frame update
	[Button]public void StartTestDuel(){
		var pv1 = new BattlePlayerData();
		var pv2 = new BattlePlayerData();
		pv1.ID = ""+Random.Range(11111111,100000000);
		pv1.Name = Random.Range(0,2)==0?"Miniking":"SuperMiniking";
		pv1.Rank = Random.Range(0,2)==0?"Ruler":"King";
		pv1.deck = decksTest[Random.Range(0,decksTest.Count)].Deck;
		pv2.ID = ""+Random.Range(11111111,100000000);
		pv2.Name = Random.Range(0,2)==0?"Manolo":"Outro Cara";
		pv2.Rank = Random.Range(0,2)==0?"Ruler":"King";
		pv2.deck = decksTest[Random.Range(0,decksTest.Count)].Deck;
		var rew = new List<string>();
		rew.Add("1500,Coin");
        _ = StartDuel(pv1, pv2, rew);
	}
	
	public void ResetVar(){
		p1 = new BattlePlayerData();
		p2 = new BattlePlayerData();
		rewards.Clear();
		turnCount = 0;
		PlayerEnergy = 0;
		EnemyEnergy= 0;
	}
	
	[Button] 
	public async Task StartDuel(BattlePlayerData p1Data, BattlePlayerData p2Data, List<string> rewardsData){
		AsyncOperation loadBattle = SceneManager.LoadSceneAsync(2);
		ResetVar();
		vsAI = false;
		p1 = p1Data;
		p2 = p2Data;
		if (p1.deck.OBJ !=null )p1.deck = p1.deck.OBJ.Deck;
		if (p2.deck.OBJ !=null )p2.deck = p2.deck.OBJ.Deck;
		rewards = rewardsData;
	}
	
	public void LoadID(){
		var x = Random.Range(0, 2);
		if (x > 0){
			atualTurn = "P1";
			atualTurnID = p1.ID;
			// Ativar Anuncio - P1 Start Game!
			Debug.Log(atualTurn);}
		else{
			atualTurn = "P2";
			atualTurnID =p2.ID;
			// Ativar Anuncio - P2 Start Game!
			Debug.Log(atualTurn);} 
		FindObjectOfType<ManagerTurn>().NextTurn();
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
		public Zyan.PlayerDecks deck ;
	}
	
}
