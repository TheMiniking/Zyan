using System.Xml.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;
using UnityEditor;

[CreateAssetMenu (fileName = "Correio OBJ", menuName = "Zyan Assets/Create CorreioOBJ")]
public class CorreioOBJ : ScriptableObject
{
	public List<Correio> inbox;
	public PlayerInventaryOBJ player;
    
	[Button]
	public void ReceberGift(int gift){
		if (inbox[gift].Recebido){
			Debug.Log("Item ja recebido, excluindo mensagem! ");
			ExcluirMensagem(gift);
		}else{
		int index = 0;
		if (inbox[gift].Gift.Count==1){
			if (inbox[gift].Gift[0]!= PlayerInventaryOBJ.Gift.Unit && inbox[gift].Gift[0]!= PlayerInventaryOBJ.Gift.Deck){
				player.addToPlayer(inbox[gift].Gift[0] , inbox[gift].Quantidade[0]);}
			else if(inbox[gift].Gift[0]== PlayerInventaryOBJ.Gift.Unit){
				player.addToPlayer(inbox[gift].Card[0] , inbox[gift].CardType[0]);} 
			else{player.addToPlayer(inbox[gift].Card ,  inbox[gift].CardType[0]);}
			inbox[gift].Recebido = true;
		}else{
			foreach (PlayerInventaryOBJ.Gift g in inbox[gift].Gift){
				if ( g != PlayerInventaryOBJ.Gift.Unit && g != PlayerInventaryOBJ.Gift.Deck){
					player.addToPlayer(inbox[gift].Gift[index] , inbox[gift].Quantidade[index]);
					index ++;}
				else if( g == PlayerInventaryOBJ.Gift.Unit){
					player.addToPlayer(inbox[gift].Card[index] , inbox[gift].CardType[index]);
					index++;} 
				else{player.addToPlayer(inbox[gift].Card ,  inbox[gift].CardType[index]); index++;}}
			inbox[gift].Recebido = true;}
		}
	}

	[Button]
	public void ExcluirMensagem(int gift) => inbox.RemoveAt(gift);
	
	//
	//
    [Serializable]
    public class Correio{
        public string Titulo;
        public string Mensage;
	    public List<PlayerInventaryOBJ.Gift> Gift; // So adicionar 1 Gift "Card" ou "Deck", os outros podem ter mais de um.
	    public List<String> Card;					// Um : usa Gift "Card", Mais que um : usa Gift "Deck"
	    public List<string> CardType;				// Somente Gift "Card" ou "Deck"
	    public List<int> Quantidade;
        public bool Recebido;

    }
}