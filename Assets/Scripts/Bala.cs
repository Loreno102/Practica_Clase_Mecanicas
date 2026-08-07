using UnityEngine;

public class Bala : MonoBehaviour
{
    public float velMov;
    void Start()
    {
        Destroy(this.gameObject, 3f);
    }

    void Update()
    {
        transform.Translate(0, 0, velMov * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision col)
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);
    }
}
