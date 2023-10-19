using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class MenuInicial : MonoBehaviour
{
	public TMP_Text pName;
	public TMP_Text pPosition;
	public TMP_Text pGem;
	public TMP_Text pCoin;
	public TMP_Text pName2;
	public TMP_Text pPosition2;
	public TMP_Text pCollection;
	public TMP_Text pMatUltraComum;
	public TMP_Text pMatRare;
	public TMP_Text pMatEpic;
	public TMP_Text pMatLegendary;
	public PlayerInventaryOBJ player;
	public int atualCardsNumb;
	public GameObject[] telas;
	public Vector3[] origPosTela;
	public Vector3 centro;
	public GameObject intro;
	// Update is called once per frame
	void Start(){
		intro.GetComponent<Animator>().SetBool("Entra",true);
		player.LoadPlayerDATA();
		MoveTela(0,true);
		MoveTela(1,false);
		MoveTela(2,false);
		MoveTela(3,false);
		MoveTela(4,false);
	}
    
    void Update()
	{
		pName.text = player.Player.Name;
		pPosition.text = "Pos: " + "10.999";
		pName2.text = player.Player.Name;
		pPosition2.text = "Pos: " + "10.999";
		pGem.text = player.Player.iTesouros.gem.ToString();
		pCoin.text = player.Player.iTesouros.coin.ToString();
		pMatUltraComum.text = player.Player.iTesouros.materialUC.ToString();
		pMatRare.text = player.Player.iTesouros.materialR.ToString();
		pMatEpic.text = player.Player.iTesouros.materialE.ToString();
		pMatLegendary.text = player.Player.iTesouros.materialL.ToString();
		pCollection.text = atualCardsNumb + " / " + player.Player.InventaryCards.Count.ToString() ;
		UpdateAtualCardsNumb();
        
	}
	
	public void IntroOut()=>intro.GetComponent<Animator>().SetBool("Entra",false);
	
	[Button]
	public void UpdateAtualCardsNumb(){
		atualCardsNumb = 0;
		foreach(KeyValuePair<string , Zyan.IdIndex>  i in player.PlayerInventaryUnit){
			if (i.Value.Quantidade > 0){atualCardsNumb++;}
		}
	}
	
	
	[Button]
	public void MoveTela(int tela, bool entrando){
		if (entrando){ _ = LeanTween.move(telas[tela], centro, 0.75f).setEaseOutElastic(); }
		else { _ = LeanTween.move(telas[tela], origPosTela[tela], 0.4f); }
	}
	[Button]
	public void MoveTela(int tela){
		if (tela < telas.Length && tela > -1){
		switch(tela){
		case 0:
			MoveTela(0,true);
			MoveTela(1,false);
			MoveTela(2,false);
			MoveTela(3,false);
			MoveTela(4,false);
			break;
		case 1:
			MoveTela(0,false);
			MoveTela(1,true);
			MoveTela(2,false);
			MoveTela(3,false);
			MoveTela(4,false);
			break;
		case 2:
			MoveTela(0,false);
			MoveTela(1,false);
			MoveTela(2,true);
			MoveTela(3,false);
			MoveTela(4,false);
			break;
		case 3:
			MoveTela(0,false);
			MoveTela(1,false);
			MoveTela(2,false);
			MoveTela(3,true);
			MoveTela(4,false);
			break;
		case 4:
			MoveTela(0,false);
			MoveTela(1,false);
			MoveTela(2,false);
			MoveTela(3,false);
			MoveTela(4,true);
			break;
		}}
		else {
			Debug.LogWarning("Numero acima! Retornando para tela inicial!");
			MoveTela(0,true);
			MoveTela(1,false);
			MoveTela(2,false);
			MoveTela(3,false);
			MoveTela(4,false);
		}
	}
}
