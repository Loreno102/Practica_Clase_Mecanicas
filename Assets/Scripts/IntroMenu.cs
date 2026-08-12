using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class IntroMenu : MonoBehaviour
{
    AudioManager audioManager;

    private void Start()
    {
        audioManager = FindAnyObjectByType<AudioManager>();
    }

    public void Jugar()
    {
        StartCoroutine(CargarEscenaConSonido("Juego"));
    }

    public void ExitGame()
    {
        StartCoroutine(SalirConSonido());
    }

    IEnumerator CargarEscenaConSonido(string escena)
    {
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene(escena);
    }

    IEnumerator SalirConSonido()
    {
        yield return new WaitForSeconds(0.2f);
        Application.Quit();
        Debug.Log("Salio del juego :D");
    }
}
