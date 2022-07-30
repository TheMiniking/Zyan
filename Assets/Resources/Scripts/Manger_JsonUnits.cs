using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manger_JsonUnits : MonoBehaviour
{
	public TextAsset cardsJson;
	public TextAsset spellJson;
	public TextAsset actionJson;
	public TextAsset equipJson;
	public Zyan.CardListII myCardListII = new Zyan.CardListII();
	public Unit[] gm;
	
	private int index;
    public void LoadUnitsPlayer()
    {   gm = GameObject.FindObjectsOfType<Unit>();
		var y = 0;
	    myCardListII = JsonUtility.FromJson<Zyan.CardListII>(cardsJson.text);
        foreach (Unit unit in gm)
        {	if (unit._isPlayer){
	        	unit._Index = index;
	       		unit._OwnerID = SceneVariables_Battle.playerID;
	        	unit.LoadCardDATA();
            }
            else
        {
            	switch (y)
            	{
            	case 0:
	            	index = int.Parse(SceneVariables_Battle._PlayerDeck.Slot1.Civil);
	            	break;
            	case 1:
	            	index = int.Parse(SceneVariables_Battle._PlayerDeck.Slot2.Civil);
	            	break;
            	case 2:
	            	index = int.Parse(SceneVariables_Battle._PlayerDeck.Slot3.Civil);
	            	break;
            	case 3:
	            	index = int.Parse(SceneVariables_Battle._PlayerDeck.Slot4.Civil);
	            	break;
            	case 4:
	            	index = int.Parse(SceneVariables_Battle._PlayerDeck.Slot5.Civil);
	            	break;
            	}
				unit._id = myCardListII.Cards[index].id;
				unit._name = myCardListII.Cards[index].Name;
				unit._type = myCardListII.Cards[index].Type;
				unit._rank = myCardListII.Cards[index].Rank;
				unit._element = myCardListII.Cards[index].Element;
				unit._special = myCardListII.Cards[index].Special;
				unit._life = myCardListII.Cards[index].Life;
				unit._atk = myCardListII.Cards[index].ATK;
				unit._def = myCardListII.Cards[index].DEF;
				unit._status = myCardListII.Cards[index].Status;
				unit._effect1 = myCardListII.Cards[index].Effect1;
				unit._effect2 = myCardListII.Cards[index].Effect2;
				unit._evolution = myCardListII.Cards[index].Evolution;
				unit._evolCust1 = myCardListII.Cards[index].EvolCust1;
				unit._evolCust2 = myCardListII.Cards[index].EvolCust2;
				unit._evolCust3 = myCardListII.Cards[index].EvolCust3;
				unit._evol1 = myCardListII.Cards[index].Evol1;
				unit._evol2 = myCardListII.Cards[index].Evol2;
				unit._evol3 = myCardListII.Cards[index].Evol3;
				unit._rarityUp = myCardListII.Cards[index].RarityUp;
				unit._rarity = myCardListII.Cards[index].Rarity;
				unit._originalAtk = myCardListII.Cards[index].ATK;
				unit._originalDef = myCardListII.Cards[index].DEF;
				unit._originalLife = myCardListII.Cards[index].Life;
	            unit._Index = index;
	            unit._OwnerID = SceneVariables_Battle.enemyID;
	            //unit.LoadCard();
				y++;
			}

		}
    }
}
