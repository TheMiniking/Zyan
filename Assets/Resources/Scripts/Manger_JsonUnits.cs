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
	public SceneVariables_Battle svb;
	
	private int index;
    public void LoadUnitsPlayer()
    {   gm = GameObject.FindObjectsOfType<Unit>();
	    myCardListII = JsonUtility.FromJson<Zyan.CardListII>(cardsJson.text);
        foreach (Unit unit in gm)
        {	if (unit._isPlayer){
	        	unit._Self._Index = index;
	       		unit._OwnerID = svb.playerID;
	        	unit._Self.LoadCardDATA();
            }
            else
        {
	            unit._Self._Index = index;
	            unit._OwnerID = svb.enemyID;
	            unit._Self.LoadCardDATA();
			}

		}
    }
}
