using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;
using UnityEditor;

[CreateAssetMenu (fileName = "Equip Inventary", menuName = "Zyan Assets/Create Equip Inventary")]
public class EquipInventaryOBJ : ScriptableObject
{
	public List<EquipObj> equipObj;
	public List<Zyan.EquipClass> Equip ;
	public List<Zyan.IdIndex> Data;
	
	void OnValidate() => LoadAll();
	
	[Button]
	public void LoadAll()
	{
		if (equipObj != null)
		{equipObj = new List<EquipObj>{};}
		var pg = Resources.LoadAll<EquipObj>("Scripts/Equip/");
		foreach (EquipObj p in pg){equipObj.Add(p); }
		Equip = new List<Zyan.EquipClass>{};
		Data = new List<Zyan.IdIndex>{};
		foreach (EquipObj a in equipObj)
		{
			a.LoadSelf();
			Equip.Add(a.Card);
			Data.Add(a.Data);
		}
	}
}
