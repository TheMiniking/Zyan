using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class JsonFile2 : MonoBehaviour
{	
	public TextAsset txtJson;
	public int CountNodes;
	public int Index;
	public string firstName;
	public string lastName;
	public string gender;
	public int age;
	public string number;
	public List<PeopleList> people;
	
	[System.Serializable]
	public class PeopleList
	//public List<string> people;
	{
		public string firstName;
		public string lastName;
		public string gender;
		public int age;
		public string number;
	} 
	

	[System.Serializable]
	public class PlayerList
	{
		public PeopleList[] people; 
	}
	
	public PlayerList myPlayerList = new PlayerList();
	
	void Start()
	{
		//myPlayerList = JsonUtility.FromJson<PlayerList>(txtJson.text);
		myPlayerList = JsonUtility.FromJson<PlayerList>(txtJson.text);
		Index = 0;
		foreach (PeopleList x in myPlayerList.people)
		{
			CountNodes ++;
		}
		/*
		foreach (PeopleList x in myPlayerList.people)
		{
			Debug.Log(x.lastName);
			Debug.Log(x.number);
		}*/
		//Debug.Log(myPlayerList.people[2].lastName);
	}

	void Update()
	{
		
	}
	
	public void UpdateNodes()
	{
		firstName = myPlayerList.people[Index].firstName;
		lastName = myPlayerList.people[Index].lastName;
		gender = myPlayerList.people[Index].gender;
		age = myPlayerList.people[Index].age;
		number = myPlayerList.people[Index].number;
	}
	public string path = "Assets/sample4.txt";
	public void Save()
	{
		var Conteudo = JsonUtility.ToJson(this, true);
		File.WriteAllText(path, Conteudo);
	}
	
	public void Load()
	{	
		var Conteudo = File.ReadAllText(path);
		var X = JsonUtility.FromJson<JsonFile>(Conteudo);
		
		/*people = X.people;
		firstName = X.firstName;
		lastName = X.lastName;
		gender = X.gender;
		age = X.age;
		number = X.number;
		*/
	}	
}