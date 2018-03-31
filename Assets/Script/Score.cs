using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {


    public Text ScoreValue;


    public void getScore(string ScoreVal)
    {
        ScoreValue = GetComponent<Text>();
        ScoreValue.text = ScoreVal;
    }


}
