using System.Collections;
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
	
	void Start()=> NextTurn();
	
	private bool _PosPlayerHud;
    public void NextTurn()
	{
		// Habilidade Eternal Life
		var unitZ = FindObjectsOfType<Unit>();
		foreach (Unit u in unitZ)
		{
			if (u._effect1 == "Eternal Life" || u._effect2 == "Eternal Life" )
			{	//Debug.Log(u._effect1 + " e " + u._effect2);
				if (u._life < u._originalLife*2)
				{
					u._life++;
				}
			}
		}
		// troca de turno
		SceneVariables_Battle.turnCount++;
		SceneVariables_Battle.PlayerEnergy++;
		SceneVariables_Battle.EnemyEnergy++;
        switch (_PlayerTurn)
       {case true:
	        SceneVariables_Battle.atualTurn = "P2";
	       SceneVariables_Battle.atualTurnID = SceneVariables_Battle.enemyID;
	        SceneVariables_Battle.p2.canSummon = true;
            turnBG.color = Color.green;
	        _PlayerTurn = false;
            break;
        case false:
	        SceneVariables_Battle.atualTurn = "P1";
	        SceneVariables_Battle.atualTurnID = SceneVariables_Battle.playerID;
	        SceneVariables_Battle.p1.canSummon = true;
            turnBG.color = Color.white;
	        _PlayerTurn = true;
	        break; 
       }
        turnCount.text = "" + SceneVariables_Battle.turnCount;
        player.text = "" + SceneVariables_Battle.atualTurn;
	    unitFList = FindObjectsOfType<UnitField>();
	    foreach ( UnitField c in unitFList)
        {c._CanMove = true;}
	    var un = FindObjectsOfType<Unit>();
	    foreach (Unit u in un)
	    {if (u._Enfermary)
	    {u.TurnEnfermary();}}
	}
	
    public void ResetScene()
    {
        SceneManager.LoadScene("Battle-NoVS");
    }
}

