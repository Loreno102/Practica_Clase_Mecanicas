using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Menu : MonoBehaviour
{

    public GameObject mainPanel;
    bool activo;
    AudioManager audioManager;

    private void Start()
    {
        mainPanel.SetActive(false);
        activo = false;
        audioManager = FindAnyObjectByType<AudioManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            activo=!activo;
            ActivarPanelMain();
        }

    }

    void ActivarPanelMain()
    {
        if (activo)
        {
            mainPanel.SetActive(true);
        }

        else { mainPanel.SetActive(false); }
    }

    public void Jugar()
    {
        StartCoroutine(CargarEscenaConSonido("Juego"));
    }

    public void Intro() 
    {
        StartCoroutine(CargarEscenaConSonido("Intro"));
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
}
