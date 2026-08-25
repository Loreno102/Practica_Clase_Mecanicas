using UnityEngine;

public class Toggle : MonoBehaviour
{
    bool activo;
    public GameObject miniMapa;

    private void Start()
    {
        miniMapa.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z)) 
        {
            activo = !activo;
        }
        
        if (activo)
        {
         miniMapa.SetActive(true);
        }
        else
        {
            miniMapa.SetActive(false);
        }
    }
}
