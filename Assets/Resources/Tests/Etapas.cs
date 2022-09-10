using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Linq;
using UnityEditor;
using Sirenix.OdinInspector;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class Etapas : MonoBehaviour
{
	public Toggle faseI;
	public Toggle faseII;
	public Toggle faseIII;
	public Toggle faseIV;
	public Button começo;
	public GameObject escolhido;
	
	[ShowInInspector] public UnityEvent EtapaI;
	[ShowInInspector] public UnityEvent EtapaII;
	[ShowInInspector] public UnityEvent EtapaIII;
	[ShowInInspector] public UnityEvent EtapaIV;
	
	[Button]
	public void EventEtapaI() { 
		EtapaI?.Invoke();
		faseI.isOn = true ;
		faseII.isOn = false ;
		faseIII.isOn = false ;
	}
	
	[Button]
	public void EventEtapaII() {
		EtapaII?.Invoke();
		faseI.isOn = false ;
		faseII.isOn = true ;
		faseIII.isOn = false ;}
	
	[Button]
	public void EventEtapaIII() {
		EtapaIII?.Invoke();
		faseI.isOn = false ;
		faseII.isOn = false ;
		faseIII.isOn = true ;}
	
	[Button]
	public void EventEtapaIV() => EtapaIV?.Invoke();
	
	public void BOn() => StartCoroutine(Steps());
	
	public IEnumerator Steps(){
		EventEtapaI();
		yield return new WaitForSecondsRealtime (1f);
		EventEtapaII();
		yield return new WaitForSecondsRealtime (1f);
		EventEtapaIII();
		yield return new WaitForSecondsRealtime (1f);
		Debug.Log("Finalizou!");
	}
	
	void Update()
	{
		if (escolhido == null){começo.interactable = false;}
		else{começo.interactable = true ;}
	}
}
