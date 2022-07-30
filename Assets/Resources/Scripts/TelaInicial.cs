using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEditor;

public class TelaInicial : MonoBehaviour
{
	public GameObject loadingTela;
	public TMP_Text loading;
	public Image grande;
	public TMP_Text dialogo;
	public TMP_InputField nameT;
	public ZyanControl controle;
	public GameObject mensageObj;
	public IdIndex[] idJoson = new IdIndex[0];
	public string[] starterDeckCards = new string[0];
	public string[] starterDeckAction = new string[0];
	public string[] starterDeckEquip = new string[0];
	public string[] starterDeckSpell = new string[0];
	
	void Start()
	{
		if (SceneVariables_Battle.playerData.Name == "")
		{
			NewGame();
		}else
		{
			LoadGame();
		}
	}
	
	private bool tempinho = false;
	public bool retornou = false;
	void FixedUpdate()
	{
		
		if (tempinho != true)
		{
			StartCoroutine(NextStep());
		}
	}
	
	public IEnumerator NextStep()
	{
		var x = FindObjectOfType<ManagerMensagens>();
		tempinho = true;
		if (x != null)
		{
		yield return new WaitForSecondsRealtime(1f);
		yield return new WaitUntil (() => x.proxima == true );
		NewGame();
		tempinho= false;
		}else
		{
			yield return new WaitForSecondsRealtime(1f);
			NewGame();
			tempinho= false;
		}
		
	}
	
	[SerializeField]private int steps;
	private int linha;
	private int parte;
	public void NewGame()
	{
		//var fala = JsonReader.ReadListFromJSONDialogo();
		
		switch (steps)
		{
		case 0 :
			loading.text = "Carregando ...";
			var jsonID = JsonReader.ReadListFromJSONCard();
			var index = 0;
			foreach (Zyan.CardList g in jsonID)
			{
				IdIndex unico = new IdIndex();
				unico.Id = g.id;
				unico.index = index;
				ArrayUtility.Add<IdIndex>(ref idJoson, unico);
				index++;
			}
			Zyan.IdIndex[] card = new Zyan.IdIndex[0];
			Zyan.IdIndex[] action = new Zyan.IdIndex[0];
			Zyan.IdIndex[] equip = new Zyan.IdIndex[0];
			Zyan.IdIndex[] spell = new Zyan.IdIndex[0];
			ArrayUtility.Add<int>(ref SceneVariables_Battle.playerData.HistoryMode, 1);
			var c = JsonReader.ReadListFromJSONCard();
			var e = JsonReader.ReadListFromJSONEquip();
			var s = JsonReader.ReadListFromJSONSpellTrap();
			var a = JsonReader.ReadListFromJSONAction();
			var index2 = 0;
			foreach (var i in c)
			{
						
				Zyan.IdIndex temp = new Zyan.IdIndex();
				temp.Id = i.id;
				temp.index = index2; 
				Zyan.IdIndex tempI = new Zyan.IdIndex();
				tempI.Id = i.id;
				tempI.index = 0; 
				ArrayUtility.Add<Zyan.IdIndex>(ref SceneVariables_Battle.playerData.InventaryCards, tempI);
				ArrayUtility.Add<Zyan.IdIndex>( ref card , temp);
				index2++;
					
			}
			JsonReader.SaveToJSONIdIndex(card ,SceneVariables_Battle.filenameIdCards);
			index2 =	0;
			foreach (var i in e)
			{
					
				Zyan.IdIndex temp2 = new Zyan.IdIndex();
				temp2.Id = i.id;
				temp2.index = index2; 
				Zyan.IdIndex tempII = new Zyan.IdIndex();
				tempII.Id = i.id;
				tempII.index = 0;
				ArrayUtility.Add<Zyan.IdIndex>(ref SceneVariables_Battle.playerData.InventaryEquip, tempII);
				ArrayUtility.Add<Zyan.IdIndex>( ref  equip, temp2);
				index2++;
			}
			JsonReader.SaveToJSONIdIndex(equip ,SceneVariables_Battle.filenameIdEquip);
			index2 =	0;
			foreach (var i in s)
			{
					
				Zyan.IdIndex temp3 = new Zyan.IdIndex();
				temp3.Id = i.id;
				temp3.index = index2; 
				Zyan.IdIndex tempIII = new Zyan.IdIndex();
				tempIII.Id = i.id;
				tempIII.index = 0;
				ArrayUtility.Add<Zyan.IdIndex>(ref SceneVariables_Battle.playerData.InventarySpell, tempIII);
				ArrayUtility.Add<Zyan.IdIndex>( ref spell , temp3);
				index2++;
			}
			JsonReader.SaveToJSONIdIndex(spell ,SceneVariables_Battle.filenameIdSpell);
			index2 =	0;
			foreach (var i in a)
			{
					
				Zyan.IdIndex temp = new Zyan.IdIndex();
				temp.Id = i.id;
				temp.index = index2;
				Zyan.IdIndex tempIV = new Zyan.IdIndex();
				tempIV.Id = i.id;
				tempIV.index = 0;
				ArrayUtility.Add<Zyan.IdIndex>(ref SceneVariables_Battle.playerData.InventaryActionCards, tempIV);
				ArrayUtility.Add<Zyan.IdIndex>( ref action , temp);
				index2++;
			}
			JsonReader.SaveToJSONIdIndex(action ,SceneVariables_Battle.filenameIdAction);
			index2 = 0;
			JsonReader.CreateAsset("Card");
			JsonReader.CreateAsset("Equip");
			JsonReader.CreateAsset("Spell");
			JsonReader.CreateAsset("Action");
			PlayerInventaryOBJ playerObj = ScriptableObject.CreateInstance<PlayerInventaryOBJ>();
			playerObj.Player = SceneVariables_Battle.playerData;
			AssetDatabase.CreateAsset( playerObj , "Assets/Resources/Scripts/Player/player.asset");
			steps++;
			break;
		case 1:
			if (SceneVariables_Battle.playerData.HistoryMode.Length == 0)
			{
				loadingTela.gameObject.SetActive(false);
				steps++;
				break;
			}else
			{
					if (SceneVariables_Battle.playerData.HistoryMode[0] == 1)
					{
						//dialogo.text = fala[parte].A10;
						parte = 0;
						steps = 13;
						break;
					}else 
					{
						LoadGame();
						break;
					}
			}
				
		case 2:
			var z = GameObject.Instantiate(mensageObj);
			var x = z.GetComponent<ManagerMensagens>();
			x.AutoMensagem( parte, "Baby Red Dragon", "19327076");
			steps++;
			break;
		case 3:
			steps++;
			break;
		case 4:
			steps++;
			break;
		case 5:
			steps++;
			break;
		case 6:
			steps++;
			break;
		case 7:
			steps++;
			break;
		case 8:
			steps++;
			break;
		case 9:
			steps++;
			break;
		case 10:
			steps++;
			break;
		case 11:
			steps++;
			break;
		case 12:
			steps++;
			nameT.gameObject.SetActive(true);
			break;
		case 13:
			nameT.gameObject.SetActive(true);
			if (nameT.text == "")
			{
				
				var d = FindObjectOfType<ManagerMensagens>();
				if (d == null)
				{
				var z2 = GameObject.Instantiate(mensageObj);
				var x2 = z2.GetComponent<ManagerMensagens>();
				x2.ShowMensagem ("Vamos lá digite seu nome para que eu possa ler","19327076","Baby Red Dragon");
				break;	
				}
				
			}else 
			{
				var s2 = FindObjectOfType<ManagerMensagens>();
				Object.Destroy(s2.gameObject);
				nameT.gameObject.SetActive(false);
				parte++;
				SceneVariables_Battle.playerData.Name = nameT.text;
				JsonReader.SaveToJSONPlayer(SceneVariables_Battle.playerData, "Player.mk");
				var z3 = GameObject.Instantiate(mensageObj);
				var x3 = z3.GetComponent<ManagerMensagens>();
				x3.AutoMensagem( parte, "Baby Red Dragon", "19327076");
				steps++;
				retornou = true;
			}
			break;
		case 14:
			steps++;
			break;
		case 15:
			steps++;
			break;
		case 16:
			steps++;
			break;
		case 17:
			steps++;
			break;
		case 18:
			steps++;
			break;
		case 19:
			steps++;
			break;
		case 20:
			steps++;
			break;
		case 21:
			steps++;
			break;
		case 22:
			steps++;
			break;
		case 23:
			steps++;
			addToPlayer(starterDeckCards , "Card");
			JsonReader.SaveToJSONPlayer(SceneVariables_Battle.playerData, "Player.mk");
			retornou = true;
			break;
		}
		
	}
	
	public void Toque(InputAction.CallbackContext context)
	{
		if (context.performed)
		{
			retornou = true;
		}
	}
	
	private void LoadGame()
	{
		SceneManager.LoadScene("Store");
	}
	void Awake()
	{
		//controle.Player.Accept.performed += Toque;
	}
	private void OnEnable()
	{
		//controle.Enable();
	}
	
	private void OnDisable()
	{
		//controle.Disable();
	}
	
	[SerializeField] private Zyan.IdIndex[] inventary ;
	public void addToPlayer( string[] deck , string cardType)
	{
		//ArrayUtility.Clear<Zyan.IdIndex>(ref inventary);
		switch (cardType)
		{
		case "Card":
			inventary = SceneVariables_Battle.playerData.InventaryCards;
			break;
		case "Equip":
			inventary = SceneVariables_Battle.playerData.InventaryEquip;
			break;
		case "Action":
			inventary = SceneVariables_Battle.playerData.InventaryActionCards;
			break;
		case "Spell":
			inventary = SceneVariables_Battle.playerData.InventarySpell;
			break;
		}
		foreach (var item in deck)
		{	
			var zerin = 0;
			var zI = JsonReader.ReadListFromJSONId(SceneVariables_Battle.filenameIdCards);
			foreach (var f in zI)
			{
				if (f.Id == item)
				{	
					inventary[zerin].index ++;
					Debug.Log(inventary[zerin].Id + " : " + inventary[zerin].index );
				}else{zerin++;}
			}
		}
	}
	
	
	[System.Serializable]
	public class IdIndex
	{
		public string Id;
		public int index;
	}
}
