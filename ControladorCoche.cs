using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorCoche : MonoBehaviour {


    public GameObject cocheGo;

    public float anguloDeGiro; //Controlar angulo de giro
    public float velocidad; //Velocidad

	// Use this for initialization
	void Start () {
        cocheGo = GameObject.FindObjectOfType<Coche>().gameObject; //Buscar objetos tipo coche y lo devuelve como gameobject

	}
	
	// Update is called once per frame
	void Update () {
        float giroEnz = 0; // Para que rote de un lado a otro en base a perspectvia 
        transform.Translate(Vector2.right * Input.GetAxis("Horizontal")* velocidad * Time.deltaTime);  //Se mueve en x y y . Se mueve en el vector de izquirda a derecha con respecto al juego

        giroEnz = Input.GetAxis("Horizontal") * -anguloDeGiro;

        cocheGo.transform.rotation = Quaternion.Euler(0,0,giroEnz); //Todos los valores que se pasen se transforman en angulos
        
	}
}
