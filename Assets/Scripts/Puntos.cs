using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Puntos : MonoBehaviour
{
    public Text textosPuntos;
    int puntaje;
    public int puntajeMaximo = 10;
    public GameObject winImagen;
    public string escenaVictoria = "Victoria";

    public int PuntajeActual
    {
        get { return puntaje; }
    }

    void Start()
    {
        textosPuntos.text = "Puntaje: " + puntaje.ToString() + " / " + puntajeMaximo.ToString();
        winImagen.SetActive(false);
    }

    public bool SumarPuntos()
    {
        puntaje += 1;
        textosPuntos.text = "Puntaje: " + puntaje.ToString() + " / " + puntajeMaximo.ToString();

        if (puntaje >= puntajeMaximo)
        {
            if (winImagen != null)
            {
                winImagen.SetActive(true);
            }

            SceneManager.LoadScene(escenaVictoria);
            return true;
        }

        return false;
    }
}
