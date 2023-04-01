using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class UIMotion : MonoBehaviour
{
	public GameObject painel;
	public Vector3 painelOriginalPosition;
	public float originalX;
	public float distancia;
	public bool posiOnOff =false;
	public bool p1;
	public SceneVariables_Battle svb;
	
	void Start() => painelOriginalPosition = painel.transform.position;
	void Update() {
		if (p1 && svb.p1.onSummon && posiOnOff) {Move(false);}
		if (!p1 && svb.p2.onSummon && posiOnOff) {Move(false);}
	}
	
	[Button]
	public void Move(bool back){
		if(back){ LeanTween.moveLocalX(painel,-originalX,0.5f);}
		else{LeanTween.moveLocalX(painel,-distancia,0.5f);}
	}
	
	[Button]
	public void AdaptMove(){
		posiOnOff = posiOnOff?false:true;
		Move(posiOnOff);
	}
	
}
