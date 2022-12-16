using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEditor;
using Sirenix.OdinInspector;

public class TelaInicial : MonoBehaviour
{
	public GameObject loadingTela;
	public TMP_Text loading;
	public Image grande;
	public TMP_Text dialogo;
	public TMP_InputField nameT;
	public ZyanControl controle;
	public GameObject mensageObj;
	public List<IdIndex> idJoson = new List<IdIndex>{};
	public List<PlayerDeckOBJ> starterDeck = new List<PlayerDeckOBJ>{};
	public PlayerInventaryOBJ player ;
	public GameObject escolhaDeck;
	
	// Carrega o Save inicial, se nao existe cria um novo
	[Button]
	void Start(){player.LoadPlayerDATA();}
	/// --------------------------------------------------///
	
	/// --------------------------------------------------///
	public void StartButton(){
		if (player.Player.HistoryMode[0] == "InicioCompleto"){LoadGame();}
		else{QuickStart();}
	}
	/// --------------------------------------------------///
	
	// Automatizaçao das mensagens ativando a coroutina[NextStep]
	private bool tempinho = false;
	public bool retornou = false;
	//void FixedUpdate() {if (tempinho != true){StartCoroutine(NextStep());}}
	/// --------------------------------------------------///
	
	//Coroutina das mensagens
	/*
	public IEnumerator NextStep(){	
		var x = FindObjectOfType<ManagerMensagens>();
		tempinho = true;
		if (x != null){
			yield return new WaitForSecondsRealtime(1f);
			yield return new WaitUntil (() => x.proxima == true );
			NewGame();
			tempinho= false;}
		else{
			yield return new WaitForSecondsRealtime(1f);
			NewGame();
	tempinho= false;}}
	*/
	/// --------------------------------------------------///
			
	/// --------------------------------------------------///
	/// Novo Jogo Rapido
	public void QuickStart(){
		if (player.Player.HistoryMode.Count == 0){
			player.Player.HistoryMode.Add("InicioP1");
			player.SavePlayerData();
			nameT.gameObject.SetActive(true);
		}
		else if (player.Player.HistoryMode[0] == "InicioP1"){
			if (nameT.text == ""){
				var d = FindObjectOfType<ManagerMensagens>();
				if (d == null){
					var z3 = GameObject.Instantiate(mensageObj);
					var x3 = z3.GetComponent<ManagerMensagens>();
					x3.ShowMensagem ("Vamos lá digite seu nome para que eu possa ler","19327076","Baby Red Dragon");}}
			else {
				var s2 = FindObjectOfType<ManagerMensagens>();
				if (s2 != null) Object.Destroy(s2.gameObject);
				nameT.gameObject.SetActive(false);
				player.Player.Name = nameT.text;
				player.Player.HistoryMode[0] = "InicioCompleto";
				escolhaDeck.SetActive(true);}
		}else {LoadGame();}
	}
	
	
	public void ChoiseDragon(){
		player.addToPlayer(starterDeck[0]);
		player.Player.Decks.Add(starterDeck[0]);
		player.SavePlayerData();
		escolhaDeck.SetActive(false);
		LoadGame();}
		
	public void ChoiseWarrior(){
		player.addToPlayer(starterDeck[1]);
		player.Player.Decks.Add(starterDeck[1]);
		player.SavePlayerData();
		escolhaDeck.SetActive(false);
		LoadGame();}
	

	/// --------------------------------------------------///
			
	// Novo jogo----------------------------------///
	/*
	[SerializeField]private int steps;
	private int linha;
	private int parte;
	public void NewGame(){
		switch (steps){
		case 0 :
			loading.text = "Carregando ...";
			if (player.Player.HistoryMode.Count == 0){
				player.Player.HistoryMode.Add("InicioP1");
				player.SavePlayerData();}
			Debug.Log("parte :"+ steps);
			steps++;
			break;
		case 1:
			Debug.Log("parte :"+ steps);
			loadingTela.gameObject.SetActive(false);
			if (player.Player.HistoryMode[0] == "InicioP1"){	
				steps++;
				break;}
			else{if (player.Player.HistoryMode[0] == "InicioP2"){	
				parte = 0;
				steps = 13;
				break;}
			else { 
				LoadGame();
				break;}}
		case 2:
			var z = GameObject.Instantiate(mensageObj);
			var x = z.GetComponent<ManagerMensagens>();
			x.AutoMensagem(parte, "Baby Red Dragon", "19327076");
			Debug.Log("parte :"+ steps);
			steps++;
			break;
		case 3:
			Debug.Log("parte :"+ steps);
			steps++;
			break;
		case 4:
			Debug.Log("parte :"+ steps);
			steps++;
			break;
		case 5:
			Debug.Log("parte :"+ steps);
			steps++;
			break;
		case 6:
			Debug.Log("parte :"+ steps);
			steps++;
			break;
		case 7:
			Debug.Log("parte :"+ steps);
			steps++;
			break;
		case 8:
			Debug.Log("parte :"+ steps);
			steps++;
			break;
		case 9:
			Debug.Log("parte :"+ steps);
			steps++;
			break;
		case 10:
			Debug.Log("parte :"+ steps);
			steps++;
			break;
		case 11:
			Debug.Log("parte :"+ steps);
			steps++;
			break;
		case 12:
			Debug.Log("parte :"+ steps);
			steps++;
			nameT.gameObject.SetActive(true);
			player.Player.HistoryMode[0] = "InicioP2";
			break;
		case 13:
			Debug.Log("parte :"+ steps);
			nameT.gameObject.SetActive(true);
			if (nameT.text == ""){
				var d = FindObjectOfType<ManagerMensagens>();
				if (d == null){
					var z2 = GameObject.Instantiate(mensageObj);
					var x2 = z2.GetComponent<ManagerMensagens>();
					x2.ShowMensagem ("Vamos lá digite seu nome para que eu possa ler","19327076","Baby Red Dragon");
				break;}}
			else {
				var s2 = FindObjectOfType<ManagerMensagens>();
				Object.Destroy(s2.gameObject);
				nameT.gameObject.SetActive(false);
				parte++;
				player.Player.Name = nameT.text;
				JsonReader.SaveToJSONPlayer(SceneVariables_Battle.playerData, "Player.mk");
				var z3 = GameObject.Instantiate(mensageObj);
				var x3 = z3.GetComponent<ManagerMensagens>();
				x3.AutoMensagem( parte, "Baby Red Dragon", "19327076");
				steps++;
				retornou = true;
				player.SavePlayerData();}
			break;
		case 14:
			Debug.Log("parte :"+ steps);
			steps++;
			break;
		case 15:
			Debug.Log("parte :"+ steps);
			steps++;
			break;
		case 16:
			Debug.Log("parte :"+ steps);
			steps++;
			break;
		case 17:
			Debug.Log("parte :"+ steps);
			steps++;
			break;
		case 18:
			Debug.Log("parte :"+ steps);
			steps++;
			break;
		case 19:
			Debug.Log("parte :"+ steps);
			steps++;
			break;
		case 20:
			Debug.Log("parte :"+ steps);
			steps++;
			break;
		case 21:
			Debug.Log("parte :"+ steps);
			steps++;
			break;
		case 22:
			Debug.Log("parte :"+ steps);
			steps++;
			break;
		case 23:
			Debug.Log("parte :"+ steps);
			steps++;
			player.Player.HistoryMode[0] = "InicioCompleto";
			player.addToPlayer(starterDeck);
			player.SavePlayerData();
			retornou = true;
			LoadGame();
			break;}}
	*/
	/// --------------------------------------------------///
	public void VirtualToque() {
		tempinho = false;
		Debug.Log("Toque - feito");}
	// Toque na tela	----------------------------------///
	public void Toque(InputAction.CallbackContext context){
		Debug.Log("Toque - feito");
		if (context.performed){retornou = true;}}
		
	/*
	private void OnEnable(){ 
		controle.Enable();
		controle.Player.AnyClick.performed += Toque;
	}
	private void OnDisable() {
		controle.Player.AnyClick.performed -= Toque;
		controle.Disable();
	}*/
	
	/// ----------------------------------------------------///
	
	//Carrega a cena principal
	private void LoadGame()=> SceneManager.LoadScene(1);
	/// ----------------------------------------------------///
	
	[System.Serializable]
	public class IdIndex
	{
		public string Id;
		public int index;
	}
}
