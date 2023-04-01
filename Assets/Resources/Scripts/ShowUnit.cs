using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.IO;
using UnityEditor;
using Sirenix.OdinInspector;

public class ShowUnit : MonoBehaviour
{
	[Title("Player Data ")]
	public PlayerInventaryOBJ player;
	
	[Title("Card")]
	public string _id;
	public UnitObj _unit;
	public RawImage _IMGcard;
	public RawImage _IMGcardBG;
	public RawImage _IMGcardEffect1;
	public RawImage _IMGcardEffect2;
	public RawImage _IMGcardRank;
	public RawImage _IMGcardElement;
	public RawImage _IMGcardStatus;
	public TMP_Text _TxtATK;
	public TMP_Text _TxtDEF;
	public TMP_Text _TxtLife;
	public TMP_Text _TxtQuant;
	public GameObject _Quant;
	public bool _onTime;
	public int _slotDeck;
	public Zyan.TimeUnits _slot;
	public bool _cardpool =true;
	public Button _Add;
	public Button _Remove;
	public DeckEdit deckeEdit;
	
	
	void Start(){
		if (_cardpool){
			if (_slotDeck > 0){_Add.gameObject.SetActive(false);
			_Remove.gameObject.SetActive(true);}
			else {_Add.gameObject.SetActive(true);
				_Remove.gameObject.SetActive(false);}
		}else {_Add.gameObject.SetActive(false);
			_Remove.gameObject.SetActive(false);}
		deckeEdit = FindObjectOfType<DeckEdit>();
	}
	
	void FixedUpdate(){if (_id != ""){if (_unit == null|| _unit.id != _id) LoadCard();}
	}
	[ShowInInspector] int qnt;
	public void LoadCard(){
		_unit = Resources.Load<UnitObj>("Scripts/Unit/"+_id);
		_IMGcardBG.texture = Resources.Load<Texture2D>("Imagens/UI/" + _unit.Type);
		_IMGcardEffect1.texture = Resources.Load<Texture2D>("Imagens/icons/" + _unit.Effect1);
		_IMGcardEffect2.texture = Resources.Load<Texture2D>("Imagens/icons/" + _unit.Effect2);
		_IMGcardRank.texture = Resources.Load<Texture2D>("Imagens/icons/" + _unit.Rank);
		_IMGcardElement.texture = Resources.Load<Texture2D>("Imagens/icons/" + _unit.Element);
		_IMGcardStatus.texture =  Resources.Load<Texture2D>("Imagens/icons/" + _unit.Status);
		_IMGcard.texture = Resources.Load<Texture2D>("Pixel Units/" + _id) != null ? Resources.Load<Texture2D>("Pixel Units/" + _id) : null;
		_TxtATK.text = _unit.ATK.ToString();
		_TxtDEF.text = _unit.DEF.ToString();
		_TxtLife.text = _unit.Life.ToString();
		if (_onTime){_Quant.SetActive(false);}
		else{_Quant.SetActive(true);
			bool exist = player.UnitUtilizadas.TryGetValue(_id, out qnt);
			qnt = player.PlayerInventaryUnit[_id].Quantidade - (exist?qnt:0);
			_TxtQuant.text = qnt.ToString() + "/"+ player.PlayerInventaryUnit[_id].Quantidade ;
			_Add.interactable = qnt>0?true:false;
		}
	}
	
	public void RemoveFromDeck(){
		switch (deckeEdit.atualSlot){
		case 1:
			switch (_slotDeck){
			case 1:
				player.AtualDeck.Slot1.Civil = "";
				break;
			case 2:
				player.AtualDeck.Slot1.Soldier = "";
				break;
			case 3:
				player.AtualDeck.Slot1.Combatant = "";
				break;
			case 4:
				player.AtualDeck.Slot1.General = "";
				break;
			case 5:
				player.AtualDeck.Slot1.King = "";
				break;
			case 6:
				player.AtualDeck.Slot1.God = "";
				break;}
			break;
		case 2:
			switch (_slotDeck){
			case 1:
				player.AtualDeck.Slot2.Civil = "";
				break;
			case 2:
				player.AtualDeck.Slot2.Soldier = "";
				break;
			case 3:
				player.AtualDeck.Slot2.Combatant = "";
				break;
			case 4:
				player.AtualDeck.Slot2.General = "";
				break;
			case 5:
				player.AtualDeck.Slot2.King = "";
				break;
			case 6:
				player.AtualDeck.Slot2.God = "";
				break;}
			break;
		case 3:
			switch (_slotDeck){
			case 1:
				player.AtualDeck.Slot3.Civil = "";
				break;
			case 2:
				player.AtualDeck.Slot3.Soldier = "";
				break;
			case 3:
				player.AtualDeck.Slot3.Combatant = "";
				break;
			case 4:
				player.AtualDeck.Slot3.General = "";
				break;
			case 5:
				player.AtualDeck.Slot3.King = "";
				break;
			case 6:
				player.AtualDeck.Slot3.God = "";
				break;}
			break;
		case 4:
			switch (_slotDeck){
			case 1:
				player.AtualDeck.Slot4.Civil = "";
				break;
			case 2:
				player.AtualDeck.Slot4.Soldier = "";
				break;
			case 3:
				player.AtualDeck.Slot4.Combatant = "";
				break;
			case 4:
				player.AtualDeck.Slot4.General = "";
				break;
			case 5:
				player.AtualDeck.Slot4.King = "";
				break;
			case 6:
				player.AtualDeck.Slot4.God = "";
				break;}
			break;
		case 5:
			switch (_slotDeck){
			case 1:
				player.AtualDeck.Slot5.Civil = "";
				break;
			case 2:
				player.AtualDeck.Slot5.Soldier = "";
				break;
			case 3:
				player.AtualDeck.Slot5.Combatant = "";
				break;
			case 4:
				player.AtualDeck.Slot5.General = "";
				break;
			case 5:
				player.AtualDeck.Slot5.King = "";
				break;
			case 6:
				player.AtualDeck.Slot5.God = "";
				break;}
			break;
		}
		player.DicUpdate();
		EndSave(false);
		deckeEdit.LoadlibraryObj();
	}
	
	public void AddToDeck(){
		switch (deckeEdit.atualSlot){
		case 1:
			switch (deckeEdit.atualRank){
			case 1:
				player.AtualDeck.Slot1.Civil = _id;
				break;
			case 2:
				player.AtualDeck.Slot1.Soldier = _id;
				break;
			case 3:
				player.AtualDeck.Slot1.Combatant = _id;
				break;
			case 4:
				player.AtualDeck.Slot1.General = _id;
				break;
			case 5:
				player.AtualDeck.Slot1.King = _id;
				break;
			case 6:
				player.AtualDeck.Slot1.God = _id;
				break;
			case 0:
				switch(_unit.Rank){
				case "Civil":
					player.AtualDeck.Slot1.Civil = _id;
					break;
				case "Soldier":
					player.AtualDeck.Slot1.Soldier = _id;
					break;
				case "Combatant":
					player.AtualDeck.Slot1.Combatant = _id;
					break;
				case "General":
					player.AtualDeck.Slot1.General = _id;
					break;
				case "King":
					player.AtualDeck.Slot1.King = _id;
					break;
				case "Queen":
					player.AtualDeck.Slot1.King = _id;
					break;
				case "Ruler":
					player.AtualDeck.Slot1.King = _id;
					break;
				case "God":
					player.AtualDeck.Slot1.God = _id;
					break;
				case "Anti-God":
					player.AtualDeck.Slot1.God = _id;
					break;
				case "Ultimate":
					player.AtualDeck.Slot1.God = _id;
					break;
				}
				break;}
			break;
		case 2:
			switch (deckeEdit.atualRank){
			case 1:
				player.AtualDeck.Slot2.Civil = _id;
				break;
			case 2:
				player.AtualDeck.Slot2.Soldier = _id;
				break;
			case 3:
				player.AtualDeck.Slot2.Combatant = _id;
				break;
			case 4:
				player.AtualDeck.Slot2.General = _id;
				break;
			case 5:
				player.AtualDeck.Slot2.King = _id;
				break;
			case 6:
				player.AtualDeck.Slot2.God = _id;
				break;
			case 0:
				switch(_unit.Rank){
				case "Civil":
					player.AtualDeck.Slot2.Civil = _id;
					break;
				case "Soldier":
					player.AtualDeck.Slot2.Soldier = _id;
					break;
				case "Combatant":
					player.AtualDeck.Slot2.Combatant = _id;
					break;
				case "General":
					player.AtualDeck.Slot2.General = _id;
					break;
				case "King":
					player.AtualDeck.Slot2.King = _id;
					break;
				case "Queen":
					player.AtualDeck.Slot2.King = _id;
					break;
				case "Ruler":
					player.AtualDeck.Slot2.King = _id;
					break;
				case "God":
					player.AtualDeck.Slot2.God = _id;
					break;
				case "Anti-God":
					player.AtualDeck.Slot2.God = _id;
					break;
				case "Ultimate":
					player.AtualDeck.Slot2.God = _id;
					break;
				}
				break;
			}
			break;
		case 3:
			switch (deckeEdit.atualRank){
			case 1:
				player.AtualDeck.Slot3.Civil = _id;
				break;
			case 2:
				player.AtualDeck.Slot3.Soldier = _id;
				break;
			case 3:
				player.AtualDeck.Slot3.Combatant = _id;
				break;
			case 4:
				player.AtualDeck.Slot3.General = _id;
				break;
			case 5:
				player.AtualDeck.Slot3.King = _id;
				break;
			case 6:
				player.AtualDeck.Slot3.God = _id;
				break;
			case 0:
				switch(_unit.Rank){
				case "Civil":
					player.AtualDeck.Slot3.Civil = _id;
					break;
				case "Soldier":
					player.AtualDeck.Slot3.Soldier = _id;
					break;
				case "Combatant":
					player.AtualDeck.Slot3.Combatant = _id;
					break;
				case "General":
					player.AtualDeck.Slot3.General = _id;
					break;
				case "King":
					player.AtualDeck.Slot3.King = _id;
					break;
				case "Queen":
					player.AtualDeck.Slot3.King = _id;
					break;
				case "Ruler":
					player.AtualDeck.Slot3.King = _id;
					break;
				case "God":
					player.AtualDeck.Slot3.God = _id;
					break;
				case "Anti-God":
					player.AtualDeck.Slot3.God = _id;
					break;
				case "Ultimate":
					player.AtualDeck.Slot3.God = _id;
					break;
				}
				break;
			}
			break;
		case 4:
			switch (deckeEdit.atualRank){
			case 1:
				player.AtualDeck.Slot4.Civil = _id;
				break;
			case 2:
				player.AtualDeck.Slot4.Soldier = _id;
				break;
			case 3:
				player.AtualDeck.Slot4.Combatant = _id;
				break;
			case 4:
				player.AtualDeck.Slot4.General = _id;
				break;
			case 5:
				player.AtualDeck.Slot4.King = _id;
				break;
			case 6:
				player.AtualDeck.Slot4.God = _id;
				break;
			case 0:
				switch(_unit.Rank){
				case "Civil":
					player.AtualDeck.Slot4.Civil = _id;
					break;
				case "Soldier":
					player.AtualDeck.Slot4.Soldier = _id;
					break;
				case "Combatant":
					player.AtualDeck.Slot4.Combatant = _id;
					break;
				case "General":
					player.AtualDeck.Slot4.General = _id;
					break;
				case "King":
					player.AtualDeck.Slot4.King = _id;
					break;
				case "Queen":
					player.AtualDeck.Slot4.King = _id;
					break;
				case "Ruler":
					player.AtualDeck.Slot4.King = _id;
					break;
				case "God":
					player.AtualDeck.Slot4.God = _id;
					break;
				case "Anti-God":
					player.AtualDeck.Slot4.God = _id;
					break;
				case "Ultimate":
					player.AtualDeck.Slot4.God = _id;
					break;
				}
				break;
			}
			break;
		case 5:
			switch (deckeEdit.atualRank){
			case 1:
				player.AtualDeck.Slot5.Civil = _id;
				break;
			case 2:
				player.AtualDeck.Slot5.Soldier = _id;
				break;
			case 3:
				player.AtualDeck.Slot5.Combatant = _id;
				break;
			case 4:
				player.AtualDeck.Slot5.General = _id;
				break;
			case 5:
				player.AtualDeck.Slot5.King = _id;
				break;
			case 6:
				player.AtualDeck.Slot5.God = _id;
				break;
			case 0:
				switch(_unit.Rank){
				case "Civil":
					player.AtualDeck.Slot5.Civil = _id;
					break;
				case "Soldier":
					player.AtualDeck.Slot5.Soldier = _id;
					break;
				case "Combatant":
					player.AtualDeck.Slot5.Combatant = _id;
					break;
				case "General":
					player.AtualDeck.Slot5.General = _id;
					break;
				case "King":
					player.AtualDeck.Slot5.King = _id;
					break;
				case "Queen":
					player.AtualDeck.Slot5.King = _id;
					break;
				case "Ruler":
					player.AtualDeck.Slot5.King = _id;
					break;
				case "God":
					player.AtualDeck.Slot5.God = _id;
					break;
				case "Anti-God":
					player.AtualDeck.Slot5.God = _id;
					break;
				case "Ultimate":
					player.AtualDeck.Slot5.God = _id;
					break;
				}
				break;
			}
			break;
		
		}
		player.DicUpdate();
		EndSave(true);
		deckeEdit.LoadTime();
		deckeEdit.LoadlibraryObj();
	}
	
	public void EndSave(bool add){
		int i =0;
		foreach(var d in player.Player.Decks){
			if (d.timeName == player.AtualDeck.timeName)
			{player.Player.Decks[i] = player.AtualDeck;
				break;
			}
			i++;
		}
		player.SavePlayerData();
		this.gameObject.SetActive(add);
	}
}
