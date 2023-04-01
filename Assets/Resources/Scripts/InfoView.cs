using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Sirenix.OdinInspector;

public class InfoView : MonoBehaviour
{
	public TMP_Text Name;
	public TMP_Text Descrip;
	public InfoOBJ Data;
	
	[Button]public void LoadInfo(string inf){
		Name.text = Data.Info[inf].Name;
		Descrip.text = Data.Info[inf].Description;
	}
	
	[Button]public void LoadTxt(string txt1, string txt2){
		Name.text = txt1;
		Descrip.text = txt2;
	}
	
	
}
