using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Superteste : MonoBehaviour
{
	// JSON FILES
	public TextAsset JsonCard;
	public TextAsset JsonActionCard;
	public TextAsset JsonSpellCard;
	public TextAsset JsonEquipCard;
	public TextAsset JsonDialogo;
	public TextAsset txtJson;
	private Zyan.CardList[] v;
	public Zyan.PlayerInventary player = new Zyan.PlayerInventary(){};
	
	void Start()
	{
		/*player.Name = "Miniking";
		player.ID = "18759962";
		player.iTesouros.coin = 1000;
		player.iTesouros.gem = 50;
		player.iTesouros.materialC = 40;
		player.iTesouros.materialUC = 10;
		player.iTesouros.materialR = 70;
		player.iTesouros.materialE = 50;
		player.iTesouros.materialL = 40;
		player.iTesouros.upgradeCuponsC = 50;
		player.iTesouros.upgradeCuponsUC = 10;
		player.iTesouros.upgradeCuponsR = 30;
		player.iTesouros.upgradeCuponsE = 70;
		player.iTesouros.upgradeCuponsL = 20;
		ArrayUtility.Add<int>(ref player.iModifications.id, 5);
		ArrayUtility.Add<int>(ref player.iModifications.newBuffATK, 0);
		ArrayUtility.Add<int>(ref player.iModifications.newBuffDEF, 1);
		ArrayUtility.Add<int>(ref player.iModifications.newBuffLife, 0);
		ArrayUtility.Add<string>(ref player.iModifications.newModificationLocal, "Effect1");
		ArrayUtility.Add<string>(ref player.iModifications.newModificarionEffect, "Aggressor");
		ArrayUtility.Add<string>(ref player.iModifications.newRarity, "Rare");*/
		SceneVariables_Battle.playerData = player;
		v =	GetJson.FromJsonCards(JsonCard.text);
		JsonReader.SaveToJSONCards(v, "Units.mk");
	}
	public void LoadPlayer(string z)
	{
		var d = JsonReader.ReadFromJSONPlayer( z);
		Debug.Log(d.Name);
	}
	
	public void LoadCards(string z)
	{
		var d = JsonReader.ReadListFromJSONCard( z);
		Debug.Log(d.Length);
	}
	
	public void SaveClick()
	{
		JsonReader.SaveToJSONPlayer ( player ,"player.mk");
	}
	
}
