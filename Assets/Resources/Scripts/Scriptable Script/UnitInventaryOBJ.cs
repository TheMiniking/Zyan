using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;
using UnityEditor;

[CreateAssetMenu (fileName = "Unit Inventary", menuName = "Zyan Assets/Create Unit Inventary")]
public class UnitInventaryOBJ : ScriptableObject
{
	public List<UnitObj> unitObj;
	public List<Zyan.UnitClass> unit ;
	public List<Zyan.IdIndex> Data;
	
	void OnValidate() => LoadAll();
	
	[Button]
	public void LoadAll()
	{
		if (unitObj != null)
		{unitObj= new List<UnitObj>{};}
		var pg = Resources.LoadAll<UnitObj>("Scripts/Unit/");
		foreach (UnitObj p in pg){unitObj.Add(p);}
		unit = new List<Zyan.UnitClass>{};
		Data = new List<Zyan.IdIndex>{};
		foreach (UnitObj a in unitObj)
		{
			a.LoadSelf();
			unit.Add(a.Card);
			Data.Add(a.Data);
		}
	}
}
