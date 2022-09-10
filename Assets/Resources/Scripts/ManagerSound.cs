using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class ManagerSound : MonoBehaviour
{
	[ShowInInspector] public AudioSource audio;
	[ShowInInspector] public AudioSource playerMusica;
	[ShowInInspector] public AudioClip musica;
	[ShowInInspector] public AudioClip[] musicList;
	[ShowInInspector] public AudioClip click;
	[ShowInInspector] public AudioClip clickUI;
	[ShowInInspector] public AudioClip active;
	[ShowInInspector] public AudioClip explosao;
	
	void Start(){
		LoadMusicList();
		LoadMusic();
	}
	
	public void LoadMusicList() => musicList = Resources.LoadAll<AudioClip>("Music");
	
	[Button]
	public void LoadMusic() { 
		musica = musicList[Random.Range(0,musicList.Length)];
		playerMusica.clip = musica;
		playerMusica.Play();}
}
