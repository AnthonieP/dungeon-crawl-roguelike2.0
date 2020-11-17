using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float damage;
    public float time = 1;
    public bool piercing = false;
    public GameObject destroyParticle;

    void Start()
    {
        
    }

    void Update()
    {
        transform.Translate(0, 0, speed * Time.deltaTime);
        time -= Time.deltaTime;
        if(time < 0)
        {
            DestroyProjectile();
        }
    }

    void DestroyProjectile()
    {
        if(destroyParticle != null)
        {
            Instantiate(destroyParticle, transform.position, transform.rotation);
        }


        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "enemy")
        {
            if (!piercing)
            {
                DestroyProjectile();
            }
        }
        else if(other.transform.tag == "Player")
        {

        }
        else
        {
            DestroyProjectile();
        }
    }
}
