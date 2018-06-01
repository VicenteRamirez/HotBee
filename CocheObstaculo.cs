using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CocheObstaculo : MonoBehaviour {

    public GameObject cronometroGo;
    public Cronometro cronometroScript;

    public GameObject audioFx;
    public AudioFx audioFXDcript;

    private void Start()
    {
        cronometroGo = GameObject.FindObjectOfType<Cronometro>().gameObject;
        cronometroScript = cronometroGo.GetComponent<Cronometro>();

        audioFx = GameObject.FindObjectOfType<AudioFx>().gameObject;
        audioFXDcript = audioFx.GetComponent<AudioFx>();
    }

    private void OnTriggerEnter2D(Collider2D other) //Colsion
    {
        if(other.GetComponent<Coche>()!=null) // Compracion de componentes
        {
            audioFXDcript.FxSonidoChoque();
            cronometroScript.tiempo = cronometroScript.tiempo - 20;
            Destroy(this.gameObject); // Destruye el autobs
        }
    }
}
