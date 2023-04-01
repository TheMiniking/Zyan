using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuConf : MonoBehaviour
{
	public AudioSource music;
	public float musicVol;
	public bool musicOn = true;
	public Slider musicSlider;
	public Toggle musicToggle;
	public AudioSource effect;
	public float effectVol;
	public bool effectOn = true;
	public Slider effectSlider;
	public Toggle effectToggle;
	public GameObject Pause;
	public TMP_Text effvol;
	public TMP_Text musvol;
	
    // Start is called before the first frame update
    void Start()
	{
		if (PlayerPrefs.HasKey("musicVol")) {musicVol = PlayerPrefs.GetFloat("musicVol");}
		else {	PlayerPrefs.SetFloat("musicVol", 1);
			musicVol = PlayerPrefs.GetFloat("musicVol");}
		if (PlayerPrefs.HasKey("effectVol")) {effectVol = PlayerPrefs.GetFloat("effectVol");}
		else {	PlayerPrefs.SetFloat("effectVol", 1);
			effectVol = PlayerPrefs.GetFloat("effectVol");}
		if (PlayerPrefs.HasKey("effectOn")) 
		{	if (PlayerPrefs.GetInt("effectOn") == 1) {effectOn = true;}
				else {effectOn = false;}}
		else {	PlayerPrefs.SetInt("effectOn", 1);
			effectOn = true;}
		if (PlayerPrefs.HasKey("musicOn")) 
		{	if (PlayerPrefs.GetInt("musicOn") == 1) {musicOn = true;}
		else {musicOn = false;}}
		else {	PlayerPrefs.SetInt("musicOn", 1);
			musicOn = true;}
		if (musicOn){music.volume = musicVol;
			musicSlider.value = musicVol;
			musicToggle.isOn = true;}
		else {music.volume = 0;
			musicSlider.value = musicVol;
			musicToggle.isOn = false;}
		if (effectOn){effect.volume = effectVol;
			effectSlider.value = effectVol;
			effectToggle.isOn = true;}
		else {effect.volume = 0;
			effectSlider.value = effectVol;
			effectToggle.isOn = false;}		
    }

    // Update is called once per frame
    void Update()
	{
		if (Pause.activeSelf){
			musicOn = musicToggle.isOn;
			musicVol = musicSlider.value;
			if (musicOn){music.volume = musicVol;}
			else {music.volume = 0;}
			effectOn = effectToggle.isOn;
			effectVol = effectSlider.value;
			if (effectOn){effect.volume = effectVol;}
			else {effect.volume = 0;}
			PlayerPrefs.SetFloat("musicVol", musicVol);
			PlayerPrefs.SetFloat("effectVol", effectVol);
			if (musicOn) {PlayerPrefs.SetInt("musicOn", 1);}
			else {PlayerPrefs.SetInt("musicOn", 0);}
			if (effectOn) {PlayerPrefs.SetInt("effectOn", 1);}
			else {PlayerPrefs.SetInt("effectOn", 0);}
			musvol.text = ""+ Mathf.RoundToInt( musicVol * 100);
			effvol.text = ""+ Mathf.RoundToInt(effectVol * 100);
		}
    }
    
	
    
}
