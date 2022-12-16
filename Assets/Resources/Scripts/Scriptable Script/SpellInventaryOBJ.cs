using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;
using UnityEditor;

[CreateAssetMenu (fileName = "Spell Inventary", menuName = "Zyan Assets/Create Spell Inventary")]
public class SpellInventaryOBJ : ScriptableObject
{
	public List<SpellObj> spellObj;
	public List<Zyan.SpellClass> Spell ;
	public List<Zyan.IdIndex> Data;
	
	void OnValidate() => LoadAll();
	
	[Button]
	public void LoadAll()
	{
		if (spellObj != null)
		{spellObj = new List<SpellObj>{};}
		var pg = Resources.LoadAll<SpellObj>("Scripts/Spell/");
		foreach (SpellObj p in pg){spellObj.Add(p); }
		Spell = new List<Zyan.SpellClass>{};
		Data = new List<Zyan.IdIndex>{};
		foreach (SpellObj a in spellObj)
		{
			a.LoadSelf();
			Spell.Add(a.Card);
			Data.Add(a.Data);
		}
	}
}
