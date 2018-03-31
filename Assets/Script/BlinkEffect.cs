using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkEffect : MonoBehaviour {

    Renderer color;
    public Material[] materials;

    // Use this for initialization
    void Start()
    {
        
    }
    public void getBlink(int n)
    {
        for (int i = 0; i<n; i++)
        StartCoroutine(Blink());
    }

     IEnumerator Blink()
    {
        color = GetComponent<Renderer>();
        color.sharedMaterial = materials[1];
        yield return new WaitForSeconds(0.2f);
        color.sharedMaterial = materials[0];
    }
	
}
