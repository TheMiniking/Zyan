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
			if (u._Self._type == "Dragon" ||u._Self._type == "Rock" || u._Self._type == "Pyro"){
				u._Self._ProvisoryModATK = 2;
				u._Self._ProvisoryModDEF = 0;
				unit._BoostMoviment = true;}
			break;
		case "00000002": // Darkness Florest
			if (u._Self._type == "Vampir" ||u._Self._type == "Wich" || u._Self._type == "Paranormal"){
				u._Self._ProvisoryModATK = 2;
				u._Self._ProvisoryModDEF = 0;
				unit._BoostMoviment = true;}
			break;
		case "00000003": // Gate of Underworld
			if (u._Self._type == "Warrior" ||u._Self._type == "Demon" || u._Self._type == "Guardian"){
				u._Self._ProvisoryModATK = 2;
				u._Self._ProvisoryModDEF = 0;
				unit._BoostMoviment = true;}
			break;
		case "00000004": // Kingdom of Sky
			if (u._Self._type == "Angel" ||u._Self._type == "Plant" || u._Self._type == "Insect" || u._Self._type == "Thunder"){
				u._Self._ProvisoryModATK = 2;
				u._Self._ProvisoryModDEF = 0;
				unit._BoostMoviment = true;}
			break;
		case "00000005": // Divine Protection
			unit._SpecialStatus = "NoDestruction";
			u._Self._TimeToReset = 2;
			break;
		case "00000006": // Moviment Atecipation
			unit._SpecialStatus = "NoEffect";
			u._Self._TimeToReset = 2;
			break;
		case "00000007": // Positive Signal
			u._Self._ProvisoryModATK = 2;
			u._Self._ProvisoryModDEF = 0;
			unit._SpecialStatus = "CountdownBoostATK2";
			u._Self._TimeToReset = 2;
			break;
		case "00000008": // Clear Mind
			u._Self._ProvisoryModDEF = 2;
			u._Self._ProvisoryModATK = 0;
			unit._SpecialStatus = "CountdownBoostDEF2";
			u._Self._TimeToReset = 2;
			break;
		case "00000009": // Forbidden Ritual
			if (u._Self._EvolutionCheck.ForbiddenRitual != true)
			{
				u._Self.TryEvolutionType();
				u._Self._EvolutionCheck.ForbiddenRitual = true;
			}
			break;
		case "00000010": // Forbidden Cicle
			if (u._Self._EvolutionCheck.ForbiddenCalice != true)
			{
				u._Self.TryEvolutionElement();
				u._Self._EvolutionCheck.ForbiddenCalice = true;
			}
			break;
		case "00000019": // Emergency Evolution
			if (u._Self._EvolutionCheck.EmergencyEvolution != true)
			{
				u._Self.TryEvolution();
				u._Self._EvolutionCheck.EmergencyEvolution = true;
			}
			break;
		case "00000011": // Emotional Eartquake
			u._Self._PermanentModATK = u._Self._PermanentModATK - 2;
			break;
		case "00000012": // Dizzy Drink
			u._Self._PermanentModDEF = u._Self._PermanentModDEF - 2;
			break;
		case "00000013": // Sand of Cronos
			u._Self._OnStatus = "Sleep";
			u._Self._OnStatusTurn = Random.Range(1,7);
			break;
		case "00000014": // Thunder of Zeus
			u._Self._OnStatus = "Paralize";
			u._Self._OnStatusTurn = Random.Range(1,7);
			break;
		case "00000015": // Lava Geiser
			u._Self._OnStatus = "Burn";
			u._Self._OnStatusTurn = Random.Range(1,7);
			break;
		case "00000016": // Poison Seed 
			u._Self._OnStatus = "Poison";
			u._Self._OnStatusTurn = Random.Range(1,7);
			break;
		case "00000017": // Classic Trap
			if (unit._SpecialStatus == "NoEffect" || unit._SpecialStatus == "NoDestruction")
			{	u._Self._OnStatus = "Burn";
				u._Self._OnStatusTurn = Random.Range(1,7);}
			else {u._Self._life = 0;}
			break;
		case "00000018": // Half-Half
			u._Self._life = Mathf.RoundToInt(u._Self._life/2);
			break;
		case "00000020": // Last Scrificie
			u._Self._ProvisoryModATK =u._Self._ProvisoryModATK + 4;
			u._Self._life = u._Self._life - 2;
			u._Self._OnStatus = "Burn";
			u._Self._OnStatusTurn = Random.Range(1,7);
			break;
		}
		
	}
}
