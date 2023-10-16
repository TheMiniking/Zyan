using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using TMPro;
using Sirenix.OdinInspector;

public class Manager_UI : MonoBehaviour
{
	//public GameObject _unitShow;
	public Unit unit;
	public TMP_Text _Name;
	public TMP_Text _ATK;
	public TMP_Text _DEF;
	public TMP_Text _Life;
	public TMP_Text _ModATK;
	public TMP_Text _ModDEF;
	public TMP_Text _ModLife;
	public RawImage _Type;
	public RawImage _Element;
	public RawImage _Rank;
	public RawImage _Effect1;
	public RawImage _Effect2;
	public RawImage _Status;
	public GameObject _Enfermagen;
	public TMP_Text _NameEffect;
	public TMP_Text _Effect;
	public TMP_Text _PlName;
	public TMP_Text _PlRank;
	public TMP_Text _PlEnergy;
	public TMP_Text _EnRank;
	public TMP_Text _EnName;
	public TMP_Text _EnEnergy;
	public TMP_Text _TypeEvo;
	public TMP_Text _ElementEvo;
	public Button _ButtonTypeEvo;
	public Button _ButtonElemntEvo;
	public UItens hud = new UItens();
	public TMP_Text _DetailName;
	public TMP_Text _DetailDescription;
	public GameObject _DetailOBJ;
	public TMP_Text _TerrenoNameTxt;
	public TMP_Text _DescriptionTxt;
	public TMP_Text _TerrenoNameInfoTxt;
	public TMP_Text _TerrenoDescription;
	public string _TerrenoName;
	public GameObject _TerrenoDescOBJ;
	public GameObject _TerrenoMiniOBJ;
	public Button _TerrenoBt;
	public Button _PassBt;
	public Button _CancelSummonBt;
	public GameObject _Fases;
	public TMP_Text _FaseTurns;
	public TMP_Text _FasePlayer;
	public int _out;
	public SceneVariables_Battle svb;
	public GameObject intro;
	
	[ShowInInspector] public Zyan.InfoItem[] itemList;
	[Button] public void LoadItens() => itemList = JsonReader.ReadListFromJSONInfo();
	
    // Start is called before the first frame update
    void Start()
	{
		intro.GetComponent<Animator>().SetBool("Entra",true);
		_EnName.text = svb.p2.Name;
		_PlName.text = svb.p1.Name;
		_EnRank.text = "Rank " + svb.p2.Rank;
	    _PlRank.text = "Rank " + svb.p1.Rank;
	    LoadItens();
	    var u = FindObjectOfType<Unit>();
	    UpdateUI(u);
    }

    // Update is called once per frame
    void Update()
    {
		_EnEnergy.text = svb.EnemyEnergy.ToString();
		_PlEnergy.text = svb.PlayerEnergy.ToString();
	    if (unit != null) UpdateUI(unit);
	    if (_TerrenoName == "") {_TerrenoMiniOBJ.gameObject.SetActive(false);}
	    else {_TerrenoMiniOBJ.gameObject.SetActive(true);
		    _TerrenoNameTxt.text = _TerrenoName;}
	    if (svb.atualTurn=="P1"){
	    	if (svb.p1.onSummon && !_CancelSummonBt.gameObject.activeSelf ){
	    		_CancelSummonBt.gameObject.SetActive(true);
	    		_CancelSummonBt.gameObject.GetComponentInChildren<TMP_Text>().text = "Cancel Summon";
				_PassBt.gameObject.SetActive(false);}
	    	else if(!svb.p1.onSummon && !svb.p1.onMove ){
	    		_CancelSummonBt.gameObject.SetActive(false);
		    	_PassBt.gameObject.SetActive(true);
	    	}else {
	    		_CancelSummonBt.gameObject.SetActive(true);
		    	_PassBt.gameObject.SetActive(false);
		    	_CancelSummonBt.gameObject.GetComponentInChildren<TMP_Text>().text = "Cancel Move";}
	    }else {
	    	_CancelSummonBt.gameObject.SetActive(false);
		    _PassBt.gameObject.SetActive(false);
	    }
    }
    
	public void UpdateTerreno(){
		var found2 = false;
		var t2 =svb._LastTerreno.GetComponent<Terreno>();
		if (t2._SpellData.id == ""){
		foreach ( Zyan.InfoItem i in itemList ){
			var d = _TerrenoName.Split(" ");
			if (d.Length > 1)
			{if (i.Name == d[2]) { 
				StopCoroutine(Detail());
				_TerrenoNameInfoTxt.text = _TerrenoName;
				_TerrenoDescription.text = i.Description;
				StartCoroutine(Detail());
				found2 = true;
				break;}
			else {
					if (i.Name == _TerrenoName) { 
						StopCoroutine(Detail());
						_TerrenoNameInfoTxt.text = _TerrenoName;
						_TerrenoDescription.text = i.Description;
						StartCoroutine(Detail());
						found2 = true;
						break;}
			}}
			else {
				if (i.Name == _TerrenoName) { 
				StopCoroutine(Detail());
				_TerrenoNameInfoTxt.text = _TerrenoName;
				_TerrenoDescription.text = i.Description;
				StartCoroutine(Detail());
				found2 = true;
				break;}}
		}if (found2 != true) {
			StopCoroutine(Detail());
			_TerrenoNameInfoTxt.text = _TerrenoName;
			_TerrenoDescription.text = "Terreno Comum , sem nenhuma modificação";
			StartCoroutine(Detail());}
		}
		else {
			StopCoroutine(Detail());
			_TerrenoNameInfoTxt.text = _TerrenoName;
			_TerrenoDescription.text = t2._SpellData.Description;
			StartCoroutine(Detail());
		}
	}
    
	public void UpdateUI(Unit _unitShow )
	{
		unit = _unitShow.GetComponent<Unit>();
		hud.Name = unit._Self._name;
		hud.ATK = unit._Self._TotallAtk;
		hud.DEF = unit._Self._TotalDef;
		hud.Life = unit._Self._TotalLife;
		hud.Type = unit._Self._type;
		hud.Element = unit._Self._element;
		hud.Rank = unit._Self._rank;
		hud.Status = unit._Self._status;
		hud.Effect1 = unit._Self._effect1;
		hud.Effect2 = unit._Self._effect2;
		_Name.text = unit._Self._name;
		_ATK.text = "" + hud.ATK;
		_DEF.text = "" + hud.DEF;
		_Life.text = ""+ hud.Life;
		if (unit._Self._ModAtk > 0) {
			_ModATK.gameObject.SetActive(true);
			_ModATK.text = "+ " + unit._Self._ModAtk;}
		else if (unit._Self._ModAtk < 0) {
			_ModATK.gameObject.SetActive(true);
			_ModATK.text = "- " + unit._Self._ModAtk;}
		else{_ModATK.gameObject.SetActive(false);}
		if (unit._Self._ModDef > 0) {
			_ModDEF.gameObject.SetActive(true);
			_ModDEF.text = "+ " + unit._Self._ModDef;}
		else if (unit._Self._ModDef < 0) {
			_ModDEF.gameObject.SetActive(true);
			_ModDEF.text = "- " + unit._Self._ModDef;}
			else{_ModDEF.gameObject.SetActive(false);}
		if (unit._Self._ModLife > 0) {
			_ModLife.gameObject.SetActive(true);
			_ModLife.text = "+ " + unit._Self._ModAtk;}
		else if (unit._Self._ModLife < 0) {
			_ModLife.gameObject.SetActive(true);
			_ModLife.text = " " + unit._Self._ModLife;}
			else{_ModLife.gameObject.SetActive(false);}
		_Type.texture = Resources.Load<Texture2D>("Imagens/UI/" + unit._Self._type);
		_Element.texture = Resources.Load<Texture2D>("Imagens/UI/" + unit._Self._element);
		_Rank.texture = Resources.Load<Texture2D>("Imagens/UI/" + unit._Self._rank);
		_Effect1.texture = Resources.Load<Texture2D>("Imagens/iconInfo/" + unit._Self._effect1);
		_Effect2.texture = Resources.Load<Texture2D>("Imagens/iconInfo/" + unit._Self._effect2);
		_Status.texture = Resources.Load<Texture2D>("Imagens/iconInfo/" + unit._Self._status);
		//_NameEffect.text = unit._type + " / " + unit._element + " / " + unit._rank;
		//_Effect.text = unit._evolution;
		/*Energy Evolution - Desativado por enquanto 
		if (unit._OnField){
			if (unit._isPlayer){
				_ButtonElemntEvo.gameObject.SetActive(true);
				_ButtonTypeEvo.gameObject.SetActive(true);
				var energ1 = unit.selfObjUnit.EvolCust2.Split(",");
				if (SceneVariables_Battle.PlayerEnergy >= int.Parse(energ1[1]))
				{_ButtonElemntEvo.interactable = true;
					_ElementEvo.text = energ1[1];}
				else{_ButtonElemntEvo.interactable = false;
					_ElementEvo.text = energ1[1];}
				var energ2 = unit.selfObjUnit.EvolCust3.Split(",");
				if (SceneVariables_Battle.PlayerEnergy >= int.Parse(energ2[1]))
				{_ButtonTypeEvo.interactable = true;
					_TypeEvo.text = energ2[1];}
				else {_ButtonTypeEvo.interactable = false;
					_TypeEvo.text = energ2[1];}
			}else {_ButtonElemntEvo.gameObject.SetActive(false);
				_ButtonTypeEvo.gameObject.SetActive(false);}
		}else {_ButtonElemntEvo.gameObject.SetActive(false);
		_ButtonTypeEvo.gameObject.SetActive(false);}
		*/
	}
	
	public void GoEvolType() => unit._Self.TryEvolutionType(int.Parse(_TypeEvo.text));
	
	public void GoEvolElement() => unit._Self.TryEvolutionElement(int.Parse(_ElementEvo.text));
	
	public void UpdateUIDescription(string data)
	{
		_NameEffect.text = data;
		//criar json com infos e comletar isso com uma funçao.
	}
	
	public void ShowDetailEffect1() => ShowDetail(unit._Self._effect1);
	public void ShowDetailEffect2() => ShowDetail(unit._Self._effect2);
	public void ShowDetailStatus() => ShowDetail(unit._Self._status);
	public void ShowDetailRank() => ShowDetail(unit._Self._rank);
	public void ShowDetailElement() => ShowDetail(unit._Self._element);
	public void ShowDetailType() => ShowDetail(unit._Self._type);
	
	public void ShowDetail( string data){
		var found = false;
		foreach ( Zyan.InfoItem i in itemList ){
			if (i.Name == data) { 
				StopCoroutine(Detail());
				_DetailName.text = data;
				_DetailDescription.text = i.Description;
				found = true;
				StartCoroutine(Detail());
				break;}
		}
		if (found != true) _DetailOBJ.gameObject.SetActive( false);
	}
	
	public IEnumerator Detail(){
		yield return new WaitForSecondsRealtime (4f);
		_DetailOBJ.gameObject.SetActive( false);
		_TerrenoDescOBJ.gameObject.SetActive( false);
		_TerrenoBt.interactable = true;
	}
	public void TurnShow(string turn, string player,bool inOut){
		_FaseTurns.text  = "Turn " + turn;
		_FasePlayer.text = player;
		if (!inOut)LeanTween.moveLocalX(_Fases,0,1f);
		else LeanTween.moveLocalX(_Fases,_out,1f);
	}
	
	public IEnumerator TurnShowAuto(){
		string turn = svb.turnCount.ToString();
		string player = ManagerTurn._PlayerTurn?svb.p1.Name:svb.p2.Name;
		TurnShow(turn,player,false);
		yield return new WaitForSecondsRealtime (2f);
		TurnShow(turn,player,true);
	}
	
	public void StShowAuto()=> StartCoroutine(TurnShowAuto());
	
	public class UItens{
		public string Name;
		public string Rank;
		public string Element;
		public string Type;
		public string Status;
		public string Effect1;
		public string Effect2;
		public int ATK;
		public int DEF;
		public int Life;
		public string Terreno;
		public string Nome_Detalhe;
		public string Detalhe;
	}
}
