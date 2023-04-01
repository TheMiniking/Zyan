﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Sirenix.OdinInspector;

public class ManagerTurn : MonoBehaviour
{
    public TMP_Text turnCount;
    public TMP_Text player;
    public SpriteRenderer turnBG;
	public UnitField[] unitFList ;
	[ShowInInspector] public static bool _PlayerTurn;
	public SceneVariables_Battle svb;
	
	void Awake()=> svb.Start();
	
	void Start()=> NextTurn();
	
	private bool _PosPlayerHud;
    public void NextTurn()
	{
		// Habilidade Eternal Life + Reset Mod Atributes
		var unitZ = FindObjectsOfType<Unit>();
		foreach (Unit u in unitZ)
		{
			u._Self._TimeToReset = u._Self._TimeToReset>=0?(u._Self._TimeToReset-1):-1;
			if (u._Self._effect1 == "Eternal Life" || u._Self._effect2 == "Eternal Life" )
			{	//Debug.Log(u._effect1 + " e " + u._effect2);
				if (u._Self._life < u._Self._originalLife*2)
				{
					u._Self._life++;
				}
				
			}
			if (u._Self._OnStatus == "Sleep")
			{ u._UnitOnField.GetComponent<UnitField>()._CanMove = false;}
		}
		var terrenoZ = FindObjectsOfType<Terreno>();
		foreach (Terreno t in terrenoZ)
		{
			if (t._SpellData.SubType == "Terreno" && t._LastUnitOver != null){
				SpellEffects.SpellToActive(t._SpellData.Script , t._LastUnitOver.GetComponent<UnitField>());
			}
		}
		// troca de turno
		svb.turnCount++;
		svb.PlayerEnergy++;
		svb.EnemyEnergy++;
        switch (_PlayerTurn)
       {case true:
	        svb.atualTurn = "P2";
	       svb.atualTurnID = svb.enemyID;
	        svb.p2.canSummon = true;
            turnBG.color = Color.green;
	        _PlayerTurn = false;
            break;
        case false:
	        svb.atualTurn = "P1";
	        svb.atualTurnID = svb.playerID;
	        svb.p1.canSummon = true;
            turnBG.color = Color.white;
	        _PlayerTurn = true;
	        break; 
       }
        turnCount.text = "" + svb.turnCount;
        player.text = "" + svb.atualTurn;
	    unitFList = FindObjectsOfType<UnitField>();
	    var un = FindObjectsOfType<Unit>();
	    foreach (Unit u in un)
	    {if (u._Enfermary)
	    {u._Self.TurnEnfermary();}}
		foreach ( UnitField c in unitFList)
		{if(c.unit._Self._OnStatus!="Sleep")c._CanMove = true;}
		Manager_UI ui = FindObjectOfType<Manager_UI>();
		ui.StShowAuto();
	}
	
    public void ResetScene()
    {
        SceneManager.LoadScene("Battle-NoVS");
    }
}

