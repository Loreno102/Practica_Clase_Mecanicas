using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public float velMov;
    public float velGiro;
    float mov, gir;

    private void Update()
    {
        if (Input.GetAxis("Vertical") != 0)
        {
            Mover();
        }

        if (Input.GetAxis("Horizontal") != 0)
        {
            Girar();
        }
    }
    void Mover()
    {
        mov = Input.GetAxis("Vertical") * velMov * Time.deltaTime;
        transform.Translate(0,0,mov);
    }

    void Girar()
        {

        }
}
