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
		case "00000001":
			if (u._type == "Dragon" ||u._type == "Rock" || u._type == "Pyro"){
				u._ProvisoryModATK = 2;
				unit._BoostMoviment = true;}
			break;
		case "00000002":
			if (u._type == "Vampir" ||u._type == "Wich" || u._type == "Paranormal"){
				u._ProvisoryModATK = 2;
				unit._BoostMoviment = true;}
			break;
		case "00000003":
			if (u._type == "Warrior" ||u._type == "Demon" || u._type == "Guardian"){
				u._ProvisoryModATK = 2;
				unit._BoostMoviment = true;}
			break;
		case "00000004":
			if (u._type == "Angel" ||u._type == "Plant" || u._type == "Insect" || u._type == "Thunder"){
				u._ProvisoryModATK = 2;
				unit._BoostMoviment = true;}
			break;
		case "00000005":
			unit._SpecialStatus = "NoDestruction";
			break;
		case "00000006":
			unit._SpecialStatus = "NoEffect";
			break;
		case "00000007":
			u._ProvisoryModATK = 2;
			unit._SpecialStatus = "CountdownBoostATK2";
			break;
		case "00000008":
			u._ProvisoryModDEF = 2;
			unit._SpecialStatus = "CountdownBoostDEF2";
			break;
		case "00000009":
			if (u._EvolutionCheck.ForbiddenRitual != true)
			{
				u.TryEvolutionType();
				u._EvolutionCheck.ForbiddenRitual = true;
			}
			break;
		case "00000010":
			if (u._EvolutionCheck.ForbiddenCalice != true)
			{
				u.TryEvolutionElement();
				u._EvolutionCheck.ForbiddenCalice = true;
			}
			break;
		case "00000019":
			if (u._EvolutionCheck.EmergencyEvolution != true)
			{
				u.TryEvolution();
				u._EvolutionCheck.EmergencyEvolution = true;
			}
			break;
		case "00000011":
			u._PermanentModATK = -2;
			break;
		case "00000012":
			u._PermanentModDEF = -2;
			break;
		case "00000013":
			u._OnStatus = "Sleep";
			u._OnStatusTurn = Random.Range(1,7);
			break;
		case "00000014":
			u._OnStatus = "Paralize";
			u._OnStatusTurn = Random.Range(1,7);
			break;
		case "00000015":
			u._OnStatus = "Burn";
			u._OnStatusTurn = Random.Range(1,7);
			break;
		case "00000016":
			u._OnStatus = "Poison";
			u._OnStatusTurn = Random.Range(1,7);
			break;
		case "00000017":
			if (unit._SpecialStatus == "NoEffect" || unit._SpecialStatus == "NoDestruction")
			{	u._OnStatus = "Burn";
				u._OnStatusTurn = Random.Range(1,7);}
			else {u._life = 0;}
			break;
		case "00000018":
		
			break;
		case "00000020":
		
			break;
		}
		
	}
}
