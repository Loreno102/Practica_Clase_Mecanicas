using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroMenu : MonoBehaviour
{
    public void Jugar()
    {
        SceneManager.LoadScene("Juego");
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Salio del juego :D");
    }
}
