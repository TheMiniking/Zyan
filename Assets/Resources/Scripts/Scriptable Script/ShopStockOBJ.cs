using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;


[CreateAssetMenu (fileName = "StoreStock", menuName = "Zyan Assets/Create Store Stock")]
public class ShopStockOBJ : ScriptableObject
{
	[ShowInInspector]public Dictionary<string,Zyan.UnitClass> CardPoolDicionary = new Dictionary<string, Zyan.UnitClass>{};
	[ShowInInspector]public Dictionary<string,Zyan.BoosterItem> CardPool = new Dictionary<string, Zyan.BoosterItem>{};// Lista com todos cards disponiveis para ganhar.
	public List<string> CardPoolComum;		// Listas separado por raridade.
	public List<string> CardPoolIncomum;
	public List<string> CardPoolRare;
	public List<string> CardPoolEpic;
	public List<string> CardPoolLendary;
	public PlayerInventaryOBJ invent;
	public GameObject mensagePrefab;
	
	public void Awake(){AtualizarDividir("Dragons Alliance");}
	
	/// ---- Atualiza Lista do booster com as unidadades que podem ser adiquiridas, de acordo com nome do "set"
	[Button]public void AtualizarDividir(string Atual){
		var dat = JsonReader.ReadListFromJSONBooster();
		CardPool.Clear();
		foreach (Zyan.BoosterItem b in dat ){CardPool.Add(b.Nome,b);}
		CardPoolDicionary.Clear();
		CardPoolComum.Clear();
		CardPoolIncomum.Clear();
		CardPoolRare.Clear();
		CardPoolEpic.Clear();
		CardPoolLendary.Clear();
		foreach (string c in CardPool[Atual].List ){
			var card = Resources.Load<UnitObj>("Scripts/Unit/"+c);
			CardPoolDicionary.Add(c,card.Card);
			switch (card.Rarity){
			case "Comum":
				CardPoolComum.Add(c);
				break;
			case "UltraComum":
				CardPoolIncomum.Add(c);
				break;
			case "Rare":
				CardPoolRare.Add(c);
				break;
			case "Epic":
				CardPoolEpic.Add(c);
				break;
			case "Legendary":
				CardPoolLendary.Add(c);
				break;
			}
		}
	}
	
	///-------- 	Shop	 -----------------------------------///
	
	///----- 	Receber uma Unidade do set atual	 ------///
	[Button]
	public string GetAUnit(){
		string unit;
		var lucky = Random.Range(0,1001);
		if (lucky <= 5 && CardPoolLendary.Count!=0){unit = CardPoolLendary[Random.Range(0,CardPoolLendary.Count)];}
		else if (lucky <= 70 && CardPoolEpic.Count!=0){unit = CardPoolEpic[Random.Range(0,CardPoolEpic.Count)];}
		else if (lucky <= 200 && CardPoolRare.Count!=0){unit = CardPoolRare[Random.Range(0,CardPoolRare.Count)];}
		else if (lucky <= 650 && CardPoolIncomum.Count!=0){unit = CardPoolIncomum[Random.Range(0,CardPoolIncomum.Count)];}
		else {unit = CardPoolComum[Random.Range(0,CardPoolComum.Count)];}
		Debug.Log("You win a : " + CardPoolDicionary[unit].Name + " a card " +CardPoolDicionary[unit].Rarity );
		return unit;
	}
	
	///----- 	Recebe uma unidade do set atual , Rara ou maior	 ------///
	[Button]
	public string GetAUnit(bool rareUp){
		string unit;
		int lucky;
		if (rareUp){ lucky = Random.Range(0,201);}
		else { lucky = Random.Range(0,1001);}
		if (lucky <= 5 && CardPoolLendary.Count!=0){unit = CardPoolLendary[Random.Range(0,CardPoolLendary.Count)];}
		else if (lucky <= 70 && CardPoolEpic.Count!=0){unit = CardPoolEpic[Random.Range(0,CardPoolEpic.Count)];}
		else if (lucky <= 200 && CardPoolRare.Count!=0){unit = CardPoolRare[Random.Range(0,CardPoolRare.Count)];}
		else if (lucky <= 650 && CardPoolIncomum.Count!=0){unit = CardPoolIncomum[Random.Range(0,CardPoolIncomum.Count)];}
		else {unit = CardPoolComum[Random.Range(0,CardPoolComum.Count)];}
		Debug.Log("You win a : " + CardPoolDicionary[unit].Name + " a card " +CardPoolDicionary[unit].Rarity );
		return unit;
	}
	
	///----- 	Recebe 10 unidades do set atual, sendo 1 rara ou maior	 ------///
	[Button]
	public string[] GetAUnitPack(){
		List<string> units = new List<string>{};
		units.Add(GetAUnit());
		units.Add(GetAUnit());
		units.Add(GetAUnit());
		units.Add(GetAUnit());
		units.Add(GetAUnit());
		units.Add(GetAUnit());
		units.Add(GetAUnit());
		units.Add(GetAUnit());
		units.Add(GetAUnit());
		units.Add(GetAUnit(true));
		return units.ToArray();
	}
	
	///----- 	Adiciona unidade recebida ao inventario, caso tenha 3 unidades recebe materiais no lugar	 ------///
	[Button]
	public void GetUnitToInventary(){
		var card = GetAUnit();
		var mens = Instantiate(mensagePrefab);
		if (invent.PlayerInventaryUnit[card].Quantidade==3){
			var _unit = Resources.Load<UnitObj>("Scripts/Unit/"+card);
			switch (_unit.Rarity){
			case "Comum":invent.Player.iTesouros.materialUC +=1;mens.GetComponent<MensagemItens>().SendMensagem(1 + "x Material Ultracomum<br>Max : "+ _unit.Name,false);break;
			case "UltraComum":invent.Player.iTesouros.materialUC +=5;mens.GetComponent<MensagemItens>().SendMensagem(5 + "x Material Ultracomum<br>Max : "+ _unit.Name,false);break;
			case "Rare":invent.Player.iTesouros.materialR +=5;mens.GetComponent<MensagemItens>().SendMensagem(5 + "x Material Rare<br>Max : "+ _unit.Name,false);break;
			case "Epic":invent.Player.iTesouros.materialE +=5;mens.GetComponent<MensagemItens>().SendMensagem(5 + "x Material Epic<br>Max : "+ _unit.Name,false);break;
			case "Legendary":invent.Player.iTesouros.materialL +=5;mens.GetComponent<MensagemItens>().SendMensagem(5 + "x Material Legendary<br>Max : "+ _unit.Name,false);break;
			}
		}else {
			invent.addToPlayer(card, "Card");
			mens.GetComponent<MensagemItens>().SendMensagem("1x "+Resources.Load<UnitObj>("Scripts/Unit/"+card).Name, false);
		}
		invent.SavePlayerData();
		
	}
	///----- 	Adiciona um apack de 10 unidades ao inventario, caso algum deles tenha 3 unidades recebe material no lugar	 ------///
	[Button]
	public void GetUnitPackToInventary(){
		var pack = GetAUnitPack();
		List<string> packL = new List<string>{};
		string packResult = "" ;
		foreach(string c in pack){packL.Add(c);}
		foreach(string card in packL){
			if (invent.PlayerInventaryUnit[card].Quantidade==3){
			var _unit = Resources.Load<UnitObj>("Scripts/Unit/"+card);
			switch (_unit.Rarity){
			case "Comum":invent.Player.iTesouros.materialUC +=1;packResult = packResult+ "1x Material Ultracomum      Max : ["+ _unit.Name+"]<br>"; break;
			case "UltraComum":invent.Player.iTesouros.materialUC +=5;packResult = packResult+ "5x Material Ultracomum      Max : ["+ _unit.Name+"]<br>";break;
			case "Rare":invent.Player.iTesouros.materialR +=5;packResult = packResult+ "5x Material Rare      Max : ["+ _unit.Name+"]<br>";break;
			case "Epic":invent.Player.iTesouros.materialE +=5;packResult = packResult+ "5x Material Epic      Max : ["+ _unit.Name+"]<br>";break;
			case "Legendary":invent.Player.iTesouros.materialL +=5;packResult = packResult+ "5x Material Legendary      Max : ["+ _unit.Name+"]<br>";break;
			}
			}else {
				invent.addToPlayer(card, "Card");
				packResult = packResult+ "1x "+ Resources.Load<UnitObj>("Scripts/Unit/"+card).Name+"<br>";
			}
		}
		var mens = Instantiate(mensagePrefab);
		mens.GetComponent<MensagemItens>().SendMensagem(packResult, true);
		invent.SavePlayerData();
	}
	///----- 	Compra uma unidade , se houver moeda/gema suficiente	 ------///
	[Button]
	public void TryGetACard(bool gem){
		if (gem){
			if (invent.Player.iTesouros.gem >= 50 ){
				invent.Player.iTesouros.gem -= 50;
				GetUnitToInventary();}
			else {Debug.Log("Quantidade de Gemas insuficiente");}
		}else{
			if (invent.Player.iTesouros.coin >= 100 ){
				invent.Player.iTesouros.coin -= 100;
				GetUnitToInventary();}
			else {Debug.Log("Quantidade de Moedas insuficiente");}
		}
	}
	
	///----- 	Compra um pack, se houver moeda/gema suficiente	 ------///
	[Button]
	public void TryGetACardPack(bool gem){
		if (gem){
			if (invent.Player.iTesouros.gem >= 450 ){
				invent.Player.iTesouros.gem = invent.Player.iTesouros.gem - 450;
				GetUnitPackToInventary();}
			else {Debug.Log("Quantidade de Gemas insuficiente");}
		}else{
			if (invent.Player.iTesouros.coin >= 1000 ){
				invent.Player.iTesouros.coin = invent.Player.iTesouros.coin - 1000;
				GetUnitPackToInventary();}
			else {Debug.Log("Quantidade de Moedas insuficiente");}
		}
	}
	
	///----- 	Presentes	 ------///
	
	/// Adiciona item ao iventario , exceto cards
	[Button]public void GetAItemToInventary(string item, int quantidade){
		var mens = Instantiate(mensagePrefab);
		switch(item){
		case "Coin":invent.Player.iTesouros.coin += quantidade;mens.GetComponent<MensagemItens>().SendMensagem(quantidade + "x Coins",false); break;
		case "Gem":invent.Player.iTesouros.gem += quantidade;mens.GetComponent<MensagemItens>().SendMensagem(quantidade + "x Gems",false);  break;
		case "MaterialC":invent.Player.iTesouros.materialC += quantidade;mens.GetComponent<MensagemItens>().SendMensagem(quantidade + "x Material Comum",false);  break;
		case "MaterialUC":invent.Player.iTesouros.materialUC += quantidade;mens.GetComponent<MensagemItens>().SendMensagem(quantidade + "x Material UltraComum",false);  break;
		case "MaterialR":invent.Player.iTesouros.materialR += quantidade;mens.GetComponent<MensagemItens>().SendMensagem(quantidade + "x Material Rare",false);  break;
		case "MaterialE":invent.Player.iTesouros.materialE += quantidade;mens.GetComponent<MensagemItens>().SendMensagem(quantidade + "x Material Epic",false);  break;
		case "MaterialL":invent.Player.iTesouros.materialL += quantidade;mens.GetComponent<MensagemItens>().SendMensagem(quantidade + "x Material Legendary",false);  break;
		case "CupomC":invent.Player.iTesouros.upgradeCuponsC += quantidade;mens.GetComponent<MensagemItens>().SendMensagem(quantidade + "x Ticket Comum",false);  break;
		case "CupomUC":invent.Player.iTesouros.upgradeCuponsUC += quantidade;mens.GetComponent<MensagemItens>().SendMensagem(quantidade + "x Ticket UltraComum",false);  break;
		case "CupomR":invent.Player.iTesouros.upgradeCuponsR += quantidade;mens.GetComponent<MensagemItens>().SendMensagem(quantidade + "x Ticket Rare",false);  break;
		case "CupomE":invent.Player.iTesouros.upgradeCuponsE += quantidade;mens.GetComponent<MensagemItens>().SendMensagem(quantidade + "x Ticket Epic",false);  break;
		case "CupomL":invent.Player.iTesouros.upgradeCuponsL += quantidade;mens.GetComponent<MensagemItens>().SendMensagem(quantidade + "x Ticket Legendary",false);  break;
		}
	}
	
	/// Adciona cards ao inventario
	/// Card,Equip,Action,Spell
	
	[Button]public void GetAUnitToInventary(string unitID, string type){
		if (invent.PlayerInventaryUnit[unitID].Quantidade==3){
			var _unit = Resources.Load<UnitObj>("Scripts/Unit/"+unitID);
			switch (_unit.Rarity){
			case "Comum":invent.Player.iTesouros.materialUC +=1;break;
			case "UltraComum":invent.Player.iTesouros.materialUC +=5;break;
			case "Rare":invent.Player.iTesouros.materialR +=5;break;
			case "Epic":invent.Player.iTesouros.materialE +=5;break;
			case "Legendary":invent.Player.iTesouros.materialL +=5;break;
			}
		}else {invent.addToPlayer(unitID, type);}
		var mens = Instantiate(mensagePrefab);
		mens.GetComponent<MensagemItens>().SendMensagem("1x "+ Resources.Load<UnitObj>("Scripts/Unit/"+unitID).Name,false);
		invent.SavePlayerData();
	}
}
