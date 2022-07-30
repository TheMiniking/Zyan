using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;
using UnityEditor;

[CreateAssetMenu (fileName = "Spell Inventary", menuName = "Zyan Assets/Create Spell Inventary")]
public class SpellInventaryOBJ : ScriptableObject
{
	public SpellObj[] spellObj;
	public Zyan.SpellClass[] Spell ;
	public Zyan.IdIndex[] Data;
	
	void OnValidate() => LoadAll();
	
	[Button]
	public void LoadAll()
	{
		if (spellObj != null)
		{ArrayUtility.Clear<SpellObj>(ref spellObj);}
		spellObj = Resources.LoadAll<SpellObj>("Scripts/Spell/");
		Spell = new Zyan.SpellClass[0];
		Data = new Zyan.IdIndex[0];
		foreach (SpellObj a in spellObj)
		{
			a.LoadSelf();
			ArrayUtility.Add<Zyan.SpellClass>(ref Spell , a.Card);
			ArrayUtility.Add<Zyan.IdIndex>(ref Data , a.Data);
		}
	}
}
