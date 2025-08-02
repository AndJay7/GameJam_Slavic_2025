using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour
{
    public float damage;

    public bool continous;


    void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (continous == false)
        {
            if (collision.gameObject.tag == "enemy")
            {
                if (collision.gameObject.TryGetComponent(out IHealth something))
                {
                    something.TakeDamage(damage);

                }

            }
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (continous == true)
        {
            if (collision.gameObject.tag == "enemy")
            {
                if (collision.gameObject.TryGetComponent(out IHealth something))
                {
                    something.TakeDamage(damage);

                }

            }
        }
    }
}
