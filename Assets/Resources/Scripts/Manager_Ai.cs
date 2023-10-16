using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class Manager_Ai : MonoBehaviour
{
	public List<GameObject> enemyUnits;
	public List<GameObject> onField ;
	public List<GameObject> onHand ;
	public List<GameObject> canSummonUnits ;
	public List<GameObject> canMoveUnits ;
	public List<GameObject> summonSpot;
	public List<GameObject> vp;
	public List<GameObject> bp;
	public List<GameObject> summonSpot2;
	
	public SceneVariables_Battle svb;
	
	public Terreno terreno;
	private bool aiOn;
    // Update is called once per frame
	void Update()
	{
		if (svb.atualTurn == "P2")
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
		yield return new WaitForSecondsRealtime (2f);
		AiChoiseNextStep();
		aiOn= false;
	}
	
    
	public void AiChoiseNextStep()
	{
		onHand = new List<GameObject>{};
		onField = new List<GameObject>{};
		canMoveUnits = new List<GameObject>{};
		foreach (GameObject g in enemyUnits)
		{	//Separa os do campo e os da mao, com o gameobject da mao.
			var f =g.GetComponent<Unit>();
			if (f._OnField)
			{onField.Add(g);}
			else{onHand.Add(g);}
		}
		foreach (GameObject g in onField)
		{	//Separa os do campo se pode movimentar.
			var f = g.GetComponent<Unit>();
			var e = f._UnitOnField;
			var t = e.GetComponent<UnitField>();
			if (t._CanMove)
			{
				canMoveUnits.Add(g);
			}
		}
		// Escolhe proximo passo, priorizando a sequencia
		// Summon [quantas vezes possivel> Movimento > finalizar o turno.
		if( onField.Count > 0 && canMoveUnits.Count > 0)
		{
			AiMoviment();
			return;
		}else if (svb.p2.canSummon && svb.atualTurn == "P2" && onHand.Count > 0)
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
		canSummonUnits = new List<GameObject>{};
		foreach (GameObject g in onHand)
		{	// verifica os que podem ser invocados.
			var d = g.GetComponent<Unit>();
			if (d._Self._life > d._Self._originalLife/2+0.5f)
			{
				canSummonUnits.Add(g);
			}
		}
		
		if (canSummonUnits.Count > 0)
		{	// Verifica quantos podem ser invocados
			var x = Random.Range(0,canSummonUnits.Count);
			var z = canSummonUnits[x];
			var unit = z.GetComponent<Unit>();
			summonSpot2 = new List<GameObject>{};
			foreach (GameObject g in summonSpot)
			{// verifica sem tem lugar para invocar livre.
				var f = g.GetComponent<Terreno>();
				if(f._HasUnit != true)
				{
					summonSpot2.Add(g);
				}
			}
			
			if (summonSpot2.Count > 0)
			{//completa a invocaçao se possivel.
				var u = Random.Range(0, summonSpot2.Count);
			svb._LastTerreno = summonSpot2[u];
			svb._LastSummonUnit = unit;
			unit.FinishSummon();
			svb.p2.canSummon = false;
			}else 
			{
				svb.p2.canSummon = false;
			}
		}else 
		{
			svb.p2.canSummon = false;
		}
	}
	
	public void AiMoviment()
	{
		Debug.Log("AI começando a Mover.");
		onHand = new List<GameObject>{};
		onField = new List<GameObject>{};
		canMoveUnits = new List<GameObject>{};
		foreach (GameObject g in enemyUnits)
		{	//Separa os do campo e os da mao, com o gameobject da mao.
			var f =g.GetComponent<Unit>();
			if (f._OnField)
			{onField.Add(g);}
			else{onHand.Add(g);}
			//Debug.Log(onHand.Count +" , "+ onField.Count  + " , "+ canMoveUnits.Count);
		}
		foreach (GameObject g in onField)
		{	// verifica quantos do campo ainda podem se mover
			var f = g.GetComponent<Unit>();
			var e = f._UnitOnField;
			var t = e.GetComponent<UnitField>();
			if (t._CanMove)
			{
				if (f._Self._OnStatus != "Sleep"){canMoveUnits.Add(g);}
				else{t._CanMove = false;}
				
			}
		}
		if (canMoveUnits.Count > 0)
		{// escolhe para qual terreno se mover,se possivel.
			var choiseUnit = canMoveUnits[0];
			var x = choiseUnit.GetComponent<Unit>();
			var x2 = x._UnitOnField;
			var z = x2.GetComponent<UnitField>();
			z.OnMouseDown();
			var visinhos = z._Visinhos;
			vp = new List<GameObject>{};
			bp = new List<GameObject>{};
			foreach (GameObject g in visinhos)
			{
				if (g != null)
				{
					terreno = g.GetComponent<Terreno>();
					if (terreno._HasUnit != true)
					{
						vp.Add(g);
					}else
					{
						var tt = terreno._UnitOver.GetComponent<UnitField>();
						if (tt._isplayer){bp.Add(tt.gameObject);}
					}
					
				}
			}
			if ( bp.Count > 0){
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
					if (vp.Count > 0)
					{
						var goTo = Random.Range(0,vp.Count);
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
				
			}else if (vp.Count > 0)
			{
				var goTo = Random.Range(0,vp.Count);
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
