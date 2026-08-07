using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public float velMov;
    public float velGir;
    float mov, gir;
    AudioManager audioManager;

    private void Start()
    {
        audioManager = FindAnyObjectByType<AudioManager>();
    }

    private void Update()
    {
        bool seMueve = false;

        if (Input.GetAxis("Vertical") != 0)
        {
            Mover();
            seMueve = true;
        }

        if (Input.GetAxis("Horizontal") != 0)
        {
            Girar();
            seMueve = true;
        }

        if (audioManager != null)
        {
            if (seMueve)
            {
                audioManager.PlayLoop("MovimientoTanque");
            }
            else
            {
                audioManager.Stop("MovimientoTanque");
            }
        }
    }
    void Mover()
    {
        mov = Input.GetAxis("Vertical") * velMov * Time.deltaTime;
        transform.Translate(0,0,mov);
    }

    void Girar()
        {
        gir = Input.GetAxis("Horizontal") * velGir * Time.deltaTime;
        transform.Rotate(0, gir, 0);
    }
}
