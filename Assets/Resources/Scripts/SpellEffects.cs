using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SpellEffects 
{
	public static void SpellToActive(string effectNumber ,UnitField unit)
	{
		var u = unit._originalUnit.GetComponent<Unit>();
		switch (effectNumber)
		{
		case "00000001": // Volcanic Arena
			if (u._type == "Dragon" ||u._type == "Rock" || u._type == "Pyro"){
				u._ProvisoryModATK = 2;
				u._ProvisoryModDEF = 0;
				unit._BoostMoviment = true;}
			break;
		case "00000002": // Darkness Florest
			if (u._type == "Vampir" ||u._type == "Wich" || u._type == "Paranormal"){
				u._ProvisoryModATK = 2;
				u._ProvisoryModDEF = 0;
				unit._BoostMoviment = true;}
			break;
		case "00000003": // Gate of Underworld
			if (u._type == "Warrior" ||u._type == "Demon" || u._type == "Guardian"){
				u._ProvisoryModATK = 2;
				u._ProvisoryModDEF = 0;
				unit._BoostMoviment = true;}
			break;
		case "00000004": // Kingdom of Sky
			if (u._type == "Angel" ||u._type == "Plant" || u._type == "Insect" || u._type == "Thunder"){
				u._ProvisoryModATK = 2;
				u._ProvisoryModDEF = 0;
				unit._BoostMoviment = true;}
			break;
		case "00000005": // Divine Protection
			unit._SpecialStatus = "NoDestruction";
			u._TimeToReset = 2;
			break;
		case "00000006": // Moviment Atecipation
			unit._SpecialStatus = "NoEffect";
			u._TimeToReset = 2;
			break;
		case "00000007": // Positive Signal
			u._ProvisoryModATK = 2;
			u._ProvisoryModDEF = 0;
			unit._SpecialStatus = "CountdownBoostATK2";
			u._TimeToReset = 2;
			break;
		case "00000008": // Clear Mind
			u._ProvisoryModDEF = 2;
			u._ProvisoryModATK = 0;
			unit._SpecialStatus = "CountdownBoostDEF2";
			u._TimeToReset = 2;
			break;
		case "00000009": // Forbidden Ritual
			if (u._EvolutionCheck.ForbiddenRitual != true)
			{
				u.TryEvolutionType();
				u._EvolutionCheck.ForbiddenRitual = true;
			}
			break;
		case "00000010": // Forbidden Cicle
			if (u._EvolutionCheck.ForbiddenCalice != true)
			{
				u.TryEvolutionElement();
				u._EvolutionCheck.ForbiddenCalice = true;
			}
			break;
		case "00000019": // Emergency Evolution
			if (u._EvolutionCheck.EmergencyEvolution != true)
			{
				u.TryEvolution();
				u._EvolutionCheck.EmergencyEvolution = true;
			}
			break;
		case "00000011": // Emotional Eartquake
			u._PermanentModATK = u._PermanentModATK - 2;
			break;
		case "00000012": // Dizzy Drink
			u._PermanentModDEF = u._PermanentModDEF - 2;
			break;
		case "00000013": // Sand of Cronos
			u._OnStatus = "Sleep";
			u._OnStatusTurn = Random.Range(1,7);
			break;
		case "00000014": // Thunder of Zeus
			u._OnStatus = "Paralize";
			u._OnStatusTurn = Random.Range(1,7);
			break;
		case "00000015": // Lava Geiser
			u._OnStatus = "Burn";
			u._OnStatusTurn = Random.Range(1,7);
			break;
		case "00000016": // Poison Seed 
			u._OnStatus = "Poison";
			u._OnStatusTurn = Random.Range(1,7);
			break;
		case "00000017": // Classic Trap
			if (unit._SpecialStatus == "NoEffect" || unit._SpecialStatus == "NoDestruction")
			{	u._OnStatus = "Burn";
				u._OnStatusTurn = Random.Range(1,7);}
			else {u._life = 0;}
			break;
		case "00000018": // Half-Half
			u._life = Mathf.RoundToInt(u._life/2);
			break;
		case "00000020": // Last Scrificie
			u._ProvisoryModATK =u._ProvisoryModATK + 4;
			u._life = u._life - 2;
			u._OnStatus = "Burn";
			u._OnStatusTurn = Random.Range(1,7);
			break;
		}
		
	}
}
