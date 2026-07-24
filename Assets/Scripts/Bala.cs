using UnityEngine;

public class Bala : MonoBehaviour
{
    public float velMov;
    void Update()
    {
        transform.Translate(0, 0, velMov * Time.deltaTime);
        Destroy(this.gameObject, 3f);
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Fronteras"))
        {
            Destroy(this.gameObject);
        }

    }
}
