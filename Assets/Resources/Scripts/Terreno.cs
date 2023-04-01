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
	public GameObject _LastUnitOver;
	public SceneVariables_Battle svb;
	
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
	public GameObject _VFXSpellActive;
	
	private SceneVariables_Battle.SVBLog _LogTemp = new SceneVariables_Battle.SVBLog();
	
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
	    _SelfTerreno = this.gameObject;
	    //if (_PlayerOwner && _HexType == "Summon"){ _PlayerOwnerID = SceneVariables_Battle.p1.ID; }
	    //else if (_PlayerOwner != true && _HexType == "Summon") { _PlayerOwnerID = SceneVariables_Battle.p2.ID; }
    }
	public void PlayAnim(string anim){
		switch(anim){
		case "Death": StartCoroutine(AnimDeath());break;
		case "Evolution": StartCoroutine(AnimEvolution());break;
		case "Heal": StartCoroutine(AnimHeal());break;
		case "Active": StartCoroutine(AnimActive());break;
		}
	}
	
	public IEnumerator AnimDeath(){
		_VFXDeath.SetActive(true);
		yield return new WaitForSecondsRealtime (1.4f);
		_VFXDeath.SetActive(false);
	}
	
	public IEnumerator AnimEvolution(){
		_VFXEvolution.SetActive(true);
		yield return new WaitForSecondsRealtime (1.4f);
		_VFXEvolution.SetActive(false);
	}
	
	public IEnumerator AnimHeal(){
		_VFXHeal.SetActive(true);
		yield return new WaitForSecondsRealtime (1.4f);
		_VFXHeal.SetActive(false);
	}
	
	public IEnumerator AnimActive(){
		_VFXActive.SetActive(true);
		yield return new WaitForSecondsRealtime (1.4f);
		_VFXActive.SetActive(false);
	}
	
    public void OnMouseDown()
	{
		var aud = FindObjectOfType<ManagerSound>();
		aud.audio.clip = aud.click;
		aud.audio.Play();
		CheckSpell();
    	svb._TerrenoAnterior = svb._LastTerreno;
    	svb._LastTerreno = _SelfTerreno;
        switch (_HexType) 
        {case "Normal" :
	        TryMove();
            break;
        case "Castelo":
	        TryMove();
            break;
        case "Summon":
	        //Se estiver movendo, se move, caso contrario tenta invocar,se foi o dono que clicou.
	        if (svb.p1.onMove && _OnRange && _CanMoveOver)
	        { MoveUnit();
	        	_LogTemp.Visual = "Movendo unit : " + svb._UnitToMove;
	        	_LogTemp.Detalhado = "Jogador: " +svb.atualTurnID + ", Começando mover a unit : " + svb._UnitToMove;
		        svb.DebugText(_LogTemp);}
	        else if ( _PlayerOwner && svb.p1.canSummon)
                {if (svb.atualTurnID == _PlayerOwnerID)
                { svb.p1.onSummon = true;
	                _LogTemp.Visual = "Escolha unidade para invocar...";
	                _LogTemp.Detalhado = "Jogador: " +svb.atualTurnID + ", Escolhendo a unidade para invocar";
	                svb.DebugText(_LogTemp);}
                else {_LogTemp.Visual = "Não pode invocar no turno inimigo";
	                _LogTemp.Detalhado = "Jogador: " +svb.atualTurnID + ", Tentou Invocar no turno inimigo";
	                svb.DebugText(_LogTemp);}
                }
	        else if (_PlayerOwner && svb.p1.canSummon != true)
	        {
	        	_LogTemp.Visual = "Ja invocou nesse turno";
		        _LogTemp.Detalhado = "Jogador: " +svb.atualTurnID + ", Tentou invocar, sem poder mais no turno";
		        svb.DebugText(_LogTemp);
                CancelMoveUnit();}
                else if ( _PlayerOwner )
                { CancelMoveUnit();
                	_LogTemp.Visual = "Local de invocaçao inimiga.";
	                _LogTemp.Detalhado = "Jogador: " +svb.atualTurnID + ", Clicou no local de invocaçao inimiga";
	                svb.DebugText(_LogTemp);} 
                else{ CancelMoveUnit();
	                _LogTemp.Visual = "Cancelando Movimento.";
	                _LogTemp.Detalhado = "Jogador: " +svb.atualTurnID + ", Cancelando Movimento";
	                svb.DebugText(_LogTemp);}
                break;
        case "Commander":
	        TryMove();
	        break;
        }
	}
	
	public void TryMove()
	{
		//Se move se ja iniciou o movimento e nao tem unit encima
	        if (svb.p1.onMove && _OnRange && _CanMoveOver) 
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
		var x =svb._UnitToMove.GetComponent<UnitField>();
		LeanTween.move(x.gameObject ,this.transform.position + new Vector3 (0f,0.8f,0f), 0.25f );
		t = FindObjectsOfType<Terreno>();
		foreach(Terreno d in t)
		{
			d._OnRange = false;
		}
		x._CanMove = false;
		svb.p1.onMove = false;
		EndTurn();
	}
	
	public void CancelMoveUnit()
	{
		t = FindObjectsOfType<Terreno>();
		foreach(Terreno d in t)
		{
			d._OnRange = false;
		}
		svb.p1.onMove = false;
		svb.p1.onSummon = false;
		
	}
	
	public void EndMoveUnit()
	{
		t = FindObjectsOfType<Terreno>();
		foreach(Terreno d in t)
		{
			d._OnRange = false;
		}
		svb.p1.onMove = false;
		svb.p1.onSummon = false;
		var x =svb._UnitToMove.GetComponent<UnitField>();
		x._CanMove = false;
		x.unit._Self._TimeToReset --;
		EndTurn();
	}
	
	public void EndTurn(){
		var u = FindObjectsOfType<UnitField>();
		var i = 0;
		foreach(UnitField un in u){if(un._CanMove&&un._isplayer) i++;}
		if (i==0&&!svb.p1.canSummon){FindObjectOfType<ManagerTurn>().NextTurn();}
	}
	
	private UnitField x;
    public void  HaveUnitOver()
    {
	    Debug.DrawRay(_ray.transform.position, _ray.transform.TransformDirection(Vector3.forward)* 1f, Color.green);
	    if (Physics.Raycast(_ray.transform.position, _ray.transform.TransformDirection(Vector3.forward), out RaycastHit hitinfo, 1f))
	    { 
		    var aud2 = FindObjectOfType<ManagerSound>();
		    if (_SpellData.Name != "" && _SpellActived == true && _UnitOver != _LastUnitOver)
		    {
		    	StartCoroutine(AnimActive());
		    	_VFXSpellActive.SetActive(true);
			    aud2.audio.clip = aud2.active;
			    aud2.audio.Play();
			    _LogTemp.Visual = _SpellData.Name + " Spell/Trap Re-Ativou!";
			    _LogTemp.Detalhado = "Jogador: " +svb.atualTurnID + ", Reativou a spell/trap : " + _SpellData.Name;
			    svb.DebugText(_LogTemp);
		    	_LastUnitOver = hitinfo.transform.gameObject;
		    	SpellEffects.SpellToActive(_SpellData.Script , _LastUnitOver.GetComponent<UnitField>());
		    }
		    if (_SpellData.Name != "" && _SpellActived != true){
		    	_SpellActived = true ;
		    	StartCoroutine(AnimActive());
		    	_VFXSpellActive.SetActive(true);
			    aud2.audio.clip = aud2.active;
			    aud2.audio.Play();
			    _LogTemp.Visual = _SpellData.Name + " Spell/Trap Ativou!";
			    _LogTemp.Detalhado = "Jogador: " +svb.atualTurnID + ", Ativou a Spell/Trap : " + _SpellData.Name;
			    svb.DebugText(_LogTemp);
		    	_LastUnitOver = hitinfo.transform.gameObject;
		    	SpellEffects.SpellToActive(_SpellData.Script , _LastUnitOver.GetComponent<UnitField>());
		    }
		    
	        //Debug.Log(hitinfo.collider);
            _HasUnit = true;
		    _UnitOver = hitinfo.transform.gameObject;
		    x =  _UnitOver.GetComponent<UnitField>();
		    unit = x.unit;
		    x._AtualHex = _SelfTerreno;
		    x._Visinhos = _Visinhos;
		    if (_PlayerOwner != x._isplayer && _HexType == "Castelo")
		    {
		    	Debug.Log(" Player "+ svb.atualTurn + "Win !!!");
		    }
		    
        }else
	    {
	        _LastUnitOver = null;
            _HasUnit = false;
	        _UnitOver = null;
	        unit = null ;
        }
    }
    
	//public void OnMouseOver() => CheckSpell();
    
	public void CheckSpell()
	{
		var debugText = GameObject.Find("DebugLine").GetComponent<TMP_Text>();
		if (_SpellActived){	FindObjectOfType<Manager_UI>()._TerrenoName = _SpellData.id != ""?_SpellData.Name:"Terreno : " + _HexType;
							FindObjectOfType<Manager_UI>()._DescriptionTxt.text = _SpellData.id!=""?_SpellData.Description:"Um Terreno normal sem nada especial.";}
		else{	FindObjectOfType<Manager_UI>()._TerrenoName = "Terreno : " + _HexType;
				FindObjectOfType<Manager_UI>()._DescriptionTxt.text ="Um Terreno normal sem nada especial.";}
	}
    
    
	public void UpdateMaterial()
	{
		if (svb.p1.onSummon && _HexType == "Summon" && svb._LastTerreno == this.gameObject)
		{ _VFXSummon.SetActive(true);}
		else {_VFXSummon.SetActive(false);}
		if (_PlayerOwner)
		{_PlayerOwnerID = svb.playerID;}
		else{ _PlayerOwnerID = svb.enemyID;}
        Renderer rend;
        if (_OnRange)
        {
            if (_HasUnit)
            {
                rend = GetComponent<Renderer>();
	            rend.sharedMaterials = _PackOcupado;
	            if (_UnitOver != null)
	            {x =  _UnitOver.GetComponent<UnitField>();
		            if (svb.p1.onMove && x._NoMove != null && x._Atack != null)
			           { if (x._Id == svb.atualTurnID)
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
