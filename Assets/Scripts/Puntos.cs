using UnityEngine;
using UnityEngine.UI;

public class Puntos : MonoBehaviour
{
    public Text textosPuntos;
    int puntaje;
    public int puntajeMaximo = 10;
    public GameObject winImagen;
    void Start()
    {
        textosPuntos.text = "Puntaje: " + puntaje.ToString() + " / " + puntajeMaximo.ToString();
        winImagen.SetActive(false);
    }

    public void SumarPuntos()
    {
        puntaje += 1;
        textosPuntos.text = "Puntaje: " + puntaje.ToString() + " / " + puntajeMaximo.ToString();

        if (puntaje >= puntajeMaximo)
        {
            winImagen.SetActive(true);
        }
    }
}
