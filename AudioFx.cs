using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFx : MonoBehaviour {
    //Almacenar los mp3 para que se reproduzcan en los audio source  por medio de audioclips
    public AudioClip[] fxs;
    AudioSource audioS;
    private void Start()
    {
        audioS = GetComponent<AudioSource>();
    }
    // 0 chocque
    public void FxSonidoChoque()
    {
        audioS.clip = fxs[0];
        audioS.Play();

    }
    // 1 Musica juego
    public void FxMusic()
    {
        audioS.clip = fxs[1];
        audioS.Play();
    }
}
