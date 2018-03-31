using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DispKey: MonoBehaviour {

    public Text key;
    
	
    public void NewKey(string newKey)
    {
        key = GetComponent<Text>();
        key.text = newKey;
    }
}
