using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Events;
using System.Reflection;

public class MapEvents : MonoBehaviour
{
	public delegate void Events(); 			// Variavel Master do Evento
	public event Events MovimentoDeTela;
	[ShowInInspector] public UnityEvent Save;
	[ShowInInspector] public UnityEvent Load;
	
	
	protected void OnDisable(){
		Save.RemoveAllListeners();
		Load.RemoveAllListeners();
		
	}
   
}
