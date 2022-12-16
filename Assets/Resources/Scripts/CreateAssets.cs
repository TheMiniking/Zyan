using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if (UNITY_EDITOR)

public static class CreateAssets
{
	// Cria os AssetsObjects com base no Json
	public static void CreateAsset(string type)
	{
		switch (type)
		{
		case "Card":
			var c = JsonReader.ReadListFromJSONCard();
			var indexI = 0;
			foreach (var i in c)
			{
				UnitObj cI = ScriptableObject.CreateInstance<UnitObj>();
				AssetDatabase.CreateAsset( cI , "Assets/Resources/Scripts/Unit/"+i.id+".asset");
				indexI++;
			}
			break;
			
		case "Action":
			var a = JsonReader.ReadListFromJSONAction();
			var indexII = 0;
			foreach (var i in a)
			{
				ActionObj cII = ScriptableObject.CreateInstance<ActionObj>();
				AssetDatabase.CreateAsset( cII , "Assets/Resources/Scripts/Action/"+i.id+".asset");
				indexII++;
			}
			break;
		case "Equip":
			var e = JsonReader.ReadListFromJSONEquip();
			var indexIII = 0;
			foreach (var i in e)
			{
				EquipObj cIII = ScriptableObject.CreateInstance<EquipObj>();
				AssetDatabase.CreateAsset( cIII , "Assets/Resources/Scripts/Equip/"+i.id+".asset");
				indexIII++;
			}
			break;
			
		case "Spell":
			var s = JsonReader.ReadListFromJSONSpellTrap();
			var indexIV = 0;
			foreach (var i in s)
			{
				SpellObj cIV = ScriptableObject.CreateInstance<SpellObj>();
				AssetDatabase.CreateAsset( cIV , "Assets/Resources/Scripts/Spell/"+i.id+".asset");
				indexIV++;
			}
			break;
		}
		
	}
}
#endif