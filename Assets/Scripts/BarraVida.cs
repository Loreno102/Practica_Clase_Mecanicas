using UnityEngine;
using UnityEngine.UI;

public class BarraVida : MonoBehaviour
{
    public Slider slider;

    public void ConfigVidaMax(int salud)
    {
        slider.maxValue = salud;
        slider.value = salud;
    }

    public void ConfigVida(int salud)
    {
        slider.value = salud;
    }

}
