using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu (fileName = "Equip", menuName = "Zyan Assets/Create Equip")]
public class EquipObj : ScriptableObject
{
	public string id ;
	public string Name;
	public string Type;
	public string ExclusiveType;
	public string TypeBonus1;
	public string TypeBonus2;
	public string TypeBonus3;
	public string TypeBonus4;
	public string TypeBonus5;
	public string TypeExcesao1;
	public string TypeExcesao2;
	public string TypeExcesao3;
	public string Status;
	public string BoostType;
	public string DeboostBool;
	public string BoostQnt;
	public string Description;
	public string ScriptID;
	public string Rarity;
	public Zyan.EquipClass Card;
	public Zyan.IdIndex Data;
	
	
	void OnValidate() 
	{
		id = this.name;
		LoadSelf();
	}
	
	public void LoadSelf()
	{
		Zyan.EquipCardList[] cards = JsonReader.ReadListFromJSONEquip();
		List<Zyan.IdIndex> dat = JsonReader.ReadListFromJSONId("Equip.mk");
		if (id != "")
		{
			foreach (Zyan.EquipCardList item in cards)
			{
				if (item.id == id)
				{
					Name = item.Name;
					Type = item.Type;
					ExclusiveType = item.ExclusiveType;
					TypeBonus1 = item.TypeBonus1;
					TypeBonus2 = item.TypeBonus2;
					TypeBonus3 = item.TypeBonus3;
					TypeBonus4 = item.TypeBonus4;
					TypeBonus5 = item.TypeBonus5;
					TypeExcesao1 = item.TypeExcesao1;
					TypeExcesao2 = item.TypeExcesao2;
					TypeExcesao3 = item.TypeExcesao3;
					Status = item.Status;
					BoostType = item.BoostType;
					DeboostBool = item.DeboostBool;
					BoostQnt = item.BoostQnt;
					Description = item.Description;
					Rarity = item.Rarity;
					ScriptID = item.ScriptID;
					Card.id = item.id;
					Card.Name = item.Name;
					Card.Type = item.Type;
					Card.ExclusiveType = item.ExclusiveType;
					Card.TypeBonus1 = item.TypeBonus1;
					Card.TypeBonus2 = item.TypeBonus2;
					Card.TypeBonus3 = item.TypeBonus3;
					Card.TypeBonus4 = item.TypeBonus4;
					Card.TypeBonus5 = item.TypeBonus5;
					Card.TypeExcesao1 = item.TypeExcesao1;
					Card.TypeExcesao2 = item.TypeExcesao2;
					Card.TypeExcesao3 = item.TypeExcesao3;
					Card.Status = item.Status;
					Card.BoostType = item.BoostType;
					Card.DeboostBool = item.DeboostBool;
					Card.BoostQnt = item.BoostQnt;
					Card.Description = item.Description;
					Card.Rarity = item.Rarity;
					Card.ScriptID = item.ScriptID;
					Card.Obj = this;
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