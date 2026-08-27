using UnityEngine;

public class Tanque_2 : MonoBehaviour
{
    public Transform posTanque_1;
    public float velMov, velGir;

    Vector3 diferencia;
    Vector3 puntoPatrullaje;
    bool tienePuntoPatrullaje;

    float tiempoDisparo = 2f;
    float proximoDisparo;

    public GameObject bala2;
    public Transform puntoDisparo;
    public Puntos puntos;
    public int vidaMaxima = 2;
    public float distanciaDetectarJugador = 10f;
    public float distanciaMinimaJugador = 2f;
    public float distanciaLlegadaPatrulla = 1.5f;

    int vidaActual;
    bool destruido;
    EnemyManager enemyManager;
    MeshGenerator meshGenerator;

    private void Start()
    {
        vidaActual = vidaMaxima;

        if (EnemyManager.instancia == null)
        {
            new GameObject("EnemyManager").AddComponent<EnemyManager>();
        }

        EnemyManager.instancia.RegistrarEnemigo(this);
    }

    public void Configurar(Transform jugador, Puntos puntosPartida, EnemyManager manager, MeshGenerator generador)
    {
        posTanque_1 = jugador;
        puntos = puntosPartida;
        enemyManager = manager;
        meshGenerator = generador;
        vidaActual = vidaMaxima;
        destruido = false;
        ReiniciarPatrullaje(meshGenerator);
    }

    public void ReiniciarPatrullaje(MeshGenerator generador)
    {
        meshGenerator = generador;
        tienePuntoPatrullaje = false;
        ElegirNuevoPuntoPatrullaje();
    }

    private void Update()
    {
        if (posTanque_1 == null)
        {
            Patrullar();
            return;
        }

        diferencia = posTanque_1.position - this.transform.position;

        if (Vector3.Distance(posTanque_1.position, this.transform.position) < distanciaDetectarJugador)
        {
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                                      Quaternion.LookRotation(diferencia),
                                      velGir * Time.deltaTime);

            Disparar();

            if (Vector3.Distance(posTanque_1.position, this.transform.position) > distanciaMinimaJugador)
            {
                this.transform.Translate(0, 0, velMov * Time.deltaTime);
            }

        }
        else
        {
            Patrullar();
        }

    }

    void Patrullar()
    {
        if (meshGenerator == null)
        {
            meshGenerator = FindAnyObjectByType<MeshGenerator>();

            if (meshGenerator == null)
            {
                return;
            }
        }

        if (!tienePuntoPatrullaje || Vector3.Distance(transform.position, puntoPatrullaje) < distanciaLlegadaPatrulla)
        {
            ElegirNuevoPuntoPatrullaje();
        }

        Vector3 direccion = puntoPatrullaje - transform.position;
        direccion.y = 0;

        if (direccion == Vector3.zero)
        {
            return;
        }

        transform.rotation = Quaternion.Slerp(transform.rotation,
                            Quaternion.LookRotation(direccion),
                            velGir * Time.deltaTime);
        transform.Translate(0, 0, velMov * Time.deltaTime);
    }

    void ElegirNuevoPuntoPatrullaje()
    {
        if (meshGenerator == null)
        {
            meshGenerator = FindAnyObjectByType<MeshGenerator>();
        }

        if (meshGenerator == null)
        {
            return;
        }

        puntoPatrullaje = meshGenerator.ObtenerPuntoPatrullajeAleatorio();
        tienePuntoPatrullaje = true;
    }

    void Disparar()
    {
        if (Time.time > proximoDisparo)
        {
            proximoDisparo = Time.time + tiempoDisparo;
            Instantiate(bala2, puntoDisparo.position, this.transform.rotation);
        }

    }
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Bala1"))
        {
            FindAnyObjectByType<AudioManager>().Play("ExplosionEfecto2");
            vidaActual -= 1;

            if (vidaActual <= 0 && !destruido)
            {
                destruido = true;

                if (enemyManager == null)
                {
                    enemyManager = EnemyManager.instancia;
                }

                if (enemyManager != null)
                {
                    enemyManager.EnemigoDestruido(this);
                }

                Destroy(gameObject);
            }
        }
    }
}
