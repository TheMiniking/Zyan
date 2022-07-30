using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;
using UnityEditor;

[CreateAssetMenu (fileName = "Action Inventary", menuName = "Zyan Assets/Create Action Inventary")]
public class ActionInventaryOBJ : ScriptableObject
{
	public ActionObj[] ActionObj;
	public Zyan.ActionClass[] Action ;
	public Zyan.IdIndex[] Data;
	
	void OnValidate() => LoadAll();
	
	[Button]
	public void LoadAll()
	{
		if (ActionObj != null)
		{ArrayUtility.Clear<ActionObj>(ref ActionObj);}
		ActionObj = Resources.LoadAll<ActionObj>("Scripts/Action/");
		Action = new Zyan.ActionClass[0];
		Data = new Zyan.IdIndex[0];
		foreach (ActionObj a in ActionObj)
		{
			a.LoadSelf();
			ArrayUtility.Add<Zyan.ActionClass>(ref Action , a.Card);
			ArrayUtility.Add<Zyan.IdIndex>(ref Data , a.Data);
		}
	}
}
