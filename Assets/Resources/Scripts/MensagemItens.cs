using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Sirenix.OdinInspector;

public class MensagemItens : MonoBehaviour
{
	public GameObject[] painelItem;
	public GameObject[] painelPack;
	
	[Button]public void SendMensagem(string mensagem, bool pack){
		if (!pack){
			painelPack[0].SetActive(false);
			painelItem[0].SetActive(true);
			painelItem[1].GetComponent<TMP_Text>().text = "Voce Ganhou :";
			painelItem[2].GetComponent<TMP_Text>().text = mensagem;
		}else{
			painelPack[0].SetActive(true);
			painelItem[0].SetActive(false);
			painelPack[1].GetComponent<TMP_Text>().text = "Voce Ganhou :";
			painelPack[2].GetComponent<TMP_Text>().text = mensagem;
		}
		
	}
	
	[Button]public void text()=>Debug.Log("1x Dragon<br>1x Warrior<br>1x Fish");
}
