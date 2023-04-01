﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine.UI;


[CreateAssetMenu (fileName = "Unit_Game", menuName = "Zyan Assets/Create Unit Game")]
public class UnitGameSOBJ : ScriptableObject
{
	[Title("Owner infos")]
	public string _OwnerID;
	public bool _isPlayer = true ;
	public int _slotDeck;
	public Zyan.TimeUnits Slot;
	
	[Title("Card Info Final/Show")]
	public int _Index = -1 ;
	public string _id;
	public string _name;
	public string _type;
	public string _rank;
	public string _element;
	public string _special;
	public int _atk;
	public int _def;
	public int _life;
	public string _status;
	public string _effect1;
	public string _effect2;
	public string _evolution;
	public string _evolCust1;
	public string _evolCust2;
	public string _evolCust3;
	public string _evol1;
	public string _evol2;
	public string _evol3;
	public string _rarityUp;
	public string _rarity;
    
	[Title("Original Valor")]
	public UnitObj selfObjUnit; // Scriptable Obj Original
	public int _originalAtk;
	public int _originalDef;
	public int _originalLife;
	public int _TotallAtk;
	public int _TotalDef;
	public int _TotalLife;
	public int _ModAtk;
	public int _ModDef;
	public int _ModLife;
    
	[Title("Boost/Deboost")]
	public Zyan.EquipClass _Equip;
	public string _OnStatus = "No";
	public int _OnStatusTurn = 0;
	public int _boostATK;
	public int _boostDEF;
	public int _boostLife;
	public int _DeboostATK;
	public int _DeboostDEF;
	public int _DeboostLife;
	public int _ProvisoryModATK;
	public int _ProvisoryModDEF;
	public int _PermanentModATK;
	public int _PermanentModDEF;
	public int _TimeToReset = -1;
	public bool _ResetMod;
	public Zyan.EvolutionCheck _EvolutionCheck;
    
	[Title("Unit OBJ e Valores")]
	public Unit _Self ;				//Script Usado na UI
	public string _UnitOnUI ;		//Nome na UI
	public GameObject _UnitOnField;	//Unit Obj usado no campo
	public bool _OnEnfermary = false;
	public GameObject _UnitPrefab;
    
	[Title("Visualizaçao Tela Inicial")]
	public bool telaInicial = false;
	public PlayerInventaryOBJ player;
	public SceneVariables_Battle svb;
	
	void Start()=>StartDuel();
	
	[Button]
	public void StartDuel()
	{
		OriginalStatus();
		_UnitOnUI = _Self.gameObject.name;
		_UnitOnField = null;
		//_Self = this.gameObject;
		if (_isPlayer){
			switch (_slotDeck)
			{
			case 1 :
				Slot = svb._PlayerDeck.Slot1;
				break;
			case 2 :
				Slot = svb._PlayerDeck.Slot2;
				break;
			case 3 :
				Slot = svb._PlayerDeck.Slot3;
				break;
			case 4 :
				Slot = svb._PlayerDeck.Slot4;
				break;
			case 5 :
				Slot = svb._PlayerDeck.Slot5;
				break;
			}
			_id = Slot.Civil;
			selfObjUnit = Resources.Load<UnitObj>("Scripts/Unit/"+_id);
		}else{
			switch (_slotDeck)
			{
			case 1 :
				Slot = svb._EnemyDeck.Slot1;
				break;
			case 2 :
				Slot = svb._EnemyDeck.Slot2;
				break;
			case 3 :
				Slot = svb._EnemyDeck.Slot3;
				break;
			case 4 :
				Slot = svb._EnemyDeck.Slot4;
				break;
			case 5 :
				Slot = svb._EnemyDeck.Slot5;
				break;
			}
			_id = Slot.Civil;
			selfObjUnit = Resources.Load<UnitObj>("Scripts/Unit/"+_id);
		}
		LoadCardDATA();
	}
    
	public void VisualUpdate(string id){
		_id = id;
		selfObjUnit = Resources.Load<UnitObj>("Scripts/Unit/"+_id);
		LoadCardDATA();
	}
    
	public void ResetMod(){
		_ProvisoryModATK = 0;
		_ProvisoryModDEF = 0;
		_UnitOnField.GetComponent<UnitField>()._SpecialStatus = "";
		_ResetMod = false;
		_TimeToReset = -1;
	}
	
	public void OriginalStatus(){
		selfObjUnit = null;
		ResetAllStatus();
		(_originalAtk , _originalDef , _originalLife,_TotallAtk, _TotalDef , _TotalLife) = (0,0,0,0,0,0);
		Slot = new Zyan.TimeUnits();
		_Self._OnField =false;
	}
	
	public void UpdateCard()
	{    
		VerifyStatus();
		VerifyUIColor();
		VerifyResources();
		VerifyEquip();
		VerifyOnFieldEnfermary();
		if (!telaInicial ){
			if(svb.p1.onSummon)
			{	if (_Self._OnField == false && _isPlayer == true){_Self._Summon.gameObject.SetActive(true);}}
			else { _Self._Summon.gameObject.SetActive(false); }}
		else {var n = GameObject.Find("NamePN");
			if (n != null){
				n.GetComponent<TMP_Text>().text = _name;
				FindObjectOfType<InfoView>().LoadTxt(_rank +" / "+ _element +" / "+ _type,_evolution);}
		}
	}
	
	public void LoadCardDATA()
	{
		if (selfObjUnit != null)
		{
			var d =selfObjUnit;
			_id = d.id;
			_name = d.Name;
			_type = d.Type;
			_rank = d.Rank;
			_element = d.Element;
			_special = d.Special;
			_atk = d.ATK;
			_def = d.DEF;
			_life= d.Life;
			_status = d.Status;
			_effect1 = d.Effect1;
			_effect2 = d.Effect2;
			_evolution = d.Evolution;
			_evolCust1 = d.EvolCust1;
			_evolCust2 = d.EvolCust2;
			_evolCust3 = d.EvolCust3;
			_evol1 = d.Evol1;
			_evol2 = d.Evol2;
			_evol3 = d.Evol3;
			_rarityUp = d.RarityUp;
			_rarity = d.Rarity;
			_originalAtk = d.ATK;
			_originalDef = d.DEF;
			_originalLife= d.Life;
			_Index = 0;
		}
		VerifyEquip();
		UpdateCard();
	}
	
	private void VerifyOnFieldEnfermary()
	{
		if (_Self._OnField )
		{	_Self._SelfButton.interactable = false ;
			_Self._Off.gameObject.SetActive(true);
			var o = _UnitOnField.GetComponent<UnitField>();
			var t = o._AtualHex;
			_Self._Terreno = t.GetComponent<Terreno>();}
		else{ if (_TotalLife >= Mathf.RoundToInt((_originalLife + _boostLife - _DeboostLife) / 2) )
		{	_Self._SelfButton.interactable = true;
			_Self._Off.gameObject.SetActive(false);}
		else{_Self._SelfButton.interactable = false;
			_Self._Off.gameObject.SetActive(true); }}
		if ( _TotalLife >= _originalLife + _boostLife - _DeboostLife )
		{   _Self._CardBGAnim.SetBool("Enfermary", false) ;
			_OnEnfermary = false;
			_Self._Enfermary.SetActive(false);}
		else {if (_Self._OnField)
		{	_OnEnfermary = false;
			_Self._CardBGAnim.SetBool("Enfermary", false);
			_Self._Enfermary.SetActive(false);
		}else if (_TotalLife < _originalLife + _boostLife - _DeboostLife)
		{	_OnEnfermary = true;
			_Self._CardBGAnim.SetBool("Enfermary", true);
			_Self._Enfermary.SetActive(true);
		}
		else { _OnEnfermary = false; 
			_Self._CardBGAnim.SetBool("Enfermary", false);
			_Self._Enfermary.SetActive(false);}}
	}
	
	private void VerifyStatus()
	{
		if (_OnStatus != "No")
		{
			switch (_OnStatus)
			{
			case "Poison":
				_DeboostATK = 0;
				_DeboostDEF = 0;
				break;
			case "Burn" :
				_DeboostATK = 1;
				_DeboostDEF = 0;
				break;
			case "Sleep":
				_DeboostATK = 0;
				_DeboostDEF = 0;
				break;
			case "Paralize":
				_DeboostATK = 0;
				_DeboostDEF = 2;
				break;
			case "Fadige":
				_DeboostATK = 0;
				_DeboostDEF = 0;
				break;
			}
		}
	}
	
	private void VerifyUIColor()
	{
		_TotallAtk = _atk + _boostATK - _DeboostATK + _ProvisoryModATK + _PermanentModATK;
		_TotalDef = _def + _boostDEF - _DeboostDEF+ _ProvisoryModDEF + _PermanentModDEF;
		_TotalLife =_life + _boostLife - _DeboostLife;
		_ModAtk = _boostATK - _DeboostATK + _ProvisoryModATK + _PermanentModATK;
		_ModDef = _boostDEF - _DeboostDEF+ _ProvisoryModDEF + _PermanentModDEF;
		_ModLife = _boostLife - _DeboostLife;
		if (_TotallAtk <= 0) _TotallAtk = 0;
		if (_TotalDef <= 0) _TotalDef = 0;
		// Muda cor das letras se estiver maior ou menor que o original
		if (_originalAtk < _TotallAtk)				{_Self._vATK.color = _Self._boost;}
		else if (_originalAtk == _TotallAtk)		{_Self._vATK.color = Color.white;}
		else {_Self._vATK.color = _Self._deboost;}
		_Self._vATK.text = "" + _TotallAtk;
		if (_originalDef < _TotalDef)				{_Self._vDEF.color = _Self._boost;}
		else if (_originalDef == _TotalDef)			{_Self._vDEF.color = Color.white;}
		else {_Self._vDEF.color = _Self._deboost;}
		_Self._vDEF.text = "" + _TotalDef;
		if (_originalLife >_TotalLife)				{_Self._vLife.color = _Self._boost;}
		else if (_originalLife == _TotalLife)		{_Self._vLife.color = Color.white;}
		else{_Self._vLife.color = _Self._boost;}
		_Self._vLife.text = "" + _TotalLife;
		
	}
	
	private void VerifyResources()
	{
		_Self._cardBG.texture = Resources.Load<Texture2D>("Imagens/UI/" + _type);
		_Self._cardEffect1.texture = Resources.Load<Texture2D>("Imagens/icons/" + _effect1);
		_Self._cardEffect2.texture = Resources.Load<Texture2D>("Imagens/icons/" + _effect2);
		_Self._cardStatus.texture = Resources.Load<Texture2D>("Imagens/icons/" + _status);
		if ( _isPlayer)
		{    
			_Self._MaterialType = Resources.Load<Material>("Unit Tipos/" + _type);
			_Self._cardBG.color = _Self._PlayerColor;
			_Self._CardBGAnim.SetBool("Enemy", false );
		}
		else 
		{    _Self._MaterialType = Resources.Load<Material>("Unit Tipos/" + _type +" 2");
			_Self._cardBG.color = _Self._EnemyColor;
			_Self._CardBGAnim.SetBool("Enemy", true );
		}
		if (Resources.Load<Texture2D>("Pixel Units/" + _id) != null)
		{ _Self._cardImage.texture = Resources.Load<Texture2D>("Pixel Units/" + _id); }
		else { _Self._cardImage.color = _Self._transparent; }
	}
	
	public void VerifyEquip()
	{
		if (Slot.Equip != "")
		{
			var e = Resources.Load<EquipObj>("Scripts/Equip/"+Slot.Equip);
			_Equip = e.Card;
			if (_Equip.Status != "None")
			{_status = _Equip.Status;}
			if (_Equip.ExclusiveType != "None")
			{
				if (_Equip.ExclusiveType == _type)
				{GetModification();}
				else if (_Equip.TypeBonus1 == _type)
				{GetModification();}
				else if (_Equip.TypeBonus2 == _type)
				{GetModification();}
				else if (_Equip.TypeBonus3 == _type)
				{GetModification();}
				else if (_Equip.TypeBonus4 == _type)
				{GetModification();}
				else if (_Equip.TypeBonus5 == _type)
				{GetModification();}
				else {GetPartialModification();}
			}
		}
	}
	
	public void GetModification()
	{
		
		switch (_Equip.BoostType){
		case "ATK":
			_boostATK = int.Parse(_Equip.BoostQnt);
			_boostDEF = 0;
			_boostLife = 0;
			break;
		case "DEF":
			_boostDEF = int.Parse(_Equip.BoostQnt);
			_boostATK = 0;
			_boostLife = 0;
			break;
		case "Life":
			_boostLife = int.Parse(_Equip.BoostQnt);
			_boostDEF = 0;
			_boostATK = 0;
			break;}
		switch (_Equip.DeboostBool){
		case "None" :
			_DeboostDEF = 0;
			_DeboostATK = 0;
			_DeboostLife =0;
			break;
		default: 
			var t = _Equip.DeboostBool.Split(",");
			if (t.Length == 2)
			{
				switch (t[0])
				{
				case "ATK":
					_DeboostDEF = 0;
					_DeboostATK = int.Parse(t[1]);
					_DeboostLife =0;
					break;
				case "DEF":
					_DeboostDEF = int.Parse(t[1]);
					_DeboostATK = 0;
					_DeboostLife =0;
					break;
				case "Life":
					_DeboostDEF = 0;
					_DeboostATK = 0;
					_DeboostLife =int.Parse(t[1]);
					break;
				}
			}if (t.Length == 4)
			{
				switch (t[0])
				{
				case "ATK":
					_DeboostDEF = 0;
					_DeboostATK = int.Parse(t[1]);
					_DeboostLife =0;
					break;
				case "DEF":
					_DeboostDEF = int.Parse(t[1]);
					_DeboostATK = 0;
					_DeboostLife =0;
					break;
				case "Life":
					_DeboostDEF = 0;
					_DeboostATK = 0;
					_DeboostLife =int.Parse(t[1]);
					break;
				}
				switch (t[2])
				{
				case "ATK":
					_DeboostATK = int.Parse(t[3]);
					break;
				case "DEF":
					_DeboostDEF = int.Parse(t[3]);
					break;
				case "Life":
					_DeboostLife =int.Parse(t[3]);
					break;
				}
			}
			break;
		}
	}
	
	public void GetPartialModification()
	{
		_DeboostDEF = 0;
		_DeboostATK = 0;
		_DeboostLife = 0;
		_boostDEF = 0;
		_boostATK = 0;
		_boostLife =0;
	}
	
	public void TurnEnfermary()
	{    
		if (_OnStatus == "Poison" || _OnStatus == "Burn") _life =_life-1;
		if (_OnStatusTurn> 0) _OnStatusTurn = _OnStatusTurn -1;
		if (_OnStatusTurn == 0 ) _OnStatus = "No";
		if (_life > _originalLife )
		{ if (_effect1 != "Eternal Life" && _effect2 != "Eternal Life")
		{_life = _originalLife;} }
		if (_life < 0)
		{ _life = 0; }
		if ( _OnEnfermary)
		{ _life++;}
	}
    
	public void SendToEnfermary()
	{
		_OnEnfermary = true;
		_life = 0;
		_Self._Terreno.PlayAnim("Death");
		GameObject.Destroy( _UnitOnField);
		_UnitOnField = null;
		_Self._OnField = false;
		_OnStatus = "No";
		_OnStatusTurn = 0;
	}
	
	private Vector3 x;
	public void FinishSummon()
	{
		x = svb._LastTerreno.transform.position ;
		x = x + new Vector3(0f,0.8f,0f);
		var y = svb._LastTerreno.transform.rotation;
		Quaternion q = new Quaternion(-90f,-90f,180f,0f); 
		svb._LastSummonUnit = _Self;
		_UnitOnField = Instantiate(_UnitPrefab, x, y );
		_UnitOnField.gameObject.name = _name;
		var uni = _UnitOnField.GetComponent<UnitField>();
		uni.unit = _Self;
		_Self._OnField = true;
		svb.p1.onSummon = false;
		svb.p1.canSummon = false;
	}
	
	[Button]
	public void TryEvolution()
	{
		Debug.Log("Tentando evoluir. " + _rank);
		switch (_rank){
		case "Civil":
			if (Slot.Soldier != "")
			{Evolution();}
			else{Debug.Log("Não pode mais evoluir.");}
			break;
		case "Soldier":
			if (Slot.Combatant != "")
			{Evolution();}
			else{Debug.Log("Não pode mais evoluir.");}
			break;
		case "Combatant":
			if (Slot.General != "")
			{Evolution();}
			else{Debug.Log("Não pode mais evoluir.");}
			break;
		case "General":
			if (Slot.King != "")
			{Evolution();}
			else{Debug.Log("Não pode mais evoluir.");}
			break;
		case "King":
			if (Slot.God != "")
			{Evolution();}
			else{Debug.Log("Não pode mais evoluir.");}
			break;
		case "Queen":
			if (Slot.God != "")
			{Evolution();}
			else{Debug.Log("Não pode mais evoluir.");}
			break;
		case "Ruler":
			if (Slot.God != "")
			{Evolution();}
			else{Debug.Log("Não pode mais evoluir.");}
			break;
		case "God":
			Debug.Log("Não pode mais evoluir.");
			break;
		case "Anti-God":
			Debug.Log("Não pode mais evoluir.");
			break;
		case "Ultimate":
			Debug.Log("Não pode mais evoluir.");
			break;
		}
	}
	
	private UnitObj nextEvo;
	[Button]
	public void TryEvolutionType( )
	{
		Debug.Log("Tentando evoluir. " + _rank);
		switch (_rank){
		case "Civil":
			if (Slot.Soldier != "")
			{nextEvo  = Resources.Load<UnitObj>("Scripts/Unit/"+Slot.Soldier);
				if (_type == nextEvo.Type)Evolution();
				else {Debug.Log("Não tem o tipo para evoluir.");}}
			else{Debug.Log("Não pode mais evoluir.");}
			break;
		case "Soldier":
			if (Slot.Combatant != "")
			{nextEvo  = Resources.Load<UnitObj>("Scripts/Unit/"+Slot.Combatant);
				if (_type == nextEvo.Type)Evolution();
				else {Debug.Log("Não tem o tipo para evoluir.");}}
			else{Debug.Log("Não pode mais evoluir.");}
			break;
		case "Combatant":
			if (Slot.General != "")
			{nextEvo  = Resources.Load<UnitObj>("Scripts/Unit/"+Slot.General);
				if (_type == nextEvo.Type)Evolution();
				else {Debug.Log("Não tem o tipo para evoluir.");}}
			else{Debug.Log("Não pode mais evoluir.");}
			break;
		case "General":
			if (Slot.King != "")
			{nextEvo  = Resources.Load<UnitObj>("Scripts/Unit/"+Slot.King);
				if (_type == nextEvo.Type)Evolution();
				else {Debug.Log("Não tem o tipo para evoluir.");}}
			else{Debug.Log("Não pode mais evoluir.");}
			break;
		case "King":
			if (Slot.God != "")
			{nextEvo  = Resources.Load<UnitObj>("Scripts/Unit/"+Slot.God);
				if (_type == nextEvo.Type)Evolution();
				else {Debug.Log("Não tem o tipo para evoluir.");}}
			else{Debug.Log("Não pode mais evoluir.");}
			break;
		case "Queen":
			if (Slot.God != "")
			{nextEvo  = Resources.Load<UnitObj>("Scripts/Unit/"+Slot.God);
				if (_type == nextEvo.Type)Evolution();
				else {Debug.Log("Não tem o tipo para evoluir.");}}
			else{Debug.Log("Não pode mais evoluir.");}
			break;
		case "Ruler":
			if (Slot.God != "")
			{nextEvo  = Resources.Load<UnitObj>("Scripts/Unit/"+Slot.God);
				if (_type == nextEvo.Type)Evolution();
				else {Debug.Log("Não tem o tipo para evoluir.");}}
			else{Debug.Log("Não pode mais evoluir.");}
			break;
		case "God":
			Debug.Log("Não pode mais evoluir.");
			break;
		case "Anti-God":
			Debug.Log("Não pode mais evoluir.");
			break;
		case "Ultimate":
			Debug.Log("Não pode mais evoluir.");
			break;
		}
	}
	
	[Button]
	public void TryEvolutionElement( )
	{
		Debug.Log("Tentando evoluir. " + _rank);
		switch (_rank){
		case "Civil":
			if (Slot.Soldier != "")
			{nextEvo  = Resources.Load<UnitObj>("Scripts/Unit/"+Slot.Soldier);
				if (_element == nextEvo.Element)Evolution();
				else {Debug.Log("Não tem o Elemento para evoluir.");}}
			else{Debug.Log("Não pode mais evoluir.");}
			break;
		case "Soldier":
			if (Slot.Combatant != "")
			{nextEvo  = Resources.Load<UnitObj>("Scripts/Unit/"+Slot.Combatant);
				if (_element == nextEvo.Element)Evolution();
				else {Debug.Log("Não tem o Elemento para evoluir.");}}
			else{Debug.Log("Não pode mais evoluir.");}
			break;
		case "Combatant":
			if (Slot.General != "")
			{nextEvo  = Resources.Load<UnitObj>("Scripts/Unit/"+Slot.General);
				if (_element == nextEvo.Element)Evolution();
				else {Debug.Log("Não tem o Elemento para evoluir.");}}
			else{Debug.Log("Não pode mais evoluir.");}
			break;
		case "General":
			if (Slot.King != "")
			{nextEvo  = Resources.Load<UnitObj>("Scripts/Unit/"+Slot.King);
				if (_element == nextEvo.Element)Evolution();
				else {Debug.Log("Não tem o Elemento para evoluir.");}}
			else{Debug.Log("Não pode mais evoluir.");}
			break;
		case "King":
			if (Slot.God != "")
			{nextEvo  = Resources.Load<UnitObj>("Scripts/Unit/"+Slot.God);
				if (_element == nextEvo.Element)Evolution();
				else {Debug.Log("Não tem o Elemento para evoluir.");}}
			else{Debug.Log("Não pode mais evoluir.");}
			break;
		case "Queen":
			if (Slot.God != "")
			{nextEvo  = Resources.Load<UnitObj>("Scripts/Unit/"+Slot.God);
				if (_element == nextEvo.Element)Evolution();
				else {Debug.Log("Não tem o Elemento para evoluir.");}}
			else{Debug.Log("Não pode mais evoluir.");}
			break;
		case "Ruler":
			if (Slot.God != "")
			{nextEvo  = Resources.Load<UnitObj>("Scripts/Unit/"+Slot.God);
				if (_element == nextEvo.Element)Evolution();
				else {Debug.Log("Não tem o Elemento para evoluir.");}}
			else{Debug.Log("Não pode mais evoluir.");}
			break;
		case "God":
			Debug.Log("Não pode mais evoluir.");
			break;
		case "Anti-God":
			Debug.Log("Não pode mais evoluir.");
			break;
		case "Ultimate":
			Debug.Log("Não pode mais evoluir.");
			break;
		}
	}
	
	
	[Button]
	public void TryEvolutionType( int cust )
	{
		Debug.Log("Tentando evoluir. " + _rank);
		switch (_rank){
		case "Civil":
			if (Slot.Soldier != "")
			{nextEvo  = Resources.Load<UnitObj>("Scripts/Unit/"+Slot.Soldier);
				if (_type == nextEvo.Type){Evolution();
					svb.PlayerEnergy = svb.PlayerEnergy - cust;}
				else {Debug.Log("Não tem o tipo para evoluir.");}}
			else{Debug.Log("Não pode mais evoluir.");}
			break;
		case "Soldier":
			if (Slot.Combatant != "")
			{nextEvo  = Resources.Load<UnitObj>("Scripts/Unit/"+Slot.Combatant);
				if (_type == nextEvo.Type){Evolution();
					svb.PlayerEnergy = svb.PlayerEnergy - cust;}
				else {Debug.Log("Não tem o tipo para evoluir.");}}
			else{Debug.Log("Não pode mais evoluir.");}
			break;
		case "Combatant":
			if (Slot.General != "")
			{nextEvo  = Resources.Load<UnitObj>("Scripts/Unit/"+Slot.General);
				if (_type == nextEvo.Type){Evolution();
					svb.PlayerEnergy = svb.PlayerEnergy - cust;}
				else {Debug.Log("Não tem o tipo para evoluir.");}}
			else{Debug.Log("Não pode mais evoluir.");}
			break;
		case "General":
			if (Slot.King != "")
			{nextEvo  = Resources.Load<UnitObj>("Scripts/Unit/"+Slot.King);
				if (_type == nextEvo.Type){Evolution();
					svb.PlayerEnergy = svb.PlayerEnergy - cust;}
				else {Debug.Log("Não tem o tipo para evoluir.");}}
			else{Debug.Log("Não pode mais evoluir.");}
			break;
		case "King":
			if (Slot.God != "")
			{nextEvo  = Resources.Load<UnitObj>("Scripts/Unit/"+Slot.God);
				if (_type == nextEvo.Type){Evolution();
					svb.PlayerEnergy = svb.PlayerEnergy - cust;}
				else {Debug.Log("Não tem o tipo para evoluir.");}}
			else{Debug.Log("Não pode mais evoluir.");}
			break;
		case "Queen":
			if (Slot.God != "")
			{nextEvo  = Resources.Load<UnitObj>("Scripts/Unit/"+Slot.God);
				if (_type == nextEvo.Type){Evolution();
					svb.PlayerEnergy = svb.PlayerEnergy - cust;}
				else {Debug.Log("Não tem o tipo para evoluir.");}}
			else{Debug.Log("Não pode mais evoluir.");}
			break;
		case "Ruler":
			if (Slot.God != "")
			{nextEvo  = Resources.Load<UnitObj>("Scripts/Unit/"+Slot.God);
				if (_type == nextEvo.Type){Evolution();
					svb.PlayerEnergy = svb.PlayerEnergy - cust;}
				else {Debug.Log("Não tem o tipo para evoluir.");}}
			else{Debug.Log("Não pode mais evoluir.");}
			break;
		case "God":
			Debug.Log("Não pode mais evoluir.");
			break;
		case "Anti-God":
			Debug.Log("Não pode mais evoluir.");
			break;
		case "Ultimate":
			Debug.Log("Não pode mais evoluir.");
			break;
		}
	}
	
	[Button]
	public void TryEvolutionElement( int cust )
	{
		Debug.Log("Tentando evoluir. " + _rank);
		switch (_rank){
		case "Civil":
			if (Slot.Soldier != "")
			{nextEvo  = Resources.Load<UnitObj>("Scripts/Unit/"+Slot.Soldier);
				if (_element == nextEvo.Element){Evolution();
					svb.PlayerEnergy = svb.PlayerEnergy - cust;}
				else {Debug.Log("Não tem o Elemento para evoluir.");}}
			else{Debug.Log("Não pode mais evoluir.");}
			break;
		case "Soldier":
			if (Slot.Combatant != "")
			{nextEvo  = Resources.Load<UnitObj>("Scripts/Unit/"+Slot.Combatant);
				if (_element == nextEvo.Element){Evolution();
					svb.PlayerEnergy = svb.PlayerEnergy - cust;}
				else {Debug.Log("Não tem o Elemento para evoluir.");}}
			else{Debug.Log("Não pode mais evoluir.");}
			break;
		case "Combatant":
			if (Slot.General != "")
			{nextEvo  = Resources.Load<UnitObj>("Scripts/Unit/"+Slot.General);
				if (_element == nextEvo.Element){Evolution();
					svb.PlayerEnergy = svb.PlayerEnergy - cust;}
				else {Debug.Log("Não tem o Elemento para evoluir.");}}
			else{Debug.Log("Não pode mais evoluir.");}
			break;
		case "General":
			if (Slot.King != "")
			{nextEvo  = Resources.Load<UnitObj>("Scripts/Unit/"+Slot.King);
				if (_element == nextEvo.Element){Evolution();
					svb.PlayerEnergy = svb.PlayerEnergy - cust;}
				else {Debug.Log("Não tem o Elemento para evoluir.");}}
			else{Debug.Log("Não pode mais evoluir.");}
			break;
		case "King":
			if (Slot.God != "")
			{nextEvo  = Resources.Load<UnitObj>("Scripts/Unit/"+Slot.God);
				if (_element == nextEvo.Element){Evolution();
					svb.PlayerEnergy = svb.PlayerEnergy - cust;}
				else {Debug.Log("Não tem o Elemento para evoluir.");}}
			else{Debug.Log("Não pode mais evoluir.");}
			break;
		case "Queen":
			if (Slot.God != "")
			{nextEvo  = Resources.Load<UnitObj>("Scripts/Unit/"+Slot.God);
				if (_element == nextEvo.Element){Evolution();
					svb.PlayerEnergy = svb.PlayerEnergy - cust;}
				else {Debug.Log("Não tem o Elemento para evoluir.");}}
			else{Debug.Log("Não pode mais evoluir.");}
			break;
		case "Ruler":
			if (Slot.God != "")
			{nextEvo  = Resources.Load<UnitObj>("Scripts/Unit/"+Slot.God);
				if (_element == nextEvo.Element){Evolution();
					svb.PlayerEnergy = svb.PlayerEnergy - cust;}
				else {Debug.Log("Não tem o Elemento para evoluir.");}}
			else{Debug.Log("Não pode mais evoluir.");}
			break;
		case "God":
			Debug.Log("Não pode mais evoluir.");
			break;
		case "Anti-God":
			Debug.Log("Não pode mais evoluir.");
			break;
		case "Ultimate":
			Debug.Log("Não pode mais evoluir.");
			break;
		}
	}
	
	public void Evolution()
	{
		_Self._Terreno.PlayAnim("Evolution");
		switch (_rank){
		case "Civil" :
			_id = Slot.Soldier;
			break;
		case "Soldier" :
			_id = Slot.Combatant;
			break;
		case "Combatant" :
			_id = Slot.General;
			break;
		case "General" :
			_id = Slot.King;
			break;
		case "King" :
			_id = Slot.God;
			break;}
		selfObjUnit = Resources.Load<UnitObj>("Scripts/Unit/"+_id);
		ResetAllStatus();
		LoadCardDATA();
		FindObjectOfType<Manager_UI>().UpdateUI(_Self);
		
	}
	
	public void ResetAllStatus()
	{
		_boostATK = 0;
		_boostDEF = 0;
		_boostLife = 0;
		_DeboostATK = 0;
		_DeboostDEF = 0;
		_DeboostLife = 0;
		_OnStatus = "No";
		_OnStatusTurn = 0;
	}
	
	public void SendInfo(int skill){
		var inf = FindObjectOfType<InfoView>();
		if (skill > 0 && skill < 4){
			switch (skill){
			case 1:
				inf.LoadInfo(_effect1);
				break;
			case 2:
				inf.LoadInfo(_effect2);
				break;
			case 3:
				inf.LoadInfo(_status);
				break;
			}}
	}
}
