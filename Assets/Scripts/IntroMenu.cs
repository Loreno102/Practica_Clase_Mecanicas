using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class IntroMenu : MonoBehaviour
{
    AudioManager audioManager;

    private void Start()
    {
        audioManager = FindAnyObjectByType<AudioManager>();
        ConfigurarEscenaFinal();
    }

    public void Jugar()
    {
        StartCoroutine(CargarEscenaConSonido("Juego"));
    }

    public void Intro()
    {
        StartCoroutine(CargarEscenaConSonido("Intro"));
    }

    void ConfigurarEscenaFinal()
    {
        string escenaActual = SceneManager.GetActiveScene().name;
        if (escenaActual != "Victoria" && escenaActual != "Derrota")
        {
            return;
        }

        CambiarTextoBoton("PlayButton", "Jugar de Nuevo");
        CambiarTextoBoton("ExitButton", "Volver Intro");

        GameObject optionsButton = GameObject.Find("OptionsButton");
        if (optionsButton != null)
        {
            optionsButton.SetActive(false);
        }

    }

    void CambiarTextoBoton(string nombreBoton, string texto)
    {
        GameObject boton = GameObject.Find(nombreBoton);
        if (boton == null)
        {
            return;
        }

        Text textoNormal = boton.GetComponentInChildren<Text>(true);
        if (textoNormal != null)
        {
            textoNormal.text = texto;
        }

        TMP_Text textoTmp = boton.GetComponentInChildren<TMP_Text>(true);
        if (textoTmp != null)
        {
            textoTmp.text = texto;
        }
    }

    public void ExitGame()
    {
        StartCoroutine(SalirConSonido());
    }

    IEnumerator CargarEscenaConSonido(string escena)
    {
        if (audioManager == null)
        {
            audioManager = FindAnyObjectByType<AudioManager>();
        }

        if (audioManager != null)
        {
            audioManager.Play("Boton");
        }

        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene(escena);
    }

    IEnumerator SalirConSonido()
    {
        if (audioManager == null)
        {
            audioManager = FindAnyObjectByType<AudioManager>();
        }

        if (audioManager != null)
        {
            audioManager.Play("Boton");
        }

        yield return new WaitForSeconds(0.2f);
        Application.Quit();
        Debug.Log("Salio del juego :D");
    }
}
