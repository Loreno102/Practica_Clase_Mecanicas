using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class AudioManager : MonoBehaviour
{
    public Audio[] audios;
    //public static AudioManager instance;

    public Slider volSlider;
    float volValue;
    float lastVolValue;

    public void Awake()
    {
        //if (instance == null)
        //{
            //instance = this;
            //DontDestroyOnLoad(gameObject);
        //}
        //else
        //{
            //Destroy(gameObject);
        //}

        foreach (Audio a in audios)
        {
            a.source = gameObject.AddComponent<AudioSource>();
            a.source.clip = a.audioFile;
            a.source.volume = a.volume;
            a.source.pitch = a.pitch;
            a.source.loop = a.loop;
        }

    }
    void Start()
    {
        if (volSlider != null)
        {
            volSlider.value = PlayerPrefs.GetFloat("volumen", volSlider.value);
            volValue = volSlider.value;
        }
        else
        {
            volValue = PlayerPrefs.GetFloat("volumen", 1f);
        }

        CambiarVolumen();
        Play("MusicaIntro");
    }
    void Update()
    {
        if (volSlider == null)
        {
            return;
        }

        volValue = volSlider.value;

        if (volValue != lastVolValue)
        {
            CambiarVolumen();
        }
        lastVolValue = volValue;
    }

    void CambiarVolumen()
    {
        AudioListener.volume = volValue;
        PlayerPrefs.SetFloat("volumen", volValue);
        PlayerPrefs.Save();
    }

    public void Play(string name)
    {
        Audio a = Array.Find(audios, audio => audio.name == name);

        if (a == null)
        {
            Debug.LogWarning("El nombre del archivo " + name + " no existe");
            return;
        }

        a.source.Play();
    }

    public void PlayLoop(string name)
    {
        Audio a = Array.Find(audios, audio => audio.name == name);

        if (a == null)
        {
            Debug.LogWarning("El nombre del archivo " + name + " no existe");
            return;
        }

        if (!a.source.isPlaying)
        {
            a.source.Play();
        }
    }

    public void Stop(string name)
    {
        Audio a = Array.Find(audios, audio => audio.name == name); 
        if (a == null)
        {
            Debug.LogWarning("El nombre del archivo " + name + " no existe");
            return;
        }
        a.source.Stop();
    }
}
