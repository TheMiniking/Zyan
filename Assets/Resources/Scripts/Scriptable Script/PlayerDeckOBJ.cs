using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;

[CreateAssetMenu (fileName = "Deck", menuName = "Zyan Assets/Create Deck Object")]
public class PlayerDeckOBJ : ScriptableObject
{
	public Zyan.PlayerDecks Deck;
	public Zyan.SpellClass[] SpellTrap; 
	
	void Start () => UpdateData();
	
	[Button]
	public void UpdateData() => OnValidate();
	
	
	void OnValidate()
	{
		Deck.timeName = this.name; 
		Deck.OBJ = this;
		foreach (Zyan.SpellClass sp in SpellTrap)
		{
			if (sp.OBJ != null)
			{
				sp.id = sp.OBJ.id;
				sp.Name = sp.OBJ.Name;
				sp.Type = sp.OBJ.Type;
				sp.SubType = sp.OBJ.SubType;
				sp.Restriction = sp.OBJ.Restriction;
				sp.Description = sp.OBJ.Description;
				sp.Script = sp.OBJ.Script;
				sp.Rarity = sp.OBJ.Rarity;
			}
		}
	}
}
