using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DentedPixel;
using TMPro;
using Sirenix.OdinInspector;

public class Terreno : MonoBehaviour
{
    public GameObject _SelfTerreno;
    public bool _HasUnit= false;
	public GameObject _UnitOver;
	
    public bool _OnRange = false;
    public bool _CanMoveOver = true;
    public Material _Base;
    public Material _BaseOcupado;
    public Material _BaseCanMove;
    public Material _BaseBattle;
    public Material _BaseMat;
    public Material[] _PackOcupado;
    public Material[] _PackBattle;
    public Material[] _PackNormal;
	public Material[] _PackCanMove;
	
	// _Visinhos index 
	// [0] UP	[1] UP Left 	[2]UP Right
	// [3] Down [4] Down Left	[5] Down Right
	public GameObject[] _Visinhos = new GameObject[6] ;

    public string _HexType;
    public bool _PlayerOwner;
	public string _PlayerOwnerID = "";
	public GameObject _ray; 
	public Zyan.SpellClass _SpellData;
	public bool _SpellActived = false;
    public Unit unit; 

	[TitleGroup("VFX")]
	public GameObject _VFXSummon;
	public GameObject _VFXDeath;
	public GameObject _VFXHeal;
	public GameObject _VFXEvolution;
	public GameObject _VFXActive;
	
    public void Start()
    {
        _PackBattle.SetValue(_BaseMat, 1);
        _PackNormal.SetValue(_BaseMat, 1);
        _PackCanMove.SetValue(_BaseMat, 1);
	    _PackOcupado.SetValue(_BaseMat, 1);
	    _VFXEvolution.SetActive(false);
	    _VFXDeath.SetActive(false);
	    _VFXHeal.SetActive(false);
	    _VFXEvolution.SetActive(false);
	    //if (_PlayerOwner && _HexType == "Summon"){ _PlayerOwnerID = SceneVariables_Battle.p1.ID; }
	    //else if (_PlayerOwner != true && _HexType == "Summon") { _PlayerOwnerID = SceneVariables_Battle.p2.ID; }
    }
	
	public IEnumerator AnimDeath(){
		_VFXDeath.SetActive(true);
		yield return new WaitForSecondsRealtime (1.5f);
		_VFXDeath.SetActive(false);
	}
	
	public IEnumerator AnimEvolution(){
		_VFXEvolution.SetActive(true);
		yield return new WaitForSecondsRealtime (1.5f);
		_VFXEvolution.SetActive(false);
	}
	
	public IEnumerator AnimHeal(){
		_VFXHeal.SetActive(true);
		yield return new WaitForSecondsRealtime (1.5f);
		_VFXHeal.SetActive(false);
	}
	
	public IEnumerator AnimActive(){
		_VFXActive.SetActive(true);
		yield return new WaitForSecondsRealtime (1.5f);
		_VFXActive.SetActive(false);
	}
	
    public void OnMouseDown()
	{
		CheckSpell();
    	SceneVariables_Battle._TerrenoAnterior = SceneVariables_Battle._LastTerreno;
    	SceneVariables_Battle._LastTerreno = _SelfTerreno;
        switch (_HexType) 
        {case "Normal" :
	        TryMove();
            break;
        case "Castelo":
	        TryMove();
            break;
        case "Summon":
	        //Se estiver movendo, se move, caso contrario tenta invocar,se foi o dono que clicou.
	        if (SceneVariables_Battle.p1.onMove && _OnRange && _CanMoveOver)
                	{ MoveUnit();
                	Debug.Log("Movendo...");}
	        else if ( _PlayerOwner && SceneVariables_Battle.p1.canSummon)
                {if (SceneVariables_Battle.atualTurnID == _PlayerOwnerID)
                { SceneVariables_Battle.p1.onSummon = true;
	                Debug.Log("Escolha unidade para invocar...");}
                else {Debug.Log("Não pode invocar no turno inimigo");}
                }
	        else if (_PlayerOwner && SceneVariables_Battle.p1.canSummon != true)
                {Debug.Log("Ja invocou nesse turno");
                CancelMoveUnit();}
                else if ( _PlayerOwner )
                	{ CancelMoveUnit();
	                Debug.Log("Local de invocaçao inimiga.");	} 
                else{ CancelMoveUnit();
	                Debug.Log("Cancelando Movimento.");
		            Debug.Log("Dados: PlayerOwnerID " + _PlayerOwnerID + " Atual Turn -" + SceneVariables_Battle.atualTurnID + "e PodeInvocar: " + SceneVariables_Battle.p1.canSummon );}
                break;
        case "Commander":
	        TryMove();
	        break;
        }
	}
	public void TryMove()
	{
		//Se move se ja iniciou o movimento e nao tem unit encima
	        if (SceneVariables_Battle.p1.onMove && _OnRange && _CanMoveOver) 
	        	{MoveUnit();}
	        else{CancelMoveUnit();}
	}
    
    public void FixedUpdate()
	{
		HaveUnitOver();
        UpdateMaterial();
    }
	private Terreno[] t;
	public void MoveUnit()
	{
		var x =SceneVariables_Battle._UnitToMove.GetComponent<UnitField>();
		LeanTween.move(x.gameObject ,this.transform.position + new Vector3 (0f,0.8f,0f), 0.25f );
		t = FindObjectsOfType<Terreno>();
		foreach(Terreno d in t)
		{
			d._OnRange = false;
		}
		x._CanMove = false;
	}
	
	public void CancelMoveUnit()
	{
		t = FindObjectsOfType<Terreno>();
		foreach(Terreno d in t)
		{
			d._OnRange = false;
		}
		SceneVariables_Battle.p1.onMove = false;
		SceneVariables_Battle.p1.onSummon = false;
		
	}
	
	public void EndMoveUnit()
	{
		t = FindObjectsOfType<Terreno>();
		foreach(Terreno d in t)
		{
			d._OnRange = false;
		}
		SceneVariables_Battle.p1.onMove = false;
		SceneVariables_Battle.p1.onSummon = false;
		var x =SceneVariables_Battle._UnitToMove.GetComponent<UnitField>();
		x._CanMove = false;
		
	}
	
	private UnitField x;
    public void  HaveUnitOver()
    {
	    Debug.DrawRay(_ray.transform.position, _ray.transform.TransformDirection(Vector3.forward)* 1f, Color.green);
	    if (Physics.Raycast(_ray.transform.position, _ray.transform.TransformDirection(Vector3.forward), out RaycastHit hitinfo, 1f))
	    { 
		    if (_SpellData.Name != "" && _SpellActived != true){
		    	_SpellActived = true ;
		    	StartCoroutine(AnimActive());
		    	Debug.Log(_SpellData.Name + " Spell Ativou!"); }
	        //Debug.Log(hitinfo.collider);
            _HasUnit = true;
		    _UnitOver = hitinfo.transform.gameObject;
		    x =  _UnitOver.GetComponent<UnitField>();
		    unit = x.unit;
		    x._AtualHex = _SelfTerreno;
		    x._Visinhos = _Visinhos;
		    if (_SpellData.id != "")
		    {
		    	
		    }
		    if (_PlayerOwner != x._isplayer && _HexType == "Castelo")
		    {
		    	Debug.Log(" Player "+ SceneVariables_Battle.atualTurn + "Win !!!");
		    }
		    
        }else
        {
            _HasUnit = false;
	        _UnitOver = null;
	        unit = null ;
        }
    }
    
	//public void OnMouseOver() => CheckSpell();
    
	public void CheckSpell()
	{
		var debugText = GameObject.Find("DebugLine").GetComponent<TMP_Text>();
		if (_SpellData.id != "" )
		{
			if (_SpellActived) FindObjectOfType<Manager_UI>()._TerrenoName = _SpellData.Name;
			else FindObjectOfType<Manager_UI>()._TerrenoName = "Terreno : " + _HexType;
		}else{
			FindObjectOfType<Manager_UI>()._TerrenoName = "Terreno : " + _HexType;
		}
	}
    
    
	public void UpdateMaterial()
	{
		if (SceneVariables_Battle.p1.onSummon && _HexType == "Summon" && SceneVariables_Battle._LastTerreno == this.gameObject)
		{ _VFXSummon.SetActive(true);}
		else {_VFXSummon.SetActive(false);}
		if (_PlayerOwner)
		{_PlayerOwnerID = SceneVariables_Battle.playerID;}
		else{ _PlayerOwnerID = SceneVariables_Battle.enemyID;}
        Renderer rend;
        if (_OnRange)
        {
            if (_HasUnit)
            {
                rend = GetComponent<Renderer>();
	            rend.sharedMaterials = _PackOcupado;
	            if (_UnitOver != null)
	            {x =  _UnitOver.GetComponent<UnitField>();
		            if (SceneVariables_Battle.p1.onMove && x._NoMove != null && x._Atack != null)
			           { if (x._Id == SceneVariables_Battle.atualTurnID)
			        		{ 
				           x._NoMove.SetActive(true);
				           x._Atack.SetActive(false); 
				           //Debug.Log("turnID:"+ SceneVariables_Battle.atualTurnID +" id : " + x._Id + "unit " + _UnitOver );
			        		}
			        	else
			        		{
				        	x._NoMove.SetActive(false);
				        	x._Atack.SetActive(true);
				        	//Debug.Log("turnID:"+ SceneVariables_Battle.atualTurnID +" id : " + x._Id + "unit " + _UnitOver );
			        		}
			           }
			        else
			           {
			           	x._NoMove.SetActive(false);
				        x._Atack.SetActive(false);
			           }
	            }
            }
            else
            {
                rend = GetComponent<Renderer>();
	            rend.sharedMaterials = _PackCanMove;
	            
	            }
        }
        else {
        	rend = GetComponent<Renderer>();
	        rend.sharedMaterials = _PackNormal;
	        if (_UnitOver != null)
	        {
        		x =  _UnitOver.GetComponent<UnitField>();
	        	if (x._NoMove != null && x._Atack != null)
	        		{ x._NoMove.SetActive(false);
		        	x._Atack.SetActive(false);}
	        }
	        
        }
    }

}
