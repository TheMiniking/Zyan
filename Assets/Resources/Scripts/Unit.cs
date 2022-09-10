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
	[Title("Card", "info $_name")]
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
	public UnitObj selfObjUnit;
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
	public Zyan.EvolutionCheck _EvolutionCheck;
	public int _TimeToReset = -1;
	public bool _ResetMod;
    
	[Title("Unit OBJ e Valores")]
    public GameObject _Self ;
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
   
    void Start()
    {
	    _Self = this.gameObject;
	    if (_isPlayer){
	    	switch (_slotDeck)
	    	{
	    	case 1 :
		    	Slot = SceneVariables_Battle._PlayerDeck.Slot1;
		    	break;
	    	case 2 :
		    	Slot = SceneVariables_Battle._PlayerDeck.Slot2;
		    	break;
	    	case 3 :
		    	Slot = SceneVariables_Battle._PlayerDeck.Slot3;
		    	break;
	    	case 4 :
		    	Slot = SceneVariables_Battle._PlayerDeck.Slot4;
		    	break;
	    	case 5 :
		    	Slot = SceneVariables_Battle._PlayerDeck.Slot5;
		    	break;
	    	}
		    _id = Slot.Civil;
		    selfObjUnit = Resources.Load<UnitObj>("Scripts/Unit/"+_id);
	    }else{
	    	switch (_slotDeck)
	    	{
	    	case 1 :
		    	Slot = SceneVariables_Battle._EnemyDeck.Slot1;
		    	break;
	    	case 2 :
		    	Slot = SceneVariables_Battle._EnemyDeck.Slot2;
		    	break;
	    	case 3 :
		    	Slot = SceneVariables_Battle._EnemyDeck.Slot3;
		    	break;
	    	case 4 :
		    	Slot = SceneVariables_Battle._EnemyDeck.Slot4;
		    	break;
	    	case 5 :
		    	Slot = SceneVariables_Battle._EnemyDeck.Slot5;
		    	break;
	    	}
	    	_id = Slot.Civil;
		    selfObjUnit = Resources.Load<UnitObj>("Scripts/Unit/"+_id);
	    }
	    LoadCardDATA();
    }
    
    private void FixedUpdate()
    {
	    if (_Index != -1) { UpdateCard(); }
	    if ( _TotalLife <= 0 ) { SendToEnfermary();}
	    if ( _UnitOnField != null) {_OnField = true;}
	    else {_OnField = false;}
	    if (_TimeToReset == 0 ){ _ResetMod = true;}
	    if (_ResetMod) {ResetMod();}
    }
    
	public void ResetMod(){
		_ProvisoryModATK = 0;
		_ProvisoryModDEF = 0;
		_UnitOnField.GetComponent<UnitField>()._SpecialStatus = "";
		_ResetMod = false;
		_TimeToReset = -1;
	}
    
    
	public void OnClick()
	{
		var aud = FindObjectOfType<ManagerSound>();
		aud.audio.clip = aud.clickUI;
		aud.audio.Play();
		var z = FindObjectOfType<Manager_UI>();
		z._TerrenoName = "";
		z.UpdateUI(_Self);
		if (_isPlayer)
		{if (SceneVariables_Battle.p1.onSummon && SceneVariables_Battle.p1.canSummon)
			{ if (_OnField != true){FinishSummon();}}}
	}
	
	public void UpdateCard()
	{    
		VerifyStatus();
		VerifyUIColor();
		VerifyResources();
		VerifyEquip();
		VerifyOnFieldEnfermary();
		if(SceneVariables_Battle.p1.onSummon )
		{if (_OnField == false && _isPlayer == true){_Summon.gameObject.SetActive(true);}}
        else { _Summon.gameObject.SetActive(false); }
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
	}
	
	private void VerifyOnFieldEnfermary()
	{
        if (_OnField )
        {	_SelfButton.interactable = false ;
	        _Off.gameObject.SetActive(true);
	        var o = _UnitOnField.GetComponent<UnitField>();
	        var t = o._AtualHex;
	        _Terreno = t.GetComponent<Terreno>();}
        else{ if (_TotalLife >= Mathf.RoundToInt((_originalLife + _boostLife - _DeboostLife) / 2) )
            {	_SelfButton.interactable = true;
                _Off.gameObject.SetActive(false);}
        	else{_SelfButton.interactable = false;
                _Off.gameObject.SetActive(true); }}
		if ( _TotalLife >= _originalLife + _boostLife - _DeboostLife )
        {   _CardBGAnim.SetBool("Enfermary", false) ;
	        _OnEnfermary = false;
	        _Enfermary.SetActive(false);}
        else {if (_OnField)
        	{	_OnEnfermary = false;
	        	_CardBGAnim.SetBool("Enfermary", false);
	        	_Enfermary.SetActive(false);
        	}else if (_TotalLife < _originalLife + _boostLife - _DeboostLife)
        	{	_OnEnfermary = true;
	        	_CardBGAnim.SetBool("Enfermary", true);
        		_Enfermary.SetActive(true);
        	}
            else { _OnEnfermary = false; 
	            _CardBGAnim.SetBool("Enfermary", false);
            	_Enfermary.SetActive(false);}}
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
		if (_originalAtk < _TotallAtk)				{_vATK.color = _boost;}
		else if (_originalAtk == _TotallAtk)		{_vATK.color = Color.white;}
		else {_vATK.color = _deboost;}
		_vATK.text = "" + _TotallAtk;
		if (_originalDef < _TotalDef)				{_vDEF.color = _boost;}
		else if (_originalDef == _TotalDef)			{_vDEF.color = Color.white;}
		else {_vDEF.color = _deboost;}
		_vDEF.text = "" + _TotalDef;
		if (_originalLife >_TotalLife)				{_vLife.color = _boost;}
		else if (_originalLife == _TotalLife)		{_vLife.color = Color.white;}
		else{_vLife.color = _boost;}
		_vLife.text = "" + _TotalLife;
		
	}
	
	private void VerifyResources()
	{
		_cardBG.texture = Resources.Load<Texture2D>("Imagens/UI/" + _type);
        _cardEffect1.texture = Resources.Load<Texture2D>("Imagens/icons/" + _effect1);
        _cardEffect2.texture = Resources.Load<Texture2D>("Imagens/icons/" + _effect2);
        _cardStatus.texture = Resources.Load<Texture2D>("Imagens/icons/" + _status);
        if ( _isPlayer)
        {    
        	_MaterialType = Resources.Load<Material>("Unit Tipos/" + _type);
	        _cardBG.color = _PlayerColor;
	        _CardBGAnim.SetBool("Enemy", false );
        }
        else 
        {    _MaterialType = Resources.Load<Material>("Unit Tipos/" + _type +" 2");
	        _cardBG.color = _EnemyColor;
	     	_CardBGAnim.SetBool("Enemy", true );
        }
		if (Resources.Load<Texture2D>("Pixel Units/" + _id) != null)
		{ _cardImage.texture = Resources.Load<Texture2D>("Pixel Units/" + _id); }
		else { _cardImage.color = _transparent; }
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
		StartCoroutine(_Terreno.AnimDeath());
		GameObject.Destroy( _UnitOnField);
		_UnitOnField = null;
		_OnField = false;
		_OnStatus = "No";
		_OnStatusTurn = 0;
	}
	
    private Vector3 x;
	public void FinishSummon()
	{
		x = SceneVariables_Battle._LastTerreno.transform.position ;
		x = x + new Vector3(0f,0.8f,0f);
		var y = SceneVariables_Battle._LastTerreno.transform.rotation;
		Quaternion q = new Quaternion(-90f,-90f,180f,0f); 
		SceneVariables_Battle._LastSummonUnit = this;
		_UnitOnField = Instantiate(_UnitPrefab, x, y );
		_UnitOnField.gameObject.name = _name;
		SceneVariables_Battle.p1.onSummon = false;
		SceneVariables_Battle.p1.canSummon = false;
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
					SceneVariables_Battle.PlayerEnergy = SceneVariables_Battle.PlayerEnergy - cust;}
				else {Debug.Log("Não tem o tipo para evoluir.");}}
			else{Debug.Log("Não pode mais evoluir.");}
			break;
		case "Soldier":
			if (Slot.Combatant != "")
			{nextEvo  = Resources.Load<UnitObj>("Scripts/Unit/"+Slot.Combatant);
				if (_type == nextEvo.Type){Evolution();
					SceneVariables_Battle.PlayerEnergy = SceneVariables_Battle.PlayerEnergy - cust;}
				else {Debug.Log("Não tem o tipo para evoluir.");}}
			else{Debug.Log("Não pode mais evoluir.");}
			break;
		case "Combatant":
			if (Slot.General != "")
			{nextEvo  = Resources.Load<UnitObj>("Scripts/Unit/"+Slot.General);
				if (_type == nextEvo.Type){Evolution();
					SceneVariables_Battle.PlayerEnergy = SceneVariables_Battle.PlayerEnergy - cust;}
				else {Debug.Log("Não tem o tipo para evoluir.");}}
			else{Debug.Log("Não pode mais evoluir.");}
			break;
		case "General":
			if (Slot.King != "")
			{nextEvo  = Resources.Load<UnitObj>("Scripts/Unit/"+Slot.King);
				if (_type == nextEvo.Type){Evolution();
					SceneVariables_Battle.PlayerEnergy = SceneVariables_Battle.PlayerEnergy - cust;}
				else {Debug.Log("Não tem o tipo para evoluir.");}}
			else{Debug.Log("Não pode mais evoluir.");}
			break;
		case "King":
			if (Slot.God != "")
			{nextEvo  = Resources.Load<UnitObj>("Scripts/Unit/"+Slot.God);
				if (_type == nextEvo.Type){Evolution();
					SceneVariables_Battle.PlayerEnergy = SceneVariables_Battle.PlayerEnergy - cust;}
				else {Debug.Log("Não tem o tipo para evoluir.");}}
			else{Debug.Log("Não pode mais evoluir.");}
			break;
		case "Queen":
			if (Slot.God != "")
			{nextEvo  = Resources.Load<UnitObj>("Scripts/Unit/"+Slot.God);
				if (_type == nextEvo.Type){Evolution();
					SceneVariables_Battle.PlayerEnergy = SceneVariables_Battle.PlayerEnergy - cust;}
				else {Debug.Log("Não tem o tipo para evoluir.");}}
			else{Debug.Log("Não pode mais evoluir.");}
			break;
		case "Ruler":
			if (Slot.God != "")
			{nextEvo  = Resources.Load<UnitObj>("Scripts/Unit/"+Slot.God);
				if (_type == nextEvo.Type){Evolution();
					SceneVariables_Battle.PlayerEnergy = SceneVariables_Battle.PlayerEnergy - cust;}
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
					SceneVariables_Battle.PlayerEnergy = SceneVariables_Battle.PlayerEnergy - cust;}
				else {Debug.Log("Não tem o Elemento para evoluir.");}}
			else{Debug.Log("Não pode mais evoluir.");}
			break;
		case "Soldier":
			if (Slot.Combatant != "")
			{nextEvo  = Resources.Load<UnitObj>("Scripts/Unit/"+Slot.Combatant);
				if (_element == nextEvo.Element){Evolution();
					SceneVariables_Battle.PlayerEnergy = SceneVariables_Battle.PlayerEnergy - cust;}
				else {Debug.Log("Não tem o Elemento para evoluir.");}}
			else{Debug.Log("Não pode mais evoluir.");}
			break;
		case "Combatant":
			if (Slot.General != "")
			{nextEvo  = Resources.Load<UnitObj>("Scripts/Unit/"+Slot.General);
				if (_element == nextEvo.Element){Evolution();
					SceneVariables_Battle.PlayerEnergy = SceneVariables_Battle.PlayerEnergy - cust;}
				else {Debug.Log("Não tem o Elemento para evoluir.");}}
			else{Debug.Log("Não pode mais evoluir.");}
			break;
		case "General":
			if (Slot.King != "")
			{nextEvo  = Resources.Load<UnitObj>("Scripts/Unit/"+Slot.King);
				if (_element == nextEvo.Element){Evolution();
					SceneVariables_Battle.PlayerEnergy = SceneVariables_Battle.PlayerEnergy - cust;}
				else {Debug.Log("Não tem o Elemento para evoluir.");}}
			else{Debug.Log("Não pode mais evoluir.");}
			break;
		case "King":
			if (Slot.God != "")
			{nextEvo  = Resources.Load<UnitObj>("Scripts/Unit/"+Slot.God);
				if (_element == nextEvo.Element){Evolution();
					SceneVariables_Battle.PlayerEnergy = SceneVariables_Battle.PlayerEnergy - cust;}
				else {Debug.Log("Não tem o Elemento para evoluir.");}}
			else{Debug.Log("Não pode mais evoluir.");}
			break;
		case "Queen":
			if (Slot.God != "")
			{nextEvo  = Resources.Load<UnitObj>("Scripts/Unit/"+Slot.God);
				if (_element == nextEvo.Element){Evolution();
					SceneVariables_Battle.PlayerEnergy = SceneVariables_Battle.PlayerEnergy - cust;}
				else {Debug.Log("Não tem o Elemento para evoluir.");}}
			else{Debug.Log("Não pode mais evoluir.");}
			break;
		case "Ruler":
			if (Slot.God != "")
			{nextEvo  = Resources.Load<UnitObj>("Scripts/Unit/"+Slot.God);
				if (_element == nextEvo.Element){Evolution();
					SceneVariables_Battle.PlayerEnergy = SceneVariables_Battle.PlayerEnergy - cust;}
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
		StartCoroutine(_Terreno.AnimEvolution());
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
}
