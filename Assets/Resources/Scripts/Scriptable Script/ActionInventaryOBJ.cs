using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;
using UnityEditor;

[CreateAssetMenu (fileName = "Action Inventary", menuName = "Zyan Assets/Create Action Inventary")]
public class ActionInventaryOBJ : ScriptableObject
{
	public List<ActionObj> ActionObj;
	public List<Zyan.ActionClass> Action ;
	public List<Zyan.IdIndex> Data;
	
	void OnValidate() => LoadAll();
	
	[Button]
	public void LoadAll()
	{
		if (ActionObj != null)
		{ActionObj = new List<ActionObj>{};}
		var pg = Resources.LoadAll<ActionObj>("Scripts/Action/");
		foreach (ActionObj p in pg){ActionObj.Add(p); }
		Action = new List<Zyan.ActionClass>{};
		Data = new List<Zyan.IdIndex>{};
		foreach (ActionObj a in ActionObj)
		{
			a.LoadSelf();
			Action.Add(a.Card);
			Data.Add(a.Data);
		}
	}
}
