using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


[CreateAssetMenu (fileName = "Unit", menuName = "Zyan Assets/Create Unit")]
public class UnitObj : ScriptableObject
{
	public string id;
	public string Name;
	public string Type;
	public string Rank;
	public string Element;
	public string Special;
	public int ATK;
	public int DEF;
	public int Life;
	public string Status;
	public string Effect1;
	public string Effect2;
	public string Evolution;
	public string EvolCust1;
	public string EvolCust2;
	public string EvolCust3;
	public string Evol1;
	public string Evol2;
	public string Evol3;
	public string RarityUp;
	public string Rarity;
	public Zyan.UnitClass Card;
	public Zyan.IdIndex Data;
	
	void OnValidate() 
	{
		id = this.name;
		LoadSelf();
	}
	
	[Button("Carregar Dados")]
	public void LoadSelf()
	{
		Zyan.CardList[] cards = JsonReader.ReadListFromJSONCard();
		Zyan.IdIndex[] dat = JsonReader.ReadListFromJSONId("Card.mk");
		if (id != "")
		{
			foreach (Zyan.CardList item in cards)
			{
				if (item.id == id)
				{
					Name = item.Name;
					Type = item.Type;
					Rank = item.Rank;
					Element = item.Element;
					Special = item.Special;
					ATK = item.ATK;
					DEF = item.DEF;
					Life = item.Life;
					Status = item.Status;
					Effect1 = item.Effect1;
					Effect2 = item.Effect2;
					Evolution = item.Evolution;
					EvolCust1 = item.EvolCust1;
					EvolCust2 = item.EvolCust2;
					EvolCust3 = item.EvolCust3;
					Evol1 = item.Evol1;
					Evol2 = item.Evol2;
					Evol3 = item.Evol3;
					RarityUp = item.RarityUp;
					Rarity = item.Rarity;
					Card.id = id;
					Card.Name = item.Name;
					Card.Type = item.Type;
					Card.Rank = item.Rank;
					Card.Element = item.Element;
					Card.Special = item.Special;
					Card.ATK = item.ATK;
					Card.DEF = item.DEF;
					Card.Life = item.Life;
					Card.Status = item.Status;
					Card.Effect1 = item.Effect1;
					Card.Effect2 = item.Effect2;
					Card.Evolution = item.Evolution;
					Card.EvolCust1 = item.EvolCust1;
					Card.EvolCust2 = item.EvolCust2;
					Card.EvolCust3 = item.EvolCust3;
					Card.Evol1 = item.Evol1;
					Card.Evol2 = item.Evol2;
					Card.Evol3 = item.Evol3;
					Card.RarityUp = item.RarityUp;
					Card.Rarity = item.Rarity;
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
