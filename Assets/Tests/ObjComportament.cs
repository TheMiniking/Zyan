using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ObjComportament : MonoBehaviour
{
	public GameObject self;
	
	void Start() => self = this.gameObject;
	
	public void SetPlaySet(){
		var et = FindObjectOfType<Etapas>();
		et.EtapaI.RemoveAllListeners();
		et.EtapaII.RemoveAllListeners();
		et.EtapaIII.RemoveAllListeners();
		et.EtapaI.AddListener(CorAleatoria);
		et.EtapaII.AddListener(OnOff);
		et.EtapaIII.AddListener(Reset);	
		et.escolhido = self;
}
	
	public void CorAleatoria(){
		self.GetComponent<Image>().color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
		self.transform.GetComponentInChildren<TMP_Text>().text = ""+Random.Range(0,999);
	}
	public void OnOff(){
		var r = Random.Range(0,1);
		switch (r){
		case 0:
			self.GetComponent<Button>().interactable = false; 
			break;
		case 1:
			self.GetComponent<Button>().interactable = true;
			break;
		}
		
	}
	
	public void Reset(){
		self.GetComponent<Button>().interactable = true;
		self.GetComponent<Image>().color = Color.white;
	}
}
