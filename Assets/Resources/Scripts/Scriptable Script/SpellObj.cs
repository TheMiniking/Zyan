using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "Spell", menuName = "Zyan Assets/Create SpellCard")]
public class SpellObj : ScriptableObject
{
	public string id;
	public string Name;
	public string Type;
	public string SubType;
	public string Restriction;
	public string Description;
	public string Script;
	public string Rarity;
	public Zyan.SpellClass Card;
	public Zyan.IdIndex Data;
	
	void OnValidate() 
	{
		Card = new Zyan.SpellClass();
		id = this.name;
		LoadSelf();
	}
	
	public void LoadSelf()
	{
		Zyan.SpellCardList[] cards = JsonReader.ReadListFromJSONSpellTrap();
		Zyan.IdIndex[] dat = JsonReader.ReadListFromJSONId("Spell.mk");
		if (id != "")
		{
			foreach (Zyan.SpellCardList item in cards)
			{
				if (item.id == id)
				{
					Name = item.Name;
					Type = item.Type;
					SubType = item.SubType;
					Restriction = item.Restriction;
					Description = item.Description;
					Rarity = item.Rarity;
					Script = item.Script;
					Card.id = item.id;
					Card.Name = item.Name;
					Card.Type = item.Type;
					Card.SubType = item.SubType;
					Card.Restriction = item.Restriction;
					Card.Description = item.Description;
					Card.Rarity = item.Rarity;
					Card.Script = item.Script;
					Card.OBJ = this;
				}
			}
			foreach (Zyan.IdIndex ind in dat)
			{
				if (ind.Id == id)
				{
					Data = ind;
				}
			}
		}
	}
}
