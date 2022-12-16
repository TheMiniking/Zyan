using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;
using UnityEditor;

[CreateAssetMenu (fileName = "Deck", menuName = "Zyan Assets/Create Deck Inventary")]
public class PlayerDecksInventaryOBJ : ScriptableObject
{
	public List<PlayerDeckOBJ> _PlayerObj;
	public List<Zyan.PlayerDecks> _Decks ;
	
	void OnValidate() => LoadAll();
	
	[Button]
	public void LoadAll()
	{
		if (_PlayerObj != null)
		{_PlayerObj = new List<PlayerDeckOBJ>{};}
		var pg = Resources.LoadAll<PlayerDeckOBJ>("Player/Deck");
		foreach (PlayerDeckOBJ p in pg){
			_PlayerObj.Add(p);
		}
		 
		_Decks = new List<Zyan.PlayerDecks>{};
		foreach (PlayerDeckOBJ a in _PlayerObj)
		{
			_Decks.Add(a.Deck);
		}
	}
}
