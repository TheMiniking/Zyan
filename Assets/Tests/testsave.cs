using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testsave : MonoBehaviour
{
	public PlayerInventaryOBJ player;
	
	public void save()=> SaveLoad.Save(player);
	public void load()=> player.Player = SaveLoad.Load();
}
