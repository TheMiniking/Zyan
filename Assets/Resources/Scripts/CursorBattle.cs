using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorBattle : MonoBehaviour
{
	public GameObject HitObj;
	public float z = -1.2f;
    // Update is called once per frame
	void FixedUpdate(){
		//mouse
		Vector3 mouse = Input.mousePosition;
		mouse.z = 100f;
		mouse = Camera.main.ScreenToWorldPoint(mouse);
		Debug.DrawRay(transform.position, mouse - transform.position, Color.yellow);
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(ray,out hit,100)){
			HitObj = HitObj!=hit.transform.gameObject? hit.transform.gameObject : HitObj;
			var hitv3 = HitObj.transform.position;
			hitv3.y = z;
			LeanTween.move(this.gameObject, hitv3, 0.1f);
		}
		Debug.DrawRay(this.transform.position, this.transform.TransformDirection(Vector3.forward)* 1f, Color.blue);
		if (Physics.Raycast(this.transform.position, this.transform.TransformDirection(Vector3.forward), out RaycastHit hitinfo, 1f))
		{
			var down = hitinfo.transform.gameObject;
			if (down.TryGetComponent<Terreno>(out Terreno t)){
				t.CheckSpell();
				GetComponent<SpriteRenderer>().color = Color.white;
			}
			else{
				var down2 =down.GetComponent<UnitField>();
				down2.SendToUI();
				if(down2._isplayer){GetComponent<SpriteRenderer>().color = Color.green;}
				else{GetComponent<SpriteRenderer>().color = Color.red;}
			}
			//Debug.Log("Terreno ou unit abaixo: "+ hitinfo.transform.gameObject.name);
		}
		else {//Debug.Log("Nada abaixo :P");
		}
    }
}
