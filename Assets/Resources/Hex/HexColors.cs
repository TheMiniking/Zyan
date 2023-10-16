using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu (fileName = "ColoerHex", menuName = "Zyan Assets/Create Color Hex")]
public class HexColors : ScriptableObject
{
	[ShowInInspector]public Dictionary<string,shaderControl.Color2> colors;
    
}
