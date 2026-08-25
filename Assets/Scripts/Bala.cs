using UnityEngine;

public class Bala : MonoBehaviour
{
    public float velMov;
    public GameObject explosion;
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
        Instantiate(explosion, this.transform.position, this.transform.rotation);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);
    }
}
