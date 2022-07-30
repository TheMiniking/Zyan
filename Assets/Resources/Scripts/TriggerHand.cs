using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerHand : MonoBehaviour
{
	public GameObject _RepresentUnit;
	public Vector3 _originalPosition;
	
	void Start()
	{
		_originalPosition = _RepresentUnit.transform.localPosition;
	}
	
	void OnMouseEnter()
	{
		_RepresentUnit.LeanMoveLocal( _originalPosition + new Vector3 (0,40,0),0.25f);
		var z = FindObjectOfType<Manager_UI>();
		z.UpdateUI(_RepresentUnit);
		
	}
	
	void OnMouseExit()
	{
		_RepresentUnit.LeanMoveLocal(_originalPosition,0.25f);
	}
}