using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    public float bulletSpeed = 40f;
    private Rigidbody bulletRigidbody;

    // Start is called before the first frame update
    private void Awake()
    {
        bulletRigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        bulletRigidbody.velocity = transform.forward * bulletSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag != "Player")
        {
            if (other.transform.gameObject.TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
            {
                rigidbody.AddExplosionForce(-150f, transform.position, 5f);
            }
            Destroy(gameObject);
        }
    }
}