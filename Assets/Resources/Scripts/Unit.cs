using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.IO;
using UnityEditor;
using Sirenix.OdinInspector;

public class Unit : MonoBehaviour
{
	[Title("Unit OBJ e Valores")]
	public UnitGameSOBJ _Self ;
    public string _UnitOnUI ;
	public GameObject _UnitOnField;
	public bool _OnField = false ;
	public Terreno _Terreno;
    public bool _OnEnfermary = false;
    public RawImage _cardImage;
    public RawImage _cardBG;
    public RawImage _cardEffect1;
    public RawImage _cardEffect2;
    public RawImage _cardStatus;
    public TMP_Text _vATK;
    public TMP_Text _vDEF;
    public TMP_Text _vLife;
	public Material _MaterialType;
    public Button _SelfButton;
    public GameObject _Off;
    public GameObject _Enfermary;
    public Color _PlayerColor;
    public Color _EnemyColor;
    public Color _transparent;
	public Color _boost;
	public Color _deboost;
	public Animator _CardBGAnim;
    public GameObject _Summon;
	public GameObject _UnitPrefab;
	public TMP_Text _vModATK;
	public TMP_Text _vModDEF;
	public TMP_Text _vModLife;
    
	[Title("Owner infos")]
	public string _OwnerID;
	public bool _isPlayer = true ;
	public int _slotDeck;
	public Zyan.TimeUnits Slot;
	
	[Title("Visualizaçao Tela Inicial")]
	public bool telaInicial = false;
	public PlayerInventaryOBJ player;
	public SceneVariables_Battle svb;
	public TMP_Text debugTxt;
    
	void Start(){ 
		svb.debugText = debugTxt;
		_Self._Self = this;
		_Self.StartDuel();
		StartCoroutine(UpdateRegular());
	}
    
	private void FixedUpdate()
	{
		if (_Self._Index != -1) { _Self.UpdateCard(); }
		if ( _Self._TotalLife <= 0 ) { _Self.SendToEnfermary();}
		if ( _UnitOnField != null) {_OnField = true;} else {_OnField = false;}
		if (_Self._TimeToReset == 0 ){ _Self._ResetMod = true;}
		if (_Self._ResetMod){_Self.ResetMod();}
	}
    
	public IEnumerator UpdateRegular(){
		yield return new WaitForSecondsRealtime (1.5f);
		_Self.UpdateCard();
		StartCoroutine(UpdateRegular());
	}
   
	public void OnClick()
	{
		if (telaInicial){
			FindObjectOfType<UnitView>().atualSlot = Slot;
			FindObjectOfType<UnitView>().UpdateBigUnit(1);
			FindObjectOfType<UnitView>().evos[0].isOn = true;
		}
		else{
		var aud = FindObjectOfType<ManagerSound>();
		aud.audio.clip = aud.clickUI;
		aud.audio.Play();
		var z = FindObjectOfType<Manager_UI>();
		z._TerrenoName = "";
			z.UpdateUI(this);
		if (_isPlayer)
		{if (svb.p1.onSummon && svb.p1.canSummon)
			{ if (_OnField != true){FinishSummon();}}}
		}
	}
	
	public void FinishSummon() {_Self.FinishSummon();}
}
