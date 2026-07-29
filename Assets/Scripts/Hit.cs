using Unity.VisualScripting;
using UnityEngine;

public class Hit : MonoBehaviour
{
    public int saludMax;
    int saludActual;

    public BarraVida barraVida;
    public GameObject imagenLose;
    void Start()
    {
        saludActual = saludMax;
        barraVida.ConfigVidaMax(saludActual);
        imagenLose.SetActive(false);
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Bala2"))
        {
            saludActual -= 1;
            barraVida.ConfigVida(saludActual);

            if (saludActual <= 0)
            {
                Debug.Log("Te moriste :D");
                imagenLose.SetActive(true);
            }
        }
    }
}
