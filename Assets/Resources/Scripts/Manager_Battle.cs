using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Manager_Battle : MonoBehaviour
{
	public GameObject _Atacante;
	public GameObject _Defensor;
	public int _AtacanteATK;
	public int _AtacanteDEF;
	public int _AtacanteLife;
	public int _AtacanteOriginalLife;
	public int _DefensorATK;
	public int _DefensorDEF;
	public int _DefensorLife;
	public int _DefensorOriginalLife;
	public string effectWin;
	public Texture2D[] dice;
	public Texture2D[] coin;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
	public void Batalha(GameObject atacante,GameObject defensor)
	{
		_Atacante = atacante;
		_Defensor = defensor;
		var x = _Atacante.GetComponent<Unit>();
		var z = _Defensor.GetComponent<Unit>();
		_AtacanteATK = x._TotallAtk;
		_AtacanteDEF = x._TotalDef;
		_AtacanteLife = x._TotalLife;
		_AtacanteOriginalLife =x._originalLife;
		_DefensorATK = z._TotallAtk;
		_DefensorDEF = z._TotalDef;
		_DefensorLife = z._TotalLife;
		_DefensorOriginalLife = z._originalLife;
		effectWin = TesteForce();
		Debug.Log(effectWin);
		switch (effectWin)
		{
		case "I":
			if (_AtacanteATK > _DefensorATK)
			{
				var d = _AtacanteATK - _DefensorATK;
				Damage(z,d);
			}
			else if (_AtacanteATK < _DefensorATK)
			{
				var d2 = _DefensorATK - _AtacanteATK  ;
				Damage(x,d2);
			}
			else 
			{
				x._Terreno.EndMoveUnit();
			}
			break;
		case "II":
			if (_AtacanteATK > _DefensorDEF)
			{
				var d = _AtacanteATK - _DefensorDEF;
				Damage(z,d);
			}
			else if (_AtacanteATK < _DefensorDEF)
			{
				var d2 = _DefensorDEF - _AtacanteATK  ;
				Damage(x,d2);
			}
			else 
			{
				x._Terreno.EndMoveUnit();
			}
			break;
		case "III":
			if (_AtacanteATK > _DefensorLife)
			{
				var d = _AtacanteATK - _DefensorLife;
				Damage(z,d);
			}
			else if (_AtacanteATK < _DefensorLife)
			{
				var d2 = _DefensorLife - _AtacanteATK  ;
				Damage(x,d2);
			}
			else 
			{
				x._Terreno.EndMoveUnit();
			}
			break;
		case "IV":
			if (_AtacanteDEF > _DefensorATK)
			{
				var d = _AtacanteDEF - _DefensorATK;
				Damage(z,d);
			}
			else if (_AtacanteDEF < _DefensorATK)
			{
				var d2 = _DefensorATK - _AtacanteDEF  ;
				Damage(x,d2);
			}
			else 
			{
				x._Terreno.EndMoveUnit();
			}
			break;
		case "V":
			if (_AtacanteDEF > _DefensorDEF)
			{
				var d = _AtacanteDEF - _DefensorDEF;
				Damage(z,d);
			}
			else if (_AtacanteDEF < _DefensorDEF)
			{
				var d2 = _DefensorDEF - _AtacanteDEF  ;
				Damage(x,d2);
			}
			else 
			{
				x._Terreno.EndMoveUnit();
			}
			break;
		case "VI":
			if (_AtacanteDEF > _DefensorLife)
			{
				var d = _AtacanteDEF - _DefensorLife;
				Damage(z,d);
			}
			else if (_AtacanteDEF < _DefensorLife)
			{
				var d2 = _DefensorATK - _AtacanteLife  ;
				Damage(x,d2);
			}
			else 
			{
				x._Terreno.EndMoveUnit();
			}
			break;
		case "VII":
			if (_AtacanteLife > _DefensorATK)
			{
				var d = _AtacanteLife - _DefensorATK;
				Damage(z,d);
			}
			else if (_AtacanteLife < _DefensorATK)
			{
				var d2 = _DefensorATK - _AtacanteLife  ;
				Damage(x,d2);
			}
			else 
			{
				x._Terreno.EndMoveUnit();
			}
			break;
		case "VIII":
			if (_AtacanteLife > _DefensorDEF)
			{
				var d = _AtacanteLife - _DefensorDEF;
				Damage(z,d);
			}
			else if (_AtacanteLife < _DefensorDEF)
			{
				var d2 = _DefensorDEF - _AtacanteLife  ;
				Damage(x,d2);
			}
			else 
			{
				x._Terreno.EndMoveUnit();
			}
			break;
		case "IX":
			if (_AtacanteLife > _DefensorLife)
			{
				var d = _AtacanteLife - _DefensorLife;
				Damage(z,d);
			}
			else if (_AtacanteLife < _DefensorLife)
			{
				var d2 = _DefensorLife - _AtacanteLife  ;
				Damage(x,d2);
			}
			else 
			{
				x._Terreno.EndMoveUnit();
			}
			break;
		default :
			if (_AtacanteATK > _DefensorDEF)
			{
				var d = _AtacanteATK - _DefensorDEF;
				Damage(z,d);
			}
			else if (_AtacanteATK < _DefensorDEF)
			{
				var d2 = _DefensorDEF - _AtacanteATK  ;
				Damage(x,d2);
			}
			else 
			{
				x._Terreno.EndMoveUnit();
			}
			break;
			
		}
		
	}
	
	public void Damage(Unit unit, int damage)
	{
		var t1 = _Atacante.GetComponent<Unit>();
		var t_1 = t1._Terreno;
		var t2 = _Defensor.GetComponent<Unit>();
		var t_2 = t2._Terreno;
		// Dano adicional
		if ( t1._effect1 == "Spiked" || t1._effect2 == "Spiked")	
		{	t2._life --;
			if (t1._effect1 == "Vampirism" ||t1._effect2 == "Vampirism")
			{t1._life ++;}}	
		if ( t2._effect1 == "Spiked" || t2._effect2 == "Spiked")	
		{	t1._life --;
			if (t2._effect2 == "Vampirism" ||t2._effect2 == "Vampirism")
			{t2._life ++;}}
		// Dano batalha.	
		if (t1 != unit)
		{	
			if (t1._effect1 == "Double Arms" || t1._effect2 == "Double Arms")
			{ damage = 2 * damage ;}
			if ( t2._effect1 == "Shilded" || t2._effect2 == "Shilded")
			{damage = Mathf.RoundToInt( damage/2 );}
			if (t1._effect1 == "Vampirism" || t1._effect2 == "Vampirism")
			{t1._life = t1._life + damage;}
			// dano final
			unit._life = unit._life - damage ;
			// Effeitos pos batalha Eternal Bersek e Eternal Guardian
			if (t1._effect1 == "Eternal Bersek" || t1._effect2 == "Eternal Bersek")
			{	if (t1._atk <= t1._originalAtk * 2)
				{t1._atk ++ ;}}
			if (t1._effect1 == "Eternal Guardian" || t1._effect2 == "Eternal Guardian")
			{	if (t1._def <= t1._originalDef * 2)
				{t1._def ++ ;}}
			if (unit._life <= 0)
			{
				//unit._Terreno.AnimDeath();
				Debug.Log("movendo depois da batalha : " + t1._UnitOnField.GetComponent<UnitField>());
				SceneVariables_Battle._UnitToMove = t1._UnitOnField.GetComponent<UnitField>();
				t_2.MoveUnit();
			}else 
			{
				// Effeitos pos batalha Eternal Bersek e Eternal Guardian
				if (t2._effect1 == "Eternal Bersek" || t2._effect2 == "Eternal Bersek")
				{	if (t2._atk <= t2._originalAtk * 2)
					{t2._atk ++ ;}}
				if (t2._effect1 == "Eternal Guardian" || t2._effect2 == "Eternal Guardian")
				{	if (t2._def <= t2._originalDef * 2)
					{t2._def ++ ;}}
				t_1.EndMoveUnit();
			}
		}else 
		{	if (t2._effect1 == "Double Arms" || t2._effect2 == "Double Arms")
			{ damage = 2 * damage ;}
			if ( t1._effect1 == "Shilded" || t1._effect2 == "Shilded")
			{damage =  Mathf.RoundToInt(damage/2);}
			if (t2._effect1 == "Vampirism" ||t2._effect2 == "Vampirism")
			{t2._life = t1._life + damage; }
			// daano final
			unit._life = unit._life - damage ;
			// Effeitos pos batalha Eternal Bersek e Eternal Guardian
			if (t1._effect1 == "Eternal Bersek" || t1._effect2 == "Eternal Bersek")
			{	if (t1._atk <= t1._originalAtk * 2)
				{t1._atk ++ ;}}
			if (t1._effect1 == "Eternal Guardian" || t1._effect2 == "Eternal Guardian")
			{	if (t1._def <= t1._originalDef * 2)
				{t1._def ++ ;}}
			if (t2._effect1 == "Eternal Bersek" || t2._effect2 == "Eternal Bersek")
			{	if (t2._atk <= t2._originalAtk * 2)
				{t2._atk ++ ;}}
			if (t2._effect1 == "Eternal Guardian" || t2._effect2 == "Eternal Guardian")
			{	if (t2._def <= t2._originalDef * 2)
				{t2._def ++ ;}}
			t_1.EndMoveUnit();
		}
		if (t1._status == "Poisonous" || t1._status == "Flame" ||t1._status == "Send Sleep" || t1._status == "Eletrocute" || t1._status == "Overdo")
		{
			var status = PlayStatus();
			if (status[0] == "Active")
			{
				switch (t1._status)
				{
				case "Poisonous":
					t2._OnStatus = "Poison";
					t2._OnStatusTurn = int.Parse(status[1]);
					break;
				case "Flame":
					t2._OnStatus = "Burn";
					t2._OnStatusTurn = int.Parse(status[1]);
					break;
				case "Send Sleep":
					t2._OnStatus = "Sleep";
					t2._OnStatusTurn = int.Parse(status[1]);
					break;
				case "Eletrocute":
					t2._OnStatus = "Paralize";
					t2._OnStatusTurn = int.Parse(status[1]);
					break;
				case "Overdo":
					t2._OnStatus = "Fadige";
					t2._OnStatusTurn = int.Parse(status[1]);
					break;
				}
			}
		}else if (t2._status == "Poisonous" || t2._status == "Flame" ||t2._status == "Send Sleep" || t2._status == "Eletrocute" || t2._status == "Overdo")
		{
			var status = PlayStatus();
			if (status[0] == "Active")
			{
				switch (t2._status)
				{
				case "Poisonous":
					t1._OnStatus = "Poison";
					t1._OnStatusTurn = int.Parse(status[1]);
					break;
				case "Flame":
					t1._OnStatus = "Burn";
					t1._OnStatusTurn = int.Parse(status[1]);
					break;
				case "Send Sleep":
					t1._OnStatus = "Sleep";
					t1._OnStatusTurn = int.Parse(status[1]);
					break;
				case "Eletrocute":
					t1._OnStatus = "Paralize";
					t1._OnStatusTurn = int.Parse(status[1]);
					break;
				case "Overdo":
					t1._OnStatus = "Fadige";
					t1._OnStatusTurn = int.Parse(status[1]);
					break;
				}
			}
		}
	}
	
	private List<string> super_a = new List<string>{};
	private List<string> super_d = new List<string>{};
	public string TesteForce()
	{
		var a = _Atacante.GetComponent<Unit>();
		var d = _Defensor.GetComponent<Unit>();
		var d_eff1 = d._effect1;
		var d_eff2 = d._effect2;
		var a_eff1 = a._effect1;
		var a_eff2 = a._effect2;
		var x = "";
		var a_val = "";
		var d_val = "";
		super_a = new List<string>{};
		super_d = new List<string>{};
		super_a.Add(a_eff1);
		super_a.Add(a_eff2);
		super_d.Add(d_eff1);
		super_d.Add(d_eff2);
		foreach (string s in super_d)
		{
			switch (s)
			{
			case "Aggressor":
				d_val = "ATK";
				break;
			case "Defender":
				d_val = "DEF";
				break;
			case "Power Astral":
				d_val = "Life";
				break;
			}
		}
		foreach (string s in super_a)
		{
			switch (s)
			{
			case "Aggressor":
				a_val = "ATK";
				break;
			case "Defender":
				a_val = "DEF";
				break;
			case "Power Astral":
				a_val = "Life";
				break;
			}
		}
		foreach (string s in super_d)
		{
			switch (s)
			{
			case "Anti-Aggressor":
				a_val = "Life";
				break;
			case "Anti-Defender":
				a_val = "ATK" ;
				break;
			case "Anti-Astral":
				a_val = "DEF";
				break;
			}
		}
		foreach (string s in super_d)
		{
			switch (s)
			{
			case "Brute Force":
				a_val = "ATK";
				d_val = "ATK";
				break;
			case "Control Force":
				a_val = "DEF";
				d_val = "DEF";
				break;
			case "Astral Force":
				a_val = "Life";
				d_val = "Life";
				break;
			}
		}
		foreach (string s in super_a)
		{
			switch (s)
			{
			case "Brute Force":
				a_val = "ATK";
				d_val = "ATK";
				break;
			case "Control Force":
				a_val = "DEF";
				d_val = "DEF";
				break;
			case "Astral Force":
				a_val = "Life";
				d_val = "Life";
				break;
			}
		}
		if (a_val == "ATK" && d_val == "ATK")
		{
			x = "I";
		}else if (a_val == "ATK" && d_val == "DEF")
		{
			x = "II";
		}else if (a_val == "ATK" && d_val == "Life")
		{
			x = "III";
		}else if (a_val == "DEF" && d_val == "ATK")
		{
			x = "IV";
		}else if (a_val == "DEF" && d_val == "DEF")
		{
			x = "V";
		}else if (a_val == "DEF" && d_val == "Life")
		{
			x = "VI";
		}else if (a_val == "Life" && d_val == "ATK")
		{
			x = "VII";
		}else if (a_val == "Life" && d_val == "DEF")
		{
			x = "VIII";
		}else if (a_val == "Life" && d_val == "Life")
		{
			x = "IX";
		}else if ( a_val == "" && d_val == "ATK")
		{
			x = "I";
		}else if ( a_val == ""  && d_val == "DEF")
		{
			x = "II";
		}else if ( a_val == ""  && d_val == "Life")
		{
			x = "III";
		}else if (a_val == "ATK" && d_val == "" )
		{
			x = "II";
		}else if (a_val == "DEF" && d_val == "")
		{
			x = "V";
		}else if (a_val == "Life" && d_val == "")
		{
			x = "VIII";
		}else
		{
			x = "";
		}
		//Debug.Log(a_val + " Vs " + d_val);
		return x;
	}
	
	private List<string> diceTurn = new List<string>{};
	public List<string> PlayStatus()
	{
		diceTurn = new List<string>{};
		var coin = Random.Range(0 , 2);
		switch(coin)
		{//cara - ativa efeito
		case 0 :
			var dice = Random.Range(1,7);
			diceTurn.Add( "Active");
			diceTurn.Add(dice.ToString());
			break;
		case 1 :
			diceTurn.Add("No");
			diceTurn.Add("0");
			break;
		} 
		Debug.Log( diceTurn[0] + " " + diceTurn[1] );
		return diceTurn;
	}
}
