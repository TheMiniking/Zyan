using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEditor;
using Sirenix.OdinInspector;
using System.Threading.Tasks;

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
	public GameObject intro;
	public GameObject inicialTela;
	
	// Carrega o Save inicial, se nao existe cria um novo
	[Button]
	void Start(){player.LoadPlayerDATA();}//StartCoroutine(LoadGameSave());}
	/// --------------------------------------------------///
	
	/// --------------------------------------------------///
	
	public IEnumerator LoadGameSave(){
		player.LoadPlayerDATA();
		yield return new WaitForSecondsRealtime(2f);
		if (player.Player.HistoryMode.Count>0){if (player.Player.HistoryMode[0] == "InicioCompleto") _ = LoadGame(); }
		else{QuickStart();}
	}
	
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
		}else {StartCoroutine(LoadGameV2());}
	}
	
	
	public void ChoiseDragon(){
		player.addToPlayer(starterDeck[0]);
		player.Player.Decks.Add(starterDeck[0].Deck);
		player.AtualDeck=starterDeck[0].Deck;
		player.DicUpdate();
		player.SavePlayerData();
		escolhaDeck.SetActive(false);
		StartCoroutine(LoadGameV2());}
		
	public void ChoiseWarrior(){
		player.addToPlayer(starterDeck[1]);
		player.Player.Decks.Add(starterDeck[1].Deck);
		player.AtualDeck=starterDeck[1].Deck;
		player.DicUpdate();
		player.SavePlayerData();
		escolhaDeck.SetActive(false);
		StartCoroutine(LoadGameV2());}
	

	/// --------------------------------------------------///
			
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
	private async Task LoadGame(){
		AsyncOperation load = SceneManager.LoadSceneAsync(1);
		while (!load.isDone){
			float progresso = Mathf.Clamp01(load.progress / 0.9f);
			loading.text = "Progresso de carregamento: " + (progresso * 100) + "%";
			await Task.Yield(); // Garante que o loop seja assíncrono
		}
		intro.SetActive(true);
		intro.GetComponent<Animator>().SetBool("Entra",false);
	}

	public IEnumerator LoadGameV2(){
		intro.SetActive(true);
		intro.GetComponent<Animator>().SetBool("Entra",false);
		yield return new WaitForSeconds(1f);
		inicialTela.SetActive(false);
	}
	/// ----------------------------------------------------///
	
	[System.Serializable]
	public class IdIndex
	{
		public string Id;
		public int index;
	}
}
