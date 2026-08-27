using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class Menu : MonoBehaviour
{

    public GameObject mainPanel;
    bool activo;
    AudioManager audioManager;

    private void Start()
    {
        mainPanel.SetActive(false);
        activo = false;
        audioManager = FindAnyObjectByType<AudioManager>();
        CrearBotonesFaltantes();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            activo=!activo;
            ActivarPanelMain();
        }

    }

    void ActivarPanelMain()
    {
        if (activo)
        {
            mainPanel.SetActive(true);
        }

        else { mainPanel.SetActive(false); }
    }

    public void Jugar()
    {
        StartCoroutine(CargarEscenaConSonido("Juego"));
    }

    public void Intro() 
    {
        StartCoroutine(CargarEscenaConSonido("Intro"));
    }

    public void Reiniciar()
    {
        StartCoroutine(CargarEscenaConSonido(SceneManager.GetActiveScene().name));
    }

    public void RegenerarTerreno()
    {
        if (audioManager == null)
        {
            audioManager = FindAnyObjectByType<AudioManager>();
        }

        if (audioManager != null)
        {
            audioManager.Play("Boton");
        }

        MeshGenerator meshGenerator = FindAnyObjectByType<MeshGenerator>();
        if (meshGenerator != null)
        {
            meshGenerator.RegenerarTerreno();
        }
    }

    void CrearBotonesFaltantes()
    {
        if (mainPanel == null)
        {
            return;
        }

        Transform contenedor = BuscarHijo(mainPanel.transform, "MainMenu");
        if (contenedor == null)
        {
            contenedor = mainPanel.transform;
        }

        Button[] botones = contenedor.GetComponentsInChildren<Button>(true);
        if (botones.Length == 0)
        {
            return;
        }

        Button plantilla = botones[0];

        CrearBotonSiNoExiste("ReiniciarButton", "Reiniciar", -220f, plantilla, contenedor, Reiniciar);
        CrearBotonSiNoExiste("RegenerarTerrenoButton", "Regenerar Terreno", -320f, plantilla, contenedor, RegenerarTerreno);
    }

    void CrearBotonSiNoExiste(string nombre, string texto, float posicionY, Button plantilla, Transform contenedor, UnityEngine.Events.UnityAction accion)
    {
        if (contenedor.Find(nombre) != null)
        {
            return;
        }

        Button nuevoBoton = Instantiate(plantilla, contenedor);
        nuevoBoton.name = nombre;
        nuevoBoton.onClick.RemoveAllListeners();
        nuevoBoton.onClick.AddListener(accion);

        RectTransform rectTransform = nuevoBoton.GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            rectTransform.anchoredPosition = new Vector2(0, posicionY);
        }

        Text textoNormal = nuevoBoton.GetComponentInChildren<Text>(true);
        if (textoNormal != null)
        {
            textoNormal.text = texto;
        }

        TMPro.TMP_Text textoTmp = nuevoBoton.GetComponentInChildren<TMPro.TMP_Text>(true);
        if (textoTmp != null)
        {
            textoTmp.text = texto;
        }
    }

    Transform BuscarHijo(Transform padre, string nombre)
    {
        if (padre.name == nombre)
        {
            return padre;
        }

        foreach (Transform hijo in padre)
        {
            Transform encontrado = BuscarHijo(hijo, nombre);
            if (encontrado != null)
            {
                return encontrado;
            }
        }

        return null;
    }

    IEnumerator CargarEscenaConSonido(string escena)
    {
        if (audioManager == null)
        {
            audioManager = FindAnyObjectByType<AudioManager>();
        }

        if (audioManager != null)
        {
            audioManager.Play("Boton");
        }

        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene(escena);
    }
}
