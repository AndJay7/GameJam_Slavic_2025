using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health = 200f;
    public float multiplier = 5f;
    // Start is called before the first frame update
    void Start()
    {

    }

    void FixedUpdate()
    {
        health -= Time.fixedDeltaTime * multiplier;
        multiplier = 0f;

    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            if (collision.gameObject.TryGetComponent(out IHealth something))
            {
                multiplier += something.DealDamage();

            }



        }



    }
    void OnCollisionEnter2D(Collision2D collision) 
    
    { 

      if (collision.gameObject.tag == "projectile")
      health -= 20;
       
    }
}
