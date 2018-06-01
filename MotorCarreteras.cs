using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotorCarreteras : MonoBehaviour {

    //Declaracion de GameObject
    public GameObject contenedorCallesGo;
    public GameObject[] contenedorCallesArray;


    public float velocidad; //Velocidad
    public bool inicioJuego; //Se haya iniciado el juego 
    public bool juegoTerminado; //Se termina

    int contadorCalles = 0; 
    int numeroSelectorCalles; //Numero de selccion. Se va a igualar a un numero aleatorio dentro del array 
    
    //Almacenar la calle que se creo y la nueva para posicionar una detras de otra
    public GameObject calleAnterior;
    public GameObject calleNueva;

    public float tamañoCalle;
    public Vector3 medidaLimitePantalla;
    public bool salioDePantalla;
    public GameObject mCamGo; //Acceder al componente camara que tieenen todos los go cam
    public Camera mCamComp; // Acceder al componente y almacenarlo

    public GameObject cocheGo;
    public GameObject audioFXGO;
    public AudioFx audioFxScript;
    public GameObject bgFinalGo;

	// Use this for initialization
	void Start () {
        InicioJuego();
	}

    void InicioJuego()
    {
        contenedorCallesGo = GameObject.Find("ContenedorCalles");

        mCamGo = GameObject.Find("MainCamera"); // Buscar componente camara
        mCamComp = mCamGo.GetComponent<Camera>();

        bgFinalGo = GameObject.Find("GameOver");
        bgFinalGo.SetActive(false);

        audioFXGO = GameObject.Find("AudioFx");
        audioFxScript = audioFXGO.GetComponent<AudioFx>();

        cocheGo = GameObject.FindObjectOfType<Coche>().gameObject;


        VelocidadMotorCarretera();
        MedirPantalla();
        BuscoCalles();
    }

   public void JuegoTerminadoEstados()
    {
        cocheGo.GetComponent<AudioSource>().Stop();
        audioFxScript.FxMusic();
        bgFinalGo.SetActive(true);
    }

    void VelocidadMotorCarretera()
    {
        velocidad = 9; // Unidades por segundo.
    }

    void BuscoCalles()
    {
        contenedorCallesArray = GameObject.FindGameObjectsWithTag("Calle"); //Buscar todos los gameobjec  con tag calle 
        for (int i = 0; i < contenedorCallesArray.Length; i++) 
        {
            contenedorCallesArray[i].gameObject.transform.parent = contenedorCallesGo.transform; //Lo haga hijo en la jerarquia de CalleGo
            contenedorCallesArray[i].gameObject.SetActive(false); // Apagar
            contenedorCallesArray[i].gameObject.transform.name = "CalleOFF" + i; // Cambiar Nombre y numeración 
        }
        CrearCalles();
    }

    void CrearCalles()
    {
        contadorCalles++; //Se suma 1 (primera calle a crear)
        numeroSelectorCalles = Random.Range(0, contenedorCallesArray.Length);//Margen(Rango) de numeros random
        GameObject Calle = Instantiate(contenedorCallesArray[numeroSelectorCalles]); //Gameonject temporal que se ecnarga de clonar una copia con un numero aleatorio
        Calle.SetActive(true); //Encender la copia
        Calle.name = "Calle" + contadorCalles; //Cambia nombre y el numero que se creo
        Calle.transform.parent = gameObject.transform;  // Hacerla hija del motor de carretera
        PosicionoCalles();
    }
    void PosicionoCalles()
    {
        calleAnterior = GameObject.Find("Calle"+(contadorCalles-1)); //Busca la calle 0 
        calleNueva = GameObject.Find("Calle" + contadorCalles); //Buscar calle nueva

        //Posicionar las calles
         MidoCalle();
         calleNueva.transform.position = new Vector3(calleAnterior.transform.position.x,
         calleAnterior.transform.position.y + tamañoCalle, 0); // en y se va a posicionar en y y se le sua el tamaño de la calle (Encima de la pieza)

        salioDePantalla = false; // Ya salio de pantalla 
    }

    void MidoCalle()
    {
        for (int i = 0; i < calleAnterior.transform.childCount; i++) //Entrar a la calle anterior y contar los hijos que se tiene
        {
            if (calleAnterior.transform.GetChild(i).gameObject.GetComponent<Pieza>() != null) //Si la calle anterior tiene el componente pieza se toma el tamaño i
            {
                float tamañoPieza = calleAnterior.transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().bounds.size.y; //Medir calle para cada atributo que contega el componente pieza (para cada hijo)
                tamañoCalle = tamañoCalle + tamañoPieza;
            }
        }
    }

    void MedirPantalla()
    {
        medidaLimitePantalla = new Vector3(0, mCamComp.ScreenToWorldPoint(new Vector3(0, 0, 0)).y - 0.5f, 0); //La medida del limite de la pantala sea el 0 de y (Medida de la pantalla)ScreenToWorldPoint(Medida de pixeles a vectores)

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (inicioJuego == true && juegoTerminado == false)
        {
            transform.Translate(Vector3.down * velocidad * Time.deltaTime); // Las unidades sea por segundo (vel constante)

            if (calleAnterior.transform.position.y + tamañoCalle < medidaLimitePantalla.y && salioDePantalla == false) //Se toma la medida de y y si es menor. Ya salio de la pantalla
            {
                salioDePantalla = true;
                //Destuir calles
                DestruyoCalles();
            }
        }
        
	}

    void DestruyoCalles()
    {
        Destroy(calleAnterior);
        tamañoCalle = 0; // Se destruyo para que no se quede almacenada la medida
        calleAnterior = null;
        CrearCalles();
    }
}
