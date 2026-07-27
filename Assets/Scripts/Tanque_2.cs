using UnityEngine;

public class Tanque_2 : MonoBehaviour
{
    public Transform posTanque_1;
    public float velMov, velGir;
    float mov, gir;

    Vector3 diferencia;

    float tiempoDisparo = 2f;
    float proximoDisparo;

    public GameObject bala2;
    public Transform puntoDisparo;

    private void Update()
    {
        diferencia = posTanque_1.position - this.transform.position;

        if (Vector3.Distance(posTanque_1.position, this.transform.position) < 10)
        {
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                                      Quaternion.LookRotation(diferencia),
                                      velGir * Time.deltaTime);

            Disparar();

            if (Vector3.Distance(posTanque_1.position, this.transform.position) > 2)
            {
                this.transform.Translate(0, 0, velMov * Time.deltaTime);
            }

        }

    }
    void Disparar()
    {
        if (Time.time > proximoDisparo)
        {
            proximoDisparo = Time.time + tiempoDisparo;
            Instantiate(bala2, puntoDisparo.position, this.transform.rotation);
        }

    }
}
