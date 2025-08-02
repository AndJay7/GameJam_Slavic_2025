using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private float maxHealth = 200f;
    private float health;
    private float multiplier = 0;

    private float visibleHealth;
    private float healthChangeVel;
    [SerializeField]
    private Transform _healthBar;

    private void Awake()
    {
        health = visibleHealth = maxHealth;
    }

    private void Update()
    {
        visibleHealth = Mathf.SmoothDamp(visibleHealth, health, ref healthChangeVel, 0.1f);

        if (_healthBar != null)
        {
            _healthBar.localScale = new Vector3(Mathf.Max(0f, visibleHealth / maxHealth), 1f, 1f);
        }
    }

    void FixedUpdate()
    {
        AddHealth(-Time.fixedDeltaTime * multiplier);
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
            AddHealth(-20); 
    }

    public void AddHealth(float addHealth)
    {
        health += addHealth;
        health = Mathf.Clamp(health, 0, maxHealth);

        if (health == 0)
            UnityEngine.SceneManagement.SceneManager.LoadScene("LoseScreen", UnityEngine.SceneManagement.LoadSceneMode.Single);
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
