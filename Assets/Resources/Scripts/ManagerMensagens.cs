using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class ManagerMensagens : MonoBehaviour
{
	// Grafico
	public Image _LBigChar;
	public Image _RBigChar;
	public Image _LChar;
	public Image _RChar;
	public TMP_Text _Name;
	public TMP_Text _Dialogo;
	
    // Start is called before the first frame update
    void Start()
    {
	    //AutoMensagem( 0 , "Miniking" , "35");// teste
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
	public bool mensageOn =false;
	public bool proxima = false;
	public int blocoProv;
	public string nameProv; 
	public int index = 0;
	public string imageNameProv;
	public bool inLeftProv;
	public bool useBigProv;
	private int mode;
	public void AutoMensagem(int bloco , string name)
	{
		mode = 1;
		var fala = JsonReader.ReadListFromJSONDialogo();
		blocoProv = bloco;
		nameProv = name;
		switch (index)
		{
		case 0:
			if (fala[bloco].A0 != "")
			{
				ShowMensagem(fala[bloco].A0, name);
				index++;
				StartCoroutine(NextMensagem());
				break;
			}else 
			{
				index++;
				proxima = true;
				break;
			}
		case 1:
			if (fala[bloco].A1 != "")
			{
				ShowMensagem(fala[bloco].A1, name);
				index++;
				StartCoroutine(NextMensagem());
				break;
			}else 
			{
				index++;
				proxima = true;
				break;
			}
		case 2:
			if (fala[bloco].A2 != "")
		{
				ShowMensagem(fala[bloco].A2, name);
			index++;
			StartCoroutine(NextMensagem());
			break;
		}else 
		{
			index++;
			proxima = true;
			break;
		}
		case 3:
			if (fala[bloco].A3 != "")
			{
				ShowMensagem(fala[bloco].A3, name);
		index++;
		StartCoroutine(NextMensagem());
		break;
		}else 
			{
				index++;
				proxima = true;
				break;
			}
		case 4:
			if (fala[bloco].A4 != "")
			{
				ShowMensagem(fala[bloco].A4, name);
				index++;
				StartCoroutine(NextMensagem());
				break;
			}else 
			{
				index++;
				proxima = true;
				break;
			}
		case 5:
			if (fala[bloco].A5 != "")
			{
				ShowMensagem(fala[bloco].A5, name);
				index++;
				StartCoroutine(NextMensagem());
				break;
			}else 
			{
				index++;
				proxima = true;
				break;
			}
		case 6:
			if (fala[bloco].A6 != "")
			{
				ShowMensagem(fala[bloco].A6, name);
				index++;
				StartCoroutine(NextMensagem());
				break;
			}else 
			{
				index++;
				proxima = true;
				break;
			}
		case 7:
			if (fala[bloco].A7 != "")
			{
				ShowMensagem(fala[bloco].A7, name);
				index++;
				StartCoroutine(NextMensagem());
				break;
			}else 
			{
				index++;
				proxima = true;
				break;
			}
		case 8:
			if (fala[bloco].A8 != "")
			{
				ShowMensagem(fala[bloco].A8, name);
				index++;
				StartCoroutine(NextMensagem());
				break;
			}else 
			{
				index++;
				proxima = true;
				break;
			}
		case 9:
			if (fala[bloco].A9 != "")
			{
				ShowMensagem(fala[bloco].A9, name);
				index++;
				StartCoroutine(NextMensagem());
				break;
			}else 
			{
				index++;
				proxima = true;
				break;
			}
		case 10:
			if (fala[bloco].A10 != "")
			{
				ShowMensagem(fala[bloco].A10, name);
				index++;
				StartCoroutine(NextMensagem());
				break;
			}else 
			{
				index++;
				proxima = true;
				break;
			}
		case 11:
			Object.Destroy(this.gameObject);
			//return true;
			break;
		}
		
		
	}
	
	public void AutoMensagem(int bloco , string name, string imgName)
	{
		mode = 2;
		var fala = JsonReader.ReadListFromJSONDialogo();
		blocoProv = bloco;
		nameProv = name;
		imageNameProv = imgName;
		switch (index)
		{
		case 0:
			if (fala[bloco].A0 != "")
			{
				ShowMensagem(fala[bloco].A0, imgName , name);
				index++;
				StartCoroutine(NextMensagem());
				break;
			}else 
			{
				index++;
				proxima = true;
				break;
			}
		case 1:
			if (fala[bloco].A1 != "")
			{
				ShowMensagem(fala[bloco].A1, imgName, name);
				index++;
				StartCoroutine(NextMensagem());
				break;
			}else 
			{
				index++;
				proxima = true;
				break;
			}
		case 2:
			if (fala[bloco].A2 != "")
			{
				ShowMensagem(fala[bloco].A2, imgName, name);
				index++;
				StartCoroutine(NextMensagem());
				break;
			}else 
			{
				index++;
				proxima = true;
				break;
			}
		case 3:
			if (fala[bloco].A3 != "")
			{
				ShowMensagem(fala[bloco].A3, imgName, name);
				index++;
				StartCoroutine(NextMensagem());
				break;
			}else 
			{
				index++;
				proxima = true;
				break;
			}
		case 4:
			if (fala[bloco].A4 != "")
			{
				ShowMensagem(fala[bloco].A4, imgName, name);
				index++;
				StartCoroutine(NextMensagem());
				break;
			}else 
			{
				index++;
				proxima = true;
				break;
			}
		case 5:
			if (fala[bloco].A5 != "")
			{
				ShowMensagem(fala[bloco].A5, imgName, name);
				index++;
				StartCoroutine(NextMensagem());
				break;
			}else 
			{
				index++;
				proxima = true;
				break;
			}
		case 6:
			if (fala[bloco].A6 != "")
			{
				ShowMensagem(fala[bloco].A6, imgName, name);
				index++;
				StartCoroutine(NextMensagem());
				break;
			}else 
			{
				index++;
				proxima = true;
				break;
			}
		case 7:
			if (fala[bloco].A7 != "")
			{
				ShowMensagem(fala[bloco].A7, imgName, name);
				index++;
				StartCoroutine(NextMensagem());
				break;
			}else 
			{
				index++;
				proxima = true;
				break;
			}
		case 8:
			if (fala[bloco].A8 != "")
			{
				ShowMensagem(fala[bloco].A8, imgName, name);
				index++;
				StartCoroutine(NextMensagem());
				break;
			}else 
			{
				index++;
				proxima = true;
				break;
			}
		case 9:
			if (fala[bloco].A9 != "")
			{
				ShowMensagem(fala[bloco].A9, imgName, name);
				index++;
				StartCoroutine(NextMensagem());
				break;
			}else 
			{
				index++;
				proxima = true;
				break;
			}
		case 10:
			if (fala[bloco].A10 != "")
			{
				ShowMensagem(fala[bloco].A10, imgName, name);
				index++;
				StartCoroutine(NextMensagem());
				break;
			}else 
			{
				index++;
				proxima = true;
				break;
			}
		case 11:
			Object.Destroy(this.gameObject);
			//return true;
			break;
		}
	}
	
	public void AutoMensagem(int bloco , string name, string imgName, bool isLeft,bool useBig)
	{
		mode = 3;
		var fala = JsonReader.ReadListFromJSONDialogo();
		blocoProv = bloco;
		nameProv = name;
		imageNameProv = imgName;
		useBigProv = useBig;
		inLeftProv = isLeft;
		switch (index)
		{
		case 0:
			if (fala[bloco].A0 != "")
			{
				ShowMensagem(fala[bloco].A0, imageNameProv , name, isLeft ,useBig );
				index++;
				StartCoroutine(NextMensagem());
				break;
			}else 
			{
				index++;
				proxima = true;
				break;
			}
		case 1:
			if (fala[bloco].A1 != "")
			{
				ShowMensagem(fala[bloco].A1, imageNameProv, name, isLeft ,useBig);
				index++;
				StartCoroutine(NextMensagem());
				break;
			}else 
			{
				index++;
				proxima = true;
				break;
			}
		case 2:
			if (fala[bloco].A2 != "")
			{
				ShowMensagem(fala[bloco].A2, imageNameProv, name, isLeft ,useBig);
				index++;
				StartCoroutine(NextMensagem());
				break;
			}else 
			{
				index++;
				proxima = true;
				break;
			}
		case 3:
			if (fala[bloco].A3 != "")
			{
				ShowMensagem(fala[bloco].A3, imageNameProv, name, isLeft ,useBig);
				index++;
				StartCoroutine(NextMensagem());
				break;
			}else 
			{
				index++;
				proxima = true;
				break;
			}
		case 4:
			if (fala[bloco].A4 != "")
			{
				ShowMensagem(fala[bloco].A4, imageNameProv, name, isLeft ,useBig);
				index++;
				StartCoroutine(NextMensagem());
				break;
			}else 
			{
				index++;
				proxima = true;
				break;
			}
		case 5:
			if (fala[bloco].A5 != "")
			{
				ShowMensagem(fala[bloco].A5, imageNameProv, name, isLeft ,useBig);
				index++;
				StartCoroutine(NextMensagem());
				break;
			}else 
			{
				index++;
				proxima = true;
				break;
			}
		case 6:
			if (fala[bloco].A6 != "")
			{
				ShowMensagem(fala[bloco].A6, imageNameProv, name, isLeft ,useBig);
				index++;
				StartCoroutine(NextMensagem());
				break;
			}else 
			{
				index++;
				proxima = true;
				break;
			}
		case 7:
			if (fala[bloco].A7 != "")
			{
				ShowMensagem(fala[bloco].A7, imageNameProv, name, isLeft ,useBig);
				index++;
				StartCoroutine(NextMensagem());
				break;
			}else 
			{
				index++;
				proxima = true;
				break;
			}
		case 8:
			if (fala[bloco].A8 != "")
			{
				ShowMensagem(fala[bloco].A8, imageNameProv, name, isLeft ,useBig);
				index++;
				StartCoroutine(NextMensagem());
				break;
			}else 
			{
				index++;
				proxima = true;
				break;
			}
		case 9:
			if (fala[bloco].A9 != "")
			{
				ShowMensagem(fala[bloco].A9, imageNameProv, name, isLeft ,useBig);
				index++;
				StartCoroutine(NextMensagem());
				break;
			}else 
			{
				index++;
				proxima = true;
				break;
			}
		case 10:
			if (fala[bloco].A10 != "")
			{
				ShowMensagem(fala[bloco].A10, imageNameProv, name, isLeft ,useBig);
				index++;
				StartCoroutine(NextMensagem());
				break;
			}else 
			{
				index++;
				proxima = true;
				break;
			}
		case 11:
			Object.Destroy(this.gameObject);
			//return true;
			break;
		}
	}
	
	public void Accept(InputAction.CallbackContext context)
	{
		if (context.performed)
		{
			//Debug.Log("proxima mensagem...");
			proxima = true;
		}
	}
    
	public IEnumerator NextMensagem()
	{
		mensageOn = true;
		yield return new WaitForSecondsRealtime (1f);
		yield return new WaitUntil (() => proxima == true);
		switch (mode)
		{
		case 1:
			AutoMensagem(blocoProv,nameProv);
			mensageOn= false;
			proxima =false;
			break;
		case 2:
			AutoMensagem(blocoProv,nameProv, imageNameProv);
			mensageOn= false;
			proxima =false;
			break;
		case 3:
			AutoMensagem(blocoProv,nameProv, imageNameProv,inLeftProv,useBigProv);
			mensageOn= false;
			proxima =false;
			break;
		}
	}
	
	public void ShowMensagem(string mensagem, string name )
	{
		_Name.text = name;
		_Dialogo.text = mensagem;
		_LBigChar.gameObject.SetActive(false);
		_RChar.gameObject.SetActive(false);
		_LChar.gameObject.SetActive(false);
		_RBigChar.gameObject.SetActive(false);
	}
    
	public void ShowMensagem(string mensagem, string leftCharImg, string name )
	{
		_Name.text = name;
		_Dialogo.text = mensagem;
		_LBigChar.gameObject.SetActive(false);
		_RChar.gameObject.SetActive(false);
		_LChar.gameObject.SetActive(true);
		_RBigChar.gameObject.SetActive(false);
		_LChar.sprite = Resources.Load<Sprite>("Imagens/UI/"+leftCharImg);
	}
	
	public void ShowMensagem(string mensagem, string CharImg, string name, bool charOnLeft ,bool useBIG)
	{
		_Name.text = name;
		_Dialogo.text = mensagem;
		if (charOnLeft)
		{
			if (useBIG)
			{
				_LBigChar.gameObject.SetActive(true);
				_LBigChar.sprite = Resources.Load<Sprite>("Imagens/UI/"+CharImg);
				_RChar.gameObject.SetActive(false);
				_LChar.gameObject.SetActive(false);
				_RBigChar.gameObject.SetActive(false);
			}else
			{
				_LChar.gameObject.SetActive(true);
				_LChar.sprite = Resources.Load<Sprite>("Imagens/UI/"+CharImg);
				_RChar.gameObject.SetActive(false);
				_LBigChar.gameObject.SetActive(false);
				_RBigChar.gameObject.SetActive(false);
			}
			
		}else
		{
			if (useBIG)
			{
				_RBigChar.gameObject.SetActive(true);
				_RBigChar.sprite = Resources.Load<Sprite>("Imagens/UI/"+CharImg);
				_LChar.gameObject.SetActive(false);
				_LBigChar.gameObject.SetActive(false);
				_RChar.gameObject.SetActive(false);
			}else
			{
				_RChar.gameObject.SetActive(true);
				_RChar.sprite = Resources.Load<Sprite>("Imagens/UI/"+CharImg);
				_LChar.gameObject.SetActive(false);
				_LBigChar.gameObject.SetActive(false);
				_RBigChar.gameObject.SetActive(false);
			}
			
		}
		
	}
}
