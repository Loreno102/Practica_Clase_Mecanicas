using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]

public class MeshGenerator : MonoBehaviour
{
    Mesh mesh;
    MeshCollider meshCollider;

    Vector3[] vertices;
    int[] triangulos;

    Color[] colores;
    public Gradient gradiente;

    [Range (1, 100)]
    public int tamanoX = 100;

    [Range(1, 100)]
    public int tamanoZ = 100;

    [Range(0f, 0.1f)]
    public float accidentesX = 0.07f;

    [Range(0f, 10f)]
    public float accidentesY = 1f;

    [Range(0f, 0.1f)]
    public float accidentesZ = 0.07f;

    float alturaMinTerreno;
    float alturaMaxTerreno;

    public GameObject[] objetos;
    public int cantidadObjetos = 1;
    public int cantidadPuntosPatrullaje = 5;

    Vector3 posPlayer;
    Vector3 posEnemigo;
    float ruidoOffsetX;
    float ruidoOffsetY;
    float ruidoOffsetZ;
    List<GameObject> objetosGenerados = new List<GameObject>();
    List<Vector3> puntosPatrullaje = new List<Vector3>();

    public GameObject tanquePlayer;
    public GameObject tanqueEnemigo;

    private void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        meshCollider = GetComponent<MeshCollider>();

        GenerarValoresAleatoriosTerreno();
        CrearMalla();
        RefrescarMalla();
        CrearObjetos();
        CrearPuntosPatrullaje();
        PosicionarTanques();
    }
    private void Update()
    {
        //CrearMalla();
        //RefrescarMalla();
    }
    void CrearMalla()
    {
        alturaMinTerreno = float.MaxValue;
        alturaMaxTerreno = float.MinValue;
        vertices = new Vector3[(tamanoX + 1) * (tamanoZ + 1)];

        for(int i = 0, z = 0; z <= tamanoZ; z++)
        {
            for(int x = 0; x <= tamanoX; x++)
            {
                float y = Mathf.PerlinNoise((x + ruidoOffsetX) * accidentesX,
                                            (z + ruidoOffsetZ) * accidentesZ) * accidentesY + ruidoOffsetY;

                vertices[i] = new Vector3(x, y, z); 

                if(y > alturaMaxTerreno)
                {
                    alturaMaxTerreno = y;
                }

                if (y < alturaMinTerreno)
                {
                    alturaMinTerreno = y;
                }

                i++;
            }
        }

        triangulos = new int[tamanoX * tamanoZ * 6];

        int vert = 0;
        int trian = 0;

        for(int z = 0; z < tamanoZ; z++)
        {
            for(int x = 0; x < tamanoX; x++)
            {
                triangulos[trian + 0] = vert + 0;
                triangulos[trian + 1] = vert + tamanoX + 1;
                triangulos[trian + 2] = vert + 1;
                triangulos[trian + 3] = vert + 1;
                triangulos[trian + 4] = vert + tamanoX + 1;
                triangulos[trian + 5] = vert + tamanoX + 2;
                vert++;
                trian += 6;
            }
            vert++;
        }

        colores = new Color[vertices.Length];
        
        for(int i = 0, z = 0; z <= tamanoZ; z++)
        {
            for ( int x =0; x <= tamanoX; x++)
            {
                float altura = Mathf.InverseLerp(alturaMinTerreno, alturaMaxTerreno, vertices[i].y);
                colores[i] = gradiente.Evaluate(altura);
                i++;
            }
        }

    }
    void RefrescarMalla()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangulos;
        mesh.colors = colores;
        mesh.RecalculateNormals();

        if (meshCollider != null)
        {
            meshCollider.sharedMesh = mesh;
        }
    }

    void CrearObjetos()
    {
        LimpiarObjetosGenerados();

        for (int i = 0; i < cantidadObjetos; i++)
        {
            if (objetos.Length == 0)
            {
                return;
            }

            GameObject objeto = Instantiate(objetos[Random.Range(0, objetos.Length)],
                ObtenerPosicionAleatoriaSobreTerreno(2f),
                Quaternion.Euler(Vector3.up * Random.Range(0, 360)));

            objetosGenerados.Add(objeto);

        }
    }

    void PosicionarTanques()
    {
        posPlayer = ObtenerPosicionAleatoriaSobreTerreno(2f);
        posEnemigo = ObtenerPosicionAleatoriaSobreTerreno(2f);

        if (tanquePlayer != null)
        {
            tanquePlayer.transform.position = posPlayer;
            tanquePlayer.transform.rotation = Quaternion.Euler(0, Random.Range(0f, 360f), 0);
        }

        if (tanqueEnemigo != null)
        {
            tanqueEnemigo.transform.position = posEnemigo;
            tanqueEnemigo.transform.rotation = Quaternion.Euler(0, Random.Range(0f, 360f), 0);
        }
    }

    public void RegenerarTerreno()
    {
        GenerarValoresAleatoriosTerreno();
        CrearMalla();
        RefrescarMalla();
        CrearObjetos();
        CrearPuntosPatrullaje();
        PosicionarTanques();

        EnemyManager enemyManager = FindAnyObjectByType<EnemyManager>();
        if (enemyManager != null)
        {
            enemyManager.ReiniciarEnemigos();
        }
    }

    public Vector3 ObtenerPosicionAleatoriaSobreTerreno(float alturaExtra)
    {
        if (vertices == null || vertices.Length == 0)
        {
            return transform.position + Vector3.up * alturaExtra;
        }

        return transform.TransformPoint(vertices[Random.Range(0, vertices.Length)]) + new Vector3(0, alturaExtra, 0);
    }

    public Vector3 ObtenerPuntoPatrullajeAleatorio()
    {
        if (puntosPatrullaje.Count == 0)
        {
            CrearPuntosPatrullaje();
        }

        if (puntosPatrullaje.Count == 0)
        {
            return ObtenerPosicionAleatoriaSobreTerreno(2f);
        }

        return puntosPatrullaje[Random.Range(0, puntosPatrullaje.Count)];
    }

    void CrearPuntosPatrullaje()
    {
        puntosPatrullaje.Clear();

        for (int i = 0; i < cantidadPuntosPatrullaje; i++)
        {
            puntosPatrullaje.Add(ObtenerPosicionAleatoriaSobreTerreno(2f));
        }
    }

    void GenerarValoresAleatoriosTerreno()
    {
        accidentesX = Random.Range(0.01f, 0.1f);
        accidentesY = Random.Range(1f, 10f);
        accidentesZ = Random.Range(0.01f, 0.1f);

        ruidoOffsetX = Random.Range(0f, 1000f);
        ruidoOffsetY = Random.Range(0f, 1f);
        ruidoOffsetZ = Random.Range(0f, 1000f);
    }

    void LimpiarObjetosGenerados()
    {
        for (int i = objetosGenerados.Count - 1; i >= 0; i--)
        {
            if (objetosGenerados[i] != null)
            {
                Destroy(objetosGenerados[i]);
            }
        }

        objetosGenerados.Clear();
    }
}
