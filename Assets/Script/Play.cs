using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Play : MonoBehaviour {
    
    public void SceneChange(int ToScene)
    {
        SceneManager.LoadScene(ToScene);
    }
}
