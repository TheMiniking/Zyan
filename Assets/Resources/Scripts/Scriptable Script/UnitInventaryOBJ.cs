using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;
using UnityEditor;

[CreateAssetMenu (fileName = "Unit Inventary", menuName = "Zyan Assets/Create Unit Inventary")]
public class UnitInventaryOBJ : ScriptableObject
{
	public UnitObj[] unitObj;
	public Zyan.UnitClass[] unit ;
	public Zyan.IdIndex[] Data;
	
	void OnValidate() => LoadAll();
	
	[Button]
	public void LoadAll()
	{
		if (unitObj != null)
		{ArrayUtility.Clear<UnitObj>(ref unitObj);}
		unitObj = Resources.LoadAll<UnitObj>("Scripts/Unit/");
		unit = new Zyan.UnitClass[0];
		Data = new Zyan.IdIndex[0];
		foreach (UnitObj a in unitObj)
		{
			a.LoadSelf();
			ArrayUtility.Add<Zyan.UnitClass>(ref unit , a.Card);
			ArrayUtility.Add<Zyan.IdIndex>(ref Data , a.Data);
		}
	}
}
