using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 200f;
    public float health = 200f;
    public float multiplier = 5f;
    
    [SerializeField]
    private Transform _healthBar;

    void FixedUpdate()
    {
        health -= Time.fixedDeltaTime * multiplier;
        multiplier = 0f;

        if (_healthBar != null)
        {
            _healthBar.localScale = new Vector3(Mathf.Max(0f, health / maxHealth), 1f, 1f);
        }
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
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            if (collision.gameObject.TryGetComponent(out IHealth something))
            {
                multiplier += something.DealDamage();

            }



        }



    }



}
