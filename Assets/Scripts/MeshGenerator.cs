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

    private void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        meshCollider = GetComponent<MeshCollider>();

        //CrearMalla();
        //RefrescarMalla();
    }
    private void Update()
    {
        CrearMalla();
        RefrescarMalla();
    }
    void CrearMalla()
    {
        vertices = new Vector3[(tamanoX + 1) * (tamanoZ + 1)];

        for(int i = 0, z = 0; z <= tamanoZ; z++)
        {
            for(int x = 0; x <= tamanoX; x++)
            {
                float y = Mathf.PerlinNoise(x * accidentesX, z * accidentesZ) * accidentesY;

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

    }
    void RefrescarMalla()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangulos;
        mesh.colors = colores;
        mesh.RecalculateNormals();
        meshCollider.sharedMesh = mesh; 
    }
}
