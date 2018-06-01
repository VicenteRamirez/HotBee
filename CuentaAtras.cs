using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuentaAtras : MonoBehaviour {

    public GameObject motorCarreteraGo;
    public MotorCarreteras motorCarreteraScript;
    public Sprite[] numeros; //Un array de sprites donde se almacenan los carteles

    public GameObject contadorNumeroGo; //Acceder contador numeros
    public SpriteRenderer contadorNumerosComp; // Acceder al componente

    public GameObject controladorCoche; // Activar y desactivar sonidos
    public GameObject cocheGo;

	// Use this for initialization
	void Start () {

        InicioComponentes();
	}
	
    void InicioComponentes()
    {
        motorCarreteraGo = GameObject.Find("MotorCarreteras");
        motorCarreteraScript = motorCarreteraGo.GetComponent<MotorCarreteras>();

        contadorNumeroGo = GameObject.Find("ContadorNumeros");
        contadorNumerosComp = contadorNumeroGo.GetComponent<SpriteRenderer>();

        cocheGo = GameObject.Find("Coche");
        controladorCoche = GameObject.Find("ControladorCoche");

        InicioCuentaAtras();

    }

    void InicioCuentaAtras()
    {
        StartCoroutine(Contando());
    }

    IEnumerator Contando() //Corrutina (Espera de segundos)
    {
        controladorCoche.GetComponent<AudioSource>().Play(); //ejecute el sonido del controlador coche
        yield return new WaitForSeconds(2); //Espera 2 segundos

        contadorNumerosComp.sprite = numeros[1]; //Accesa a la pos 1
        this.gameObject.GetComponent<AudioSource>().Play(); //Ejecuta el sonido de la cuenta atras
        yield return new WaitForSeconds(1); //Espera 1 segundo

        contadorNumerosComp.sprite = numeros[2]; 
        this.gameObject.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(1);

        contadorNumerosComp.sprite = numeros[3];
        motorCarreteraScript.inicioJuego = true; //Se inicia el juego
        contadorNumeroGo.GetComponent<AudioSource>().Play(); //Sonido de arranque
        cocheGo.GetComponent<AudioSource>().Play(); 
        yield return new WaitForSeconds(2);

        contadorNumeroGo.SetActive(false); //El carte desaparece
    }


    // Update is called once per frame
    void Update () {
		
	}
}
