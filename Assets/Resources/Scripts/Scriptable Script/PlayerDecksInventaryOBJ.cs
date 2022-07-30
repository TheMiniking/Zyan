using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;
using UnityEditor;

[CreateAssetMenu (fileName = "Deck", menuName = "Zyan Assets/Create Deck Inventary")]
public class PlayerDecksInventaryOBJ : ScriptableObject
{
	public PlayerDeckOBJ[] _PlayerObj;
	public Zyan.PlayerDecks[] _Decks ;
	
	void OnValidate() => LoadAll();
	
	[Button]
	public void LoadAll()
	{
		if (_PlayerObj != null)
		{ArrayUtility.Clear<PlayerDeckOBJ>(ref _PlayerObj);}
		_PlayerObj = Resources.LoadAll<PlayerDeckOBJ>("Player/Deck");
		_Decks = new Zyan.PlayerDecks[0];
		foreach (PlayerDeckOBJ a in _PlayerObj)
		{
			ArrayUtility.Add<Zyan.PlayerDecks>(ref _Decks , a.Deck);
		}
	}
}
