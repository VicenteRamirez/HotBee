using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //Framework de  la interfaz
public class Cronometro : MonoBehaviour {

    public GameObject motorCarreterasGo; //Distancia y tiempo que se está moviendo
    public MotorCarreteras motorCarreterasScript; //Conectar con MotorCarreteras
    public float tiempo;
    public float d;
    public float distancia;
    public Text txtTiempo;
    public Text txtDistancia;
    public Text txtDistanciaFinal;
    public int puntuacion;


    // Use this for initialization
    void Start () {
        motorCarreterasGo = GameObject.Find("MotorCarreteras"); //Buscar gameo en unity
        motorCarreterasScript = motorCarreterasGo.GetComponent<MotorCarreteras>(); //Asignar componente

        txtTiempo.text = "2:00";
        txtDistancia.text = "0";
        tiempo = 120;

	}
	
	// Update is called once per frame
	void Update () {
        if (motorCarreterasScript.inicioJuego == true && motorCarreterasScript.juegoTerminado == false)
        {
            CalculoTiempoDistancia();
        }
        if(tiempo<= 0 && motorCarreterasScript.juegoTerminado == false)
        {
            motorCarreterasScript.juegoTerminado = true;
            motorCarreterasScript.JuegoTerminadoEstados();
            txtDistanciaFinal.text = ((int)distancia).ToString()+ "M";
            txtTiempo.text = "0:00";
        }
       
    }

    void CalculoTiempoDistancia()
    {
        
        //Distancia
        distancia += Time.deltaTime * motorCarreterasScript.velocidad; //Formula de la distancia
        txtDistancia.text = ((int)distancia).ToString();
       

        //motorCarreterasScript.velocidad+=(float)(.01);
       if(distancia>150)
        {
            motorCarreterasScript.velocidad  += (float)0.01;
        }
       
        PlayerPrefs.SetInt("Puntuacion", (int)(distancia));
        // ( (distancia) )= PlayerPrefs.GetInt("Puntuacion");
        PlayerPrefs.SetInt("Puntuacion",(int)(distancia));
        PlayerPrefs.Save();
       

        //Tiempo
        tiempo -= Time.deltaTime; //Cuenta regresiva
        int minutos = (int)tiempo / 60; //Obtener minutos
        int segundos = (int)tiempo % 60; //Obtener segundos
        txtTiempo.text = minutos.ToString() + ":" + segundos.ToString().PadLeft(2,'0'); // Cajas de texto 00: 00 PadLeft = Valor de os numeros . Rellenar con 0
    }

    public override bool Equals(object obj)
    {
        var cronometro = obj as Cronometro;
        return cronometro != null &&
               base.Equals(obj) &&
               EqualityComparer<Text>.Default.Equals(txtTiempo, cronometro.txtTiempo);
    }
}
