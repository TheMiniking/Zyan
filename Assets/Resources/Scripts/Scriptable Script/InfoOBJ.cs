using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;
using UnityEditor;

[CreateAssetMenu (fileName = "Info Obj", menuName = "Zyan Assets/Create Info Obj")]
public class InfoOBJ : ScriptableObject
{
	public Zyan.InfoItem[] infoData;
	[ShowInInspector]public Dictionary<string,Zyan.InfoItem> Info = new Dictionary<string, Zyan.InfoItem>{};
	
	void OnValidate()=> LoadData();
	
	[Button]
	public void LoadData(){
		infoData = JsonReader.ReadListFromJSONInfo();
		Info.Clear();
		foreach (Zyan.InfoItem i in infoData){
			Info.Add(i.Name, i);
		}
	}
}
