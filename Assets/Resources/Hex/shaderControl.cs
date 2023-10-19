using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;

public class shaderControl : MonoBehaviour
{
	public Material cristal;
	public Material cristalM;
	public GameObject mCristal;
	public GameObject hexMetal;
	public List<Color> cor;
	public string lastUnit;
	[Button]
	public void GetAtualImagem(){
		var unit = GetComponent<UnitField>();
		MudarImagem(unit.unit._Self._id,unit._isplayer, unit.unit._Self._type);}
	
	[Button]
	public void MudarImagem(string id,bool player, string type){
		lastUnit = id;
		cristalM = mCristal.GetComponent<MeshRenderer>().materials[1];
		cristal = mCristal.GetComponent<MeshRenderer>().materials[0];
		var verification = Resources.Load<Texture>("Hex/CristalM/"+id);
		if (verification != null){
			cristalM.SetTexture("_MainTex",Resources.Load<Texture>("Hex/CristalM/"+id));
			cristalM.SetTexture("_MaskImagem",Resources.Load<Texture>("Hex/CristalMShadow/"+id+"b"));
			cristal.SetTexture("_TextureBase",Resources.Load<Texture>("Hex/CristalMShadow/"+id+"b"));	
		}else{
			cristalM.SetTexture("_MainTex",Resources.Load<Texture>("Hex/CristalM/"+type));
			cristalM.SetTexture("_MaskImagem",Resources.Load<Texture>("Hex/CristalM/"+type));
			cristal.SetTexture("_TextureBase",Resources.Load<Texture>("Hex/CristalM/"+type));
		}
		if(player){cristal.SetColor("_Color",cor[0]);}
		else{cristal.SetColor("_Color",cor[1]);}
		
	}
	
	[System.Serializable]
	public class Color2{
		public Color Hi;
		public Color Normal;
	}
}
