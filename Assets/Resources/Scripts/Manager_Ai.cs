using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class Manager_Ai : MonoBehaviour
{
	public GameObject[] enemyUnits;
	public GameObject[] onField ;
	public GameObject[] onHand ;
	public GameObject[] canSummonUnits ;
	public GameObject[] canMoveUnits ;
	public GameObject[] summonSpot;
	public GameObject[] vp;
	public GameObject[] bp;
	public GameObject[] summonSpot2;
	
	public Terreno terreno;
	private bool aiOn;
    // Update is called once per frame
	void Update()
	{
		if (SceneVariables_Battle.atualTurn == "P2")
		{
			if (aiOn != true)
			{
				StartCoroutine(AiCoroutine());
			}
		}
	}
    
	public IEnumerator AiCoroutine()
	{
		aiOn = true;
		yield return new WaitForSecondsRealtime (1f);
		AiChoiseNextStep();
		aiOn= false;
	}
	
    
	public void AiChoiseNextStep()
	{
		ArrayUtility.Clear<GameObject>(ref onHand);
		ArrayUtility.Clear<GameObject>(ref onField);
		ArrayUtility.Clear<GameObject>(ref canMoveUnits);
		foreach (GameObject g in enemyUnits)
		{	//Separa os do campo e os da mao, com o gameobject da mao.
			var f =g.GetComponent<Unit>();
			if (f._OnField)
			{ArrayUtility.Add<GameObject>(ref onField, g);}
			else{ArrayUtility.Add<GameObject>(ref onHand, g);}
		}
		foreach (GameObject g in onField)
		{	//Separa os do campo se pode movimentar.
			var f = g.GetComponent<Unit>();
			var e = f._UnitOnField;
			var t = e.GetComponent<UnitField>();
			if (t._CanMove)
			{
				ArrayUtility.Add<GameObject>(ref canMoveUnits, g);
			}
		}
		// Escolhe proximo passo, priorizando a sequencia
		// Summon [quantas vezes possivel> Movimento > finalizar o turno.
		if( onField.Length > 0 && canMoveUnits.Length > 0)
		{
			AiMoviment();
			return;
		}else if (SceneVariables_Battle.p2.canSummon && SceneVariables_Battle.atualTurn == "P2" && onHand.Length > 0)
		{
			AiSummon();
			return;
		}else 
		{
			var x = FindObjectOfType<ManagerTurn>();
			x.NextTurn();
		}
	}
	
	public void AiSummon()
	{
		Debug.Log("Ai tentando invocar...");
		ArrayUtility.Clear<GameObject>(ref canSummonUnits);
		foreach (GameObject g in onHand)
		{	// verifica os que podem ser invocados.
			var d = g.GetComponent<Unit>();
			if (d._life > d._originalLife/2+0.5f)
			{
				ArrayUtility.Add<GameObject>(ref canSummonUnits, g);
			}
		}
		
		if (canSummonUnits.Length > 0)
		{	// Verifica quantos podem ser invocados
			var x = Random.Range(0,canSummonUnits.Length);
			var z = canSummonUnits[x];
			var unit = z.GetComponent<Unit>();
			ArrayUtility.Clear<GameObject>(ref summonSpot2);
			foreach (GameObject g in summonSpot)
			{// verifica sem tem lugar para invocar livre.
				var f = g.GetComponent<Terreno>();
				if(f._HasUnit != true)
				{
					ArrayUtility.Add<GameObject>(ref summonSpot2, g);
				}
			}
			
			if (summonSpot2.Length > 0)
			{//completa a invocaçao se possivel.
			var u = Random.Range(0, summonSpot2.Length);
			SceneVariables_Battle._LastTerreno = summonSpot2[u];
			SceneVariables_Battle._LastSummonUnit = unit;
			unit.FinishSummon();
			SceneVariables_Battle.p2.canSummon = false;
			}else 
			{
				SceneVariables_Battle.p2.canSummon = false;
			}
		}else 
		{
			SceneVariables_Battle.p2.canSummon = false;
		}
	}
	
	public void AiMoviment()
	{
		Debug.Log("AI começando a Mover.");
		ArrayUtility.Clear<GameObject>(ref canMoveUnits);
		ArrayUtility.Clear<GameObject>(ref onHand);
		ArrayUtility.Clear<GameObject>(ref onField);
		foreach (GameObject g in enemyUnits)
		{	//Separa os do campo e os da mao, com o gameobject da mao.
			var f =g.GetComponent<Unit>();
			if (f._OnField)
			{ArrayUtility.Add<GameObject>(ref onField, g);}
			else{ArrayUtility.Add<GameObject>(ref onHand, g);}
		}
		foreach (GameObject g in onField)
		{	// verifica quantos do campo ainda podem se mover
			var f = g.GetComponent<Unit>();
			var e = f._UnitOnField;
			var t = e.GetComponent<UnitField>();
			if (t._CanMove)
			{
				ArrayUtility.Add<GameObject>(ref canMoveUnits, g);
			}
		}
		if (canMoveUnits.Length > 0)
		{// escolhe para qual terreno se mover,se possivel.
			var choiseUnit = canMoveUnits[0];
			var x = choiseUnit.GetComponent<Unit>();
			var x2 = x._UnitOnField;
			var z = x2.GetComponent<UnitField>();
			z.OnMouseDown();
			var visinhos = z._Visinhos;
			ArrayUtility.Clear<GameObject>(ref vp);
			ArrayUtility.Clear<GameObject>(ref bp);
			foreach (GameObject g in visinhos)
			{
				if (g != null)
				{
					terreno = g.GetComponent<Terreno>();
					if (terreno._HasUnit != true)
					{
						ArrayUtility.Add<GameObject>(ref vp, g);
					}else
					{
						var tt = terreno._UnitOver.GetComponent<UnitField>();
						if (tt._isplayer){ArrayUtility.Add<GameObject>(ref bp, tt.gameObject);}
					}
					
				}
			}
			if ( bp.Length > 0){
				var go = Random.Range(1,3);
				switch (go)
				{
				case 1:
					var x5 = FindObjectOfType<Manager_Battle>();
					var tr = bp[0];
					var atk = x2.GetComponent<UnitField>();
					var def = tr.GetComponent<UnitField>();
					Debug.Log("Atacou" + atk._originalUnit + " vs " + def._originalUnit );
					x5.Batalha(atk._originalUnit, def._originalUnit);
					break;
				case 2:
					if (vp.Length > 0)
					{
						var goTo = Random.Range(0,vp.Length);
						var terreno = vp[goTo];
						var t = terreno.GetComponent<Terreno>();
						t.MoveUnit();
					}else
					{
						z._CanMove = false;
						Debug.Log("AI nao consegue mover");
						var t2 = FindObjectOfType<Terreno>();
						t2.CancelMoveUnit();
					}
					break;
				}
				
			}else if (vp.Length > 0)
			{
				var goTo = Random.Range(0,vp.Length);
				var terreno = vp[goTo];
				var t = terreno.GetComponent<Terreno>();
				t.MoveUnit();
			}else
			{
				z._CanMove = false;
				Debug.Log("AI nao consegue mover");
				var t2 = FindObjectOfType<Terreno>();
				t2.CancelMoveUnit();
			}
			
		}else 
		{
			Debug.Log("AI nao consegue mover, sem unidades");
		}
		
	}
	
	public void AiAttack()
	{
		Debug.Log("AI Atacando.");
	}
   
}
