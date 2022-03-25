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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag != "Player")
        {
            if (collision.transform.gameObject.TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
            {
                rigidbody.AddExplosionForce(-150f, transform.position, 5f);
            }
            //Recycle this pooled bullet instance
            gameObject.Recycle();
        }
    }
}