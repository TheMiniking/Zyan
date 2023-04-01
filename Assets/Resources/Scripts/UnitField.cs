using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Sirenix.OdinInspector;

public class UnitField : MonoBehaviour
{
	
	[Title("Dados Adicionais :")]
	public GameObject _AtualHex;
	public GameObject[] _Visinhos;
	public bool _CanMove = true;
	public bool _isplayer;
	public string _Id;
	
	[Title("Componentes :")]
    public Unit unit;
    public GameObject _originalUnit;
    public GameObject self;
	public GameObject _Atack;
	public GameObject _NoMove;
    public TMP_Text atk;
    public TMP_Text def;
	public TMP_Text life;
	public Material[] mat;
	public Material _MatNoMove;
	public Material _MatCanMove;
	public bool _BoostMoviment;
	public string _SpecialStatus;
	public SpriteRenderer _StatusImg;
	public SceneVariables_Battle svb;
	
	private SceneVariables_Battle.SVBLog _LogTemp = new SceneVariables_Battle.SVBLog();
	
	[Title("Animation :")]
	[PreviewField]public Sprite[] _AnimPoison;
	[PreviewField]public Sprite[] _AnimBurn; 
	[PreviewField]public Sprite[] _AnimFadige; 
	[PreviewField]public Sprite[] _AnimSleep; 
	[PreviewField]public Sprite[] _AnimParalize; 
	[PreviewField]public Sprite[] _AnimNumbers; 
	[PreviewField]public Sprite[] _AnimBalao; 
	[PreviewField]public Sprite[] _AnimDice; 
	bool onAnim = false;
    

    // Start is called before the first frame update
    void Start()
	{	
		_originalUnit = svb._LastSummonUnit.gameObject;
	    self = this.gameObject ;
	    unit._UnitOnField = self;
		_isplayer = unit._isPlayer;
		_Id = unit._Self._id;
    }

	// Update is called once per frame
    void Update()
    {
	    atk.text	= unit._vATK.text;
	    def.text	= unit._vDEF.text;
	    life.text	= unit._vLife.text;
	    unit._Terreno = _AtualHex.GetComponent<Terreno>();
	    UpMaterial();
	    if (unit._Self._OnStatus == "No") 
	    {	onAnim = false;
		    _StatusImg.gameObject.SetActive(false);}
	    if (unit._Self._OnStatus!= "No" && unit._Self._OnStatusTurn > 0 && onAnim != true)
	    {	_StatusImg.gameObject.SetActive(true);
		    AnimStatus(unit._Self._OnStatus);
		    onAnim = true;
		    if(unit._Self._status== "Sleep") _CanMove= false;
	    }
    }
    
	void OnDestroy(){
		var aud = FindObjectOfType<ManagerSound>();
		aud.audio.clip = aud.explosao;
		aud.audio.Play();
		unit._OnField = false;
		unit._Terreno = null;
		unit._Self._UnitOnField = null;
	}
    
	public void UpMaterial()
	{
	    if (_CanMove){mat.SetValue(_MatCanMove, 0);}
	    else		 {mat.SetValue(_MatNoMove, 0);}
        mat.SetValue(unit._MaterialType, 1);
	    var rend = GetComponent<Renderer>();
	    rend.sharedMaterials = mat;
	}
    
	/*
	// Remoçao do pasar o mouse - mudar o metodo
	public void OnMouseOver()
	{
		
	}
	*/
    
	public void AnimStatus( string status){
		switch (status){
		case "Poison" :
			StartCoroutine(Animation(_AnimPoison));
			break;
		case "Burn" :
			StartCoroutine(Animation(_AnimBurn));
			break;
		case "Paralize" :
			StartCoroutine(Animation(_AnimParalize));
			break;
		case "Fadige" :
			StartCoroutine(Animation(_AnimFadige));
			break;
		case "Sleep" :
			StartCoroutine(Animation(_AnimSleep));
			break;
		}
	}
    
    
	public IEnumerator Animation(Sprite[] status){
		_StatusImg.sprite = status[0];
		yield return new WaitForSecondsRealtime (0.5f);
		_StatusImg.sprite = status[1];
		yield return new WaitForSecondsRealtime (0.5f);
		_StatusImg.sprite = status[0];
		yield return new WaitForSecondsRealtime (0.5f);
		_StatusImg.sprite = status[1];
		yield return new WaitForSecondsRealtime (0.5f);
		if (unit._Self._OnStatusTurn > 0) _StatusImg.sprite = _AnimDice[unit._Self._OnStatusTurn-1];
		yield return new WaitForSecondsRealtime (1f);
		onAnim = false;
	}
    
	public void OnMouseDown()
	{	
		SendToUI();
		svb._TerrenoAnterior = svb._LastTerreno;
		svb._LastTerreno = _AtualHex;
		_LogTemp.Visual = "Click : " + unit._Self._name;
		_LogTemp.Detalhado = "Jogador: " +svb.atualTurnID + ", Clicou na unit : " + unit.name;
		svb.DebugText(_LogTemp);
		switch ( svb.atualTurn)
		{
		case "P1":
			if (_isplayer){
				if (_CanMove && unit._Self._OnStatus != "Sleep")
					{svb.p1.onMove = true;
					svb._UnitToMove = this;
					Debug.Log("Pronto para mover :" + unit._Self._name);
					foreach ( GameObject x in _Visinhos){
						if (x != null){var y = x.GetComponent<Terreno>()._OnRange = true;}}}
				else {
					_LogTemp.Visual = "Nao pode mover : " + unit._Self._name;
					_LogTemp.Detalhado = "Jogador: " +svb.atualTurnID + ", Clicou na unit : " + unit.name;
					svb.DebugText(_LogTemp);}}
			else {
				_LogTemp.Visual = "Indo pra batalha :" + unit.name;
				_LogTemp.Detalhado = "Jogador: " +svb.atualTurnID + ", Indo para batalha : " + unit.name;
				svb.DebugText(_LogTemp);
				var ah = _AtualHex.GetComponent<Terreno>();
				if (ah._UnitOver.GetComponent<UnitField>() != svb._UnitToMove && ah._OnRange)
				{ FindObjectOfType<Manager_Battle>().Batalha(svb._UnitToMove._originalUnit, _originalUnit );}
				else
				{
					_LogTemp.Visual = "Não é seu turno !" ;
					_LogTemp.Detalhado = "Jogador: " +svb.atualTurnID + ", Tentou Jogar na unit do inimigo ";
					svb.DebugText(_LogTemp);}}
		break;
		case "P2":
			if (_isplayer != true){
				if (_CanMove && unit._Self._OnStatus != "Sleep")
				{svb.p2.onMove = true;
					svb._UnitToMove = this;
					_LogTemp.Visual = "Pronto para mover : " + unit._Self._name;
					_LogTemp.Detalhado = "Jogador: " +svb.atualTurnID + ", Pronto para mover : " + unit.name;
					svb.DebugText(_LogTemp);
					foreach ( GameObject x in _Visinhos){
						if (x != null)
						{var y = x.GetComponent<Terreno>();
							y._OnRange = true;}}}
				else {
					_LogTemp.Visual = "Nao pode mover : " + unit._Self._name;
					_LogTemp.Detalhado = "Jogador: " +svb.atualTurnID + ", Tentou mover unit inimiga : " + unit.name;
					svb.DebugText(_LogTemp);}}
			else {
				_LogTemp.Visual = "Não é seu turno !";
				_LogTemp.Detalhado = "Jogador: " +svb.atualTurnID + ", Clicou na unit, sem ser dono : " + unit.name;
				svb.DebugText(_LogTemp);}
			break;
	
		}
	}
	
	public void SendToUI(){
		var z = FindObjectOfType<Manager_UI>();
		if (_AtualHex.GetComponent<Terreno>()._SpellData.id != "" && _AtualHex.GetComponent<Terreno>()._SpellActived)
		{ z._TerrenoName = _AtualHex.GetComponent<Terreno>()._SpellData.Name;
			z._DescriptionTxt.text = svb._LastTerreno.GetComponent<Terreno>()._SpellData.Name!=null?
				svb._LastTerreno.GetComponent<Terreno>()._SpellData.Description:"Um Terreno normal sem nada especial.";}
		else{z._TerrenoName ="Terreno : " + _AtualHex.GetComponent<Terreno>()._HexType;}
		z.UpdateUI(_originalUnit.GetComponent<Unit>());
	}
	
}
