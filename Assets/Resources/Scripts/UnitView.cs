using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitView : MonoBehaviour
{
	public Unit bigUnit;
	public Zyan.TimeUnits atualSlot;
	public Toggle[] evos;
	
	public void UpdateBigUnit(int rank){
		switch (rank)
		{
		case 1:
			bigUnit._Self._id = atualSlot.Civil;
			break;
		case 2:
			bigUnit._Self._id = atualSlot.Soldier;
			break;
		case 3:
			bigUnit._Self._id = atualSlot.Combatant;
			break;
		case 4:
			bigUnit._Self._id = atualSlot.General;
			break;
		case 5:
			bigUnit._Self._id = atualSlot.King;
			break;
		case 6:
			bigUnit._Self._id = atualSlot.God;
			break;
			
		}
		bigUnit._Self.selfObjUnit = Resources.Load<UnitObj>("Scripts/Unit/"+bigUnit._Self._id);
		bigUnit._Self.LoadCardDATA();
		ActiveEvos();
	}
	
	public void ActiveEvos(){
		if (atualSlot.Civil!= ""){ evos[0].interactable = true;} else {evos[0].interactable = false;}
		if (atualSlot.Soldier!= ""){ evos[1].interactable = true;} else {evos[1].interactable = false;}
		if (atualSlot.Combatant!= ""){ evos[2].interactable = true;} else {evos[2].interactable = false;}
		if (atualSlot.General!= ""){ evos[3].interactable = true;} else {evos[3].interactable = false;}
		if (atualSlot.King!= ""){ evos[4].interactable = true;} else {evos[4].interactable = false;}
		if (atualSlot.God!= ""){ evos[5].interactable = true;} else {evos[5].interactable = false;}
	}
	
}
