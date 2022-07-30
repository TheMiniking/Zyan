using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu (fileName = "Unit", menuName = "Zyan Assets/Create ActionCard")]
public class ActionObj : ScriptableObject
{
	public string id;
	public string Name;
	public int Cust;
	public string Activation;
	public string Time;
	public string Description;
	public string Rarity;
	public string Script;
	public Zyan.ActionClass Card;
	public Zyan.IdIndex Data;
	
	void OnValidate() 
	{
		Card = new Zyan.ActionClass();
		id = this.name;
		LoadSelf();
	}
	
	public void LoadSelf()
	{
		Zyan.ActionCardList[] cards = JsonReader.ReadListFromJSONAction();
		Zyan.IdIndex[] dat = JsonReader.ReadListFromJSONId("Action.mk");
		if (id != "")
		{
			foreach (Zyan.ActionCardList item in cards)
			{
				if (item.id == id)
				{
					Name = item.Name;
					Cust = item.Cust;
					Activation = item.Activation;
					Time = item.Time;
					Description = item.Description;
					Rarity = item.Rarity;
					Script = item.Script;
					Card.id = item.id;
					Card.Name = item.Name;
					Card.Cust = item.Cust;
					Card.Activation = item.Activation;
					Card.Time = item.Time;
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
