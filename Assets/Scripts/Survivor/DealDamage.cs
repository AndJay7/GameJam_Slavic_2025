using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour
{
    public float damage;

    public bool continous;

    public int startskip = 2;

    public int skip = 2;

    public bool iscapped = false;

    public int cap = 5;

    void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (continous == false)
        {
            if (collision.gameObject.tag == "enemy" || collision.gameObject.tag == "boss")
            {
                if (collision.gameObject.TryGetComponent(out IHealth something))
                {
                    something.TakeDamage(damage);

                }
                if(iscapped == true) 
                { 
                    cap--;
                    if (cap < 1)
                    {
                       Destroy(transform.parent.gameObject);
                    }
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
                if (skip == 0)
                {

                    if (collision.gameObject.TryGetComponent(out IHealth something))
                {
                    something.TakeDamage(damage);

                }
                    if (iscapped == true)
                    {
                        cap--;
                        if (cap < 1)
                        {
                            Destroy(transform.parent.gameObject);
                        }
                    }
                    skip = startskip;
                }
                else
                    {
                        skip--;
                    }
                    
                
             
                
             }
            
        }
    }
}
