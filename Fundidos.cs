using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Fundidos : MonoBehaviour {

    public Image Fundido;
    public string[] escenas;
	// Use this for initialization
	void Start () {
        Fundido.CrossFadeAlpha(0, 0.5f, false); //Funcion de clase imagen que permite hacer fundidos
	}
	
	public void FadeOut (int s) //S posicion de las escenas
    {
        Fundido.CrossFadeAlpha(1, 0.5f, false); 
        StartCoroutine(CambioEscena(escenas[s])); //Inicia la corrutina para pasar la escena
    }
    IEnumerator CambioEscena(string escena)
    {
        yield return new WaitForSeconds(1); //Espera 1 sef
        SceneManager.LoadScene(escena);
    }
    
}
