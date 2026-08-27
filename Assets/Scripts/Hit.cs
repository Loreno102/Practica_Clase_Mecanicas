using UnityEngine;
using UnityEngine.SceneManagement;

public class Hit : MonoBehaviour
{
    public int saludMax;
    int saludActual;

    public BarraVida barraVida;
    public GameObject imagenLose;
    public string escenaDerrota = "Derrota";
    void Start()
    {
        if (saludMax <= 0)
        {
            saludMax = 10;
        }

        saludActual = saludMax;
        if (barraVida != null)
        {
            barraVida.ConfigVidaMax(saludActual);
        }

        if (imagenLose != null)
        {
            imagenLose.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Bala2"))
        {
            FindAnyObjectByType<AudioManager>().Play("ExplosionEfecto");

            saludActual -= 1;
            if (saludActual < 0)
            {
                saludActual = 0;
            }
            if (barraVida != null)
            {
                barraVida.ConfigVida(saludActual);
            }

            if (saludActual <= 0)
            {
                Debug.Log("Te moriste :D");
                if (imagenLose != null)
                {
                    imagenLose.SetActive(true);
                }

                SceneManager.LoadScene(escenaDerrota);
            }
        }
    }
}
