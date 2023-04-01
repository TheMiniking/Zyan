using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using TMPro;

public class DeckEdit : MonoBehaviour
{
	public GameObject[] timeObj;
	public GameObject[] slotsObj;
	public PlayerInventaryOBJ player;
	public GameObject originalUnit;
	public GameObject painelUnit;
	public List<GameObject> unitsObj= new List<GameObject>{};
	public int atualSlot = 1;
	public int atualRank;
	public TMP_Dropdown rankDp;
	public TMP_Dropdown ElementDp;
	public TMP_Dropdown TDp;
	public GameObject searchObj;
	
	
	[Button]
	public void LoadTime(){
		var atualdeck = player.AtualDeck;
		foreach (GameObject s in timeObj){ 
			s.gameObject.SetActive(true);
			var unit = s.GetComponent<ShowUnit>();
			switch (unit._slotDeck){
			case 1:
				unit._slot = atualdeck.Slot1;
				break;
			case 2:
				unit._slot = atualdeck.Slot2;
				break;
			case 3:
				unit._slot = atualdeck.Slot3;
				break;
			case 4:
				unit._slot = atualdeck.Slot4;
				break;
			case 5:
				unit._slot = atualdeck.Slot5;
				break;
			}
			unit._id = unit._slot.Civil;
			unit.LoadCard();
		}
		LoadSlots( atualSlot );
	}
	
	public void LoadSlots(int slotN){
		atualRank = -1;
		Zyan.TimeUnits slt = player.AtualDeck.Slot1;
		switch(slotN){
		case 1:
			slt = player.AtualDeck.Slot1;
			break;
		case 2:
			slt = player.AtualDeck.Slot2;
			break;
		case 3:
			slt = player.AtualDeck.Slot3;
			break;
		case 4:
			slt = player.AtualDeck.Slot4;
			break;
		case 5:
			slt = player.AtualDeck.Slot5;
			break;
		}
		if (slt.Civil != ""){
			slotsObj[0].gameObject.SetActive(true);
			slotsObj[0].GetComponent<ShowUnit>()._id = slt.Civil;
			slotsObj[0].GetComponent<ShowUnit>().LoadCard();}
		else {slotsObj[0].gameObject.SetActive(false);}
		if (slt.Soldier != ""){
			slotsObj[1].gameObject.SetActive(true);
			slotsObj[1].GetComponent<ShowUnit>()._id = slt.Soldier;
			slotsObj[1].GetComponent<ShowUnit>().LoadCard();}
		else {slotsObj[1].gameObject.SetActive(false);}
		if (slt.Combatant != ""){
			slotsObj[2].gameObject.SetActive(true);
			slotsObj[2].GetComponent<ShowUnit>()._id = slt.Combatant;
			slotsObj[2].GetComponent<ShowUnit>().LoadCard();}
		else {slotsObj[2].gameObject.SetActive(false);}
		if (slt.General != ""){
			slotsObj[3].gameObject.SetActive(true);
			slotsObj[3].GetComponent<ShowUnit>()._id = slt.General;
			slotsObj[3].GetComponent<ShowUnit>().LoadCard();}
		else {slotsObj[3].gameObject.SetActive(false);}
		if (slt.King != ""){
			slotsObj[4].gameObject.SetActive(true);
			slotsObj[4].GetComponent<ShowUnit>()._id = slt.King;
			slotsObj[4].GetComponent<ShowUnit>().LoadCard();}
		else {slotsObj[4].gameObject.SetActive(false);}
		if (slt.God != ""){
			slotsObj[5].gameObject.SetActive(true);
			slotsObj[5].GetComponent<ShowUnit>()._id = slt.Civil;
			slotsObj[5].GetComponent<ShowUnit>().LoadCard();}
		else {slotsObj[5].gameObject.SetActive(false);}
		atualSlot = slotN;
	}
	
	[Button]
	public void LoadlibraryObj(){
		string rank = rankDp.captionText.text;
		string element = ElementDp.captionText.text;
		string type = TDp.captionText.text;
		atualRank = rank == "All Ranks"?-1 :atualRank ;
		LimpaObjSearch();
		foreach(KeyValuePair<string , Zyan.IdIndex>  i in player.PlayerInventaryUnit){
			if (i.Value.Quantidade > 0 ){
				var uu = Resources.Load<UnitObj>("Scripts/Unit/"+i.Value.Id);
				if (rank == "All Ranks"){
					if ( type == "All Type"){
						if (element == "All Elements"){
							GameObject u = Instantiate(originalUnit,painelUnit.transform.parent);
							u.GetComponent<ShowUnit>()._id = i.Value.Id;
							u.GetComponent<ShowUnit>().LoadCard();
							unitsObj.Add(u);
						}else if (element == uu.Element){
								GameObject u = Instantiate(originalUnit,painelUnit.transform.parent);
								u.GetComponent<ShowUnit>()._id = i.Value.Id;
								u.GetComponent<ShowUnit>().LoadCard();
							unitsObj.Add(u);	}}
					else if ( type == uu.Type){
						if (element == "All Elements"){
							GameObject u = Instantiate(originalUnit,painelUnit.transform.parent);
							u.GetComponent<ShowUnit>()._id = i.Value.Id;
							u.GetComponent<ShowUnit>().LoadCard();
							unitsObj.Add(u);
						}else if (element == uu.Element){
							GameObject u = Instantiate(originalUnit,painelUnit.transform.parent);
							u.GetComponent<ShowUnit>()._id = i.Value.Id;
							u.GetComponent<ShowUnit>().LoadCard();
							unitsObj.Add(u);	}}
				} else if (rank == uu.Rank){
					if ( type == "All Type"){
						if (element == "All Elements"){
							GameObject u = Instantiate(originalUnit,painelUnit.transform.parent);
							u.GetComponent<ShowUnit>()._id = i.Value.Id;
							u.GetComponent<ShowUnit>().LoadCard();
							unitsObj.Add(u);
						}else if (element == uu.Element){
							GameObject u = Instantiate(originalUnit,painelUnit.transform.parent);
							u.GetComponent<ShowUnit>()._id = i.Value.Id;
							u.GetComponent<ShowUnit>().LoadCard();
							unitsObj.Add(u);	}}
					else if ( type == uu.Type){
						if (element == "All Elements"){
							GameObject u = Instantiate(originalUnit,painelUnit.transform.parent);
							u.GetComponent<ShowUnit>()._id = i.Value.Id;
							u.GetComponent<ShowUnit>().LoadCard();
							unitsObj.Add(u);
						}else if (element == uu.Element){
							GameObject u = Instantiate(originalUnit,painelUnit.transform.parent);
							u.GetComponent<ShowUnit>()._id = i.Value.Id;
							u.GetComponent<ShowUnit>().LoadCard();
							unitsObj.Add(u);	}}
				}
				
			}
		}
	}
	
	[Button]
	public void LoadlibraryObjAdicional(bool typeOrElement){
		string rank = rankDp.captionText.text;
		string element = ElementDp.captionText.text;
		string type = TDp.captionText.text;
		atualRank = rank == "All Ranks"?-1 :atualRank ;
		foreach(KeyValuePair<string , Zyan.IdIndex>  i in player.PlayerInventaryUnit){
			if (i.Value.Quantidade > 0 ){
				var uu = Resources.Load<UnitObj>("Scripts/Unit/"+i.Value.Id);
				if (rank == uu.Rank){
					if ( !typeOrElement){ 
						if (element == uu.Element){
							bool uEx = false;
							foreach (GameObject g in unitsObj){if (!uEx) {uEx = g.GetComponent<ShowUnit>()._id==uu.id?true:false;}}
							if (!uEx){
								GameObject u = Instantiate(originalUnit,painelUnit.transform.parent);
								u.GetComponent<ShowUnit>()._id = i.Value.Id;
								u.GetComponent<ShowUnit>().LoadCard();
								unitsObj.Add(u);
							}}}
					else if (type == uu.Type){
						bool uEx = false;
						foreach (GameObject g in unitsObj){if (!uEx) {uEx = g.GetComponent<ShowUnit>()._id==uu.id?true:false;}}
						if (!uEx){
							GameObject u = Instantiate(originalUnit,painelUnit.transform.parent);
							u.GetComponent<ShowUnit>()._id = i.Value.Id;
							u.GetComponent<ShowUnit>().LoadCard();
							unitsObj.Add(u);
						}}
				}
			}
		}
	}
	
	void LimpaObjSearch(){
		if (unitsObj != null){
			foreach (GameObject g in unitsObj){Object.Destroy(g);}
			unitsObj.Clear();
		}
	}
	public void Search(){
		atualRank = rankDp.value==6?-1:rankDp.value+1;
		Search(atualRank);}
	//Tranforma procura simples para complexa com base no anterior
	public void Search(int rankU){
		if (searchObj.activeSelf){
		atualRank = rankU;
		rankDp.value = rankU==-1?6:rankU-1;
		LoadlibraryObj();}
		else{
			Search(rankU,true);
		}
	}
	
	public void Search(int rankU, bool autoSearch){
		atualRank = rankU;
		rankDp.value = rankU==-1?6:rankU-1;
		if(autoSearch && rankDp.value>0){
			var ele = slotsObj[rankDp.value-1].GetComponent<ShowUnit>()._unit.Element;
			switch (ele){
			case "Divinity":ElementDp.value = 0;break;
			case "Radioativity":ElementDp.value = 1;break;
			case "Genetic":ElementDp.value = 2;break;
			case "Natural":ElementDp.value = 3;break;
			case "Elemental":ElementDp.value = 4;break;
			case "Light":ElementDp.value = 5;break;
			case "Dark":ElementDp.value = 6;break;
			case "Fire":ElementDp.value = 7;break;
			case "Earth":ElementDp.value = 8;break;
			case "Wind":ElementDp.value = 9;break;
			case "Water":ElementDp.value = 10;break;
			case "Metal":ElementDp.value = 11;break;
			case "Ice":ElementDp.value = 12;break;
			case "Nature":ElementDp.value = 13;break;
			case "Eletricity":ElementDp.value = 14;break;
			}
			var stype =slotsObj[rankDp.value-1].GetComponent<ShowUnit>()._unit.Type;
			switch (stype){
			case "Dragon":TDp.value = 0;break;
			case "Warrior":TDp.value = 1;break;
			case "Angel":TDp.value = 2;break;
			case "Demon":TDp.value = 3;break;
			case "Witch":TDp.value = 4;break;
			case "Vampir":TDp.value = 5;break;
			case "Paranormal":TDp.value = 6;break;
			case "Sea-Monster":TDp.value = 7;break;
			case "Beast":TDp.value = 8;break;
			case "Winged-Beast":TDp.value = 9;break;
			case "Dinosaur":TDp.value = 10;break;
			case "Reptile":TDp.value = 11;break;
			case "Guardian":TDp.value = 12;break;
			case "Machine":TDp.value = 13;break;
			case "Cyber":TDp.value = 14;break;
			case "Insect":TDp.value = 15;break;
			case "Plant":TDp.value = 16;break;
			case "Rock":TDp.value = 17;break;
			case "Pyro":TDp.value = 18;break;
			case "Thunder":TDp.value = 19;break;
			case "Alien":TDp.value = 20;break;
			case "Mytholic-Beast":TDp.value = 21;break;
			case "Mytholic-God":TDp.value = 22;break;
			}
		}else{
			ElementDp.value = 15;
			TDp.value = 23;
		}
		if(autoSearch){
			LimpaObjSearch();
			LoadlibraryObjAdicional(false);
			LoadlibraryObjAdicional(true);
		}else{LoadlibraryObj();}
		
	}
}
