using UnityEngine;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instancia;

    public MeshGenerator meshGenerator;
    public Puntos puntos;
    public Transform jugador;
    public GameObject enemigoPrefab;
    public int enemigosPorMuerte = 2;

    bool partidaGanada;
    GameObject plantillaEnemigo;
    List<Tanque_2> enemigos = new List<Tanque_2>();

    private void Awake()
    {
        if (instancia != null && instancia != this)
        {
            Destroy(gameObject);
            return;
        }

        instancia = this;
    }

    private void Start()
    {
        BuscarReferencias();

        if (FindAnyObjectByType<Tanque_2>() == null)
        {
            CrearEnemigo();
        }
    }

    private void OnDestroy()
    {
        if (instancia == this)
        {
            instancia = null;
        }
    }

    public void BuscarReferencias()
    {
        if (meshGenerator == null)
        {
            meshGenerator = FindAnyObjectByType<MeshGenerator>();
        }

        if (puntos == null)
        {
            puntos = FindAnyObjectByType<Puntos>();
        }

        if (jugador == null)
        {
            Hit vidaJugador = FindAnyObjectByType<Hit>();
            if (vidaJugador != null)
            {
                jugador = vidaJugador.transform;
            }
        }

        if (enemigoPrefab == null)
        {
            Tanque_2 enemigoEscena = FindAnyObjectByType<Tanque_2>();
            if (enemigoEscena != null)
            {
                enemigoPrefab = enemigoEscena.gameObject;
            }
        }

        PrepararPlantillaEnemigo();
    }

    public void RegistrarEnemigo(Tanque_2 enemigo)
    {
        BuscarReferencias();

        if (!enemigos.Contains(enemigo))
        {
            enemigos.Add(enemigo);
        }

        enemigo.Configurar(jugador, puntos, this, meshGenerator);
    }

    public void EnemigoDestruido(Tanque_2 enemigo)
    {
        if (partidaGanada)
        {
            return;
        }

        bool gano = false;

        if (puntos != null)
        {
            gano = puntos.SumarPuntos();
        }

        if (gano)
        {
            partidaGanada = true;
            return;
        }

        enemigos.Remove(enemigo);
        GameObject plantilla = ObtenerPlantillaEnemigo(enemigo.gameObject);

        for (int i = 0; i < enemigosPorMuerte; i++)
        {
            CrearEnemigo(plantilla);
        }
    }

    public void ReiniciarEnemigos()
    {
        BuscarReferencias();
        GameObject plantilla = ObtenerPlantillaEnemigo();

        Tanque_2[] enemigosEscena = FindObjectsByType<Tanque_2>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
        foreach (Tanque_2 enemigoEscena in enemigosEscena)
        {
            if (!enemigos.Contains(enemigoEscena))
            {
                enemigos.Add(enemigoEscena);
            }
        }

        for (int i = enemigos.Count - 1; i >= 0; i--)
        {
            if (enemigos[i] != null)
            {
                Destroy(enemigos[i].gameObject);
            }
        }

        enemigos.Clear();
        partidaGanada = false;
        CrearEnemigo(plantilla);
    }

    public void CrearEnemigo()
    {
        CrearEnemigo(ObtenerPlantillaEnemigo());
    }

    void CrearEnemigo(GameObject plantilla)
    {
        BuscarReferencias();

        if (plantilla == null || meshGenerator == null)
        {
            return;
        }

        Vector3 posicion = meshGenerator.ObtenerPosicionAleatoriaSobreTerreno(2f);
        Quaternion rotacion = Quaternion.Euler(0, Random.Range(0f, 360f), 0);
        GameObject nuevoEnemigo = Instantiate(plantilla, posicion, rotacion);
        nuevoEnemigo.name = "Tanque_2";
        nuevoEnemigo.SetActive(true);

        Tanque_2 tanque = nuevoEnemigo.GetComponent<Tanque_2>();
        if (tanque != null)
        {
            RegistrarEnemigo(tanque);
            tanque.ReiniciarPatrullaje(meshGenerator);
        }
    }

    GameObject ObtenerPlantillaEnemigo(GameObject respaldo = null)
    {
        if (plantillaEnemigo != null)
        {
            return plantillaEnemigo;
        }

        if (enemigoPrefab == null)
        {
            enemigoPrefab = respaldo;
        }

        PrepararPlantillaEnemigo();
        return plantillaEnemigo != null ? plantillaEnemigo : enemigoPrefab;
    }

    void PrepararPlantillaEnemigo()
    {
        if (plantillaEnemigo != null || enemigoPrefab == null)
        {
            return;
        }

        plantillaEnemigo = Instantiate(enemigoPrefab, transform);
        plantillaEnemigo.name = "Plantilla_Tanque_2";
        plantillaEnemigo.SetActive(false);
    }
}
