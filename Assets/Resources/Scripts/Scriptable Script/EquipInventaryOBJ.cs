using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;
using UnityEditor;

[CreateAssetMenu (fileName = "Equip Inventary", menuName = "Zyan Assets/Create Equip Inventary")]
public class EquipInventaryOBJ : ScriptableObject
{
	public EquipObj[] equipObj;
	public Zyan.EquipClass[] Equip ;
	public Zyan.IdIndex[] Data;
	
	void OnValidate() => LoadAll();
	
	[Button]
	public void LoadAll()
	{
		if (equipObj != null)
		{ArrayUtility.Clear<EquipObj>(ref equipObj);}
		equipObj = Resources.LoadAll<EquipObj>("Scripts/Equip/");
		Equip = new Zyan.EquipClass[0];
		Data = new Zyan.IdIndex[0];
		foreach (EquipObj a in equipObj)
		{
			a.LoadSelf();
			ArrayUtility.Add<Zyan.EquipClass>(ref Equip , a.Card);
			ArrayUtility.Add<Zyan.IdIndex>(ref Data , a.Data);
		}
	}
}
