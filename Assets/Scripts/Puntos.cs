using UnityEngine;
using UnityEngine.UI;

public class Puntos : MonoBehaviour
{
    public Text textosPuntos;
    int puntaje;
    public GameObject winImagen;
    void Start()
    {
        textosPuntos.text = "Puntaje: " + puntaje.ToString();
        winImagen.SetActive(false);
    }

    public void SumarPuntos()
    {
        puntaje += 1;
        textosPuntos.text = "Puntaje: " + puntaje.ToString();

        if (puntaje >=5)
        {
            winImagen.SetActive(true);
        }
    }
}
