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
	public SceneVariables_Battle SVB;
	
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
		
		SVB = FindObjectOfType<SceneVariables_Battle>();
		_originalUnit = SceneVariables_Battle._LastSummonUnit.gameObject;
	    unit = _originalUnit.GetComponent<Unit>();
	    self = this.gameObject ;
	    unit._UnitOnField = self;
		_isplayer = unit._isPlayer;
		_Id = unit._id;
    }

	// Update is called once per frame
    void Update()
    {
	    atk.text	= unit._vATK.text;
	    def.text	= unit._vDEF.text;
	    life.text	= unit._vLife.text;
	    UpMaterial();
	    if (unit._OnStatus == "No") 
	    {	onAnim = false;
		    _StatusImg.gameObject.SetActive(false);}
	    if (unit._OnStatus!= "No" && unit._OnStatusTurn > 0 && onAnim != true)
	    {	_StatusImg.gameObject.SetActive(true);
		    AnimStatus(unit._OnStatus);
		    SVB.DebugText("On status : "+ unit._OnStatus);
		    onAnim = true;}
    }
    
	void OnDestroy(){
		var aud = FindObjectOfType<ManagerSound>();
		aud.audio.clip = aud.explosao;
		aud.audio.Play();
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
		if (unit._OnStatusTurn > 0) _StatusImg.sprite = _AnimDice[unit._OnStatusTurn-1];
		yield return new WaitForSecondsRealtime (1f);
		onAnim = false;
	}
    
	public void OnMouseDown()
	{	
		var z = FindObjectOfType<Manager_UI>();
		if (_AtualHex.GetComponent<Terreno>()._SpellData.id != "" && _AtualHex.GetComponent<Terreno>()._SpellActived)
		{ z._TerrenoName = _AtualHex.GetComponent<Terreno>()._SpellData.Name;}
		else{z._TerrenoName ="Terreno : " + _AtualHex.GetComponent<Terreno>()._HexType;}
		z.UpdateUI(_originalUnit);
		SceneVariables_Battle._TerrenoAnterior = SceneVariables_Battle._LastTerreno;
		SceneVariables_Battle._LastTerreno = _AtualHex;
		SVB.DebugText("Click : " + unit._name);
		switch ( SceneVariables_Battle.atualTurn)
		{
		case "P1":
			if (_isplayer){
				if (_CanMove && unit._OnStatus != "Sleep")
					{SceneVariables_Battle.p1.onMove = true;
					SceneVariables_Battle._UnitToMove = this;
					Debug.Log("Pronto para mover :" + unit._name);
					foreach ( GameObject x in _Visinhos){
						if (x != null)
						{var y = x.GetComponent<Terreno>();
							y._OnRange = true;}}}
				else {SVB.DebugText("Nao pode mover : " + unit._name);}}
			else {
				SVB.DebugText("indo pra batalha :" + this);
				if (_AtualHex.GetComponent<Terreno>()._UnitOver.GetComponent<UnitField>() != SceneVariables_Battle._UnitToMove)
				{ FindObjectOfType<Manager_Battle>().Batalha(SceneVariables_Battle._UnitToMove._originalUnit, _originalUnit );}
				else
				{SVB.DebugText("Não é seu turno !");}}
		break;
		case "P2":
			if (_isplayer != true){
				if (_CanMove)
				{SceneVariables_Battle.p2.onMove = true;
					SceneVariables_Battle._UnitToMove = this;
					SVB.DebugText("Pronto para mover : " + unit._name);
					foreach ( GameObject x in _Visinhos){
						if (x != null)
						{var y = x.GetComponent<Terreno>();
							y._OnRange = true;}}}
				else {SVB.DebugText("Nao pode mover : " + unit._name);}}
			else {SVB.DebugText("Não é seu turno !");}
			break;
	
		}
	}
}
