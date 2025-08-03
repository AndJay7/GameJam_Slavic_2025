using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private float maxHealth = 200f;
    private float health;
    private float multiplier = 0;

    private float visibleHealth;
    private float healthChangeVel;
    [SerializeField]
    private Transform _healthBar;

    [SerializeField]
    private float _hitCooldownOnActivate;
    [SerializeField]
    private float _hitCooldownDamageDecrement;
    [SerializeField]
    private Vector2 _hitFXPosition;
    [SerializeField]
    private Vector2 _hitFXRandomPosition;
    [SerializeField]
    private GameObject _hitFX;
    [SerializeField]
    private GameObject _hitFXFlash;

    private float _hitCooldown;
    
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

        _hitCooldown -= Time.deltaTime;
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

    void OnTriggerEnter2D(Collider2D collision)     
    { 

      if (collision.gameObject.tag == "projectile")
            AddHealth(-20); 
    }

    public void AddHealth(float addHealth)
    {
        if (addHealth < 0)
        {
            _hitCooldown /= 1f + addHealth * addHealth * _hitCooldownDamageDecrement;
                
            if (_hitCooldown <= 0f)
            {
                var hitFXPosition = (Vector2)transform.position + _hitFXPosition;
                hitFXPosition.x += Random.Range(-_hitFXRandomPosition.x, _hitFXRandomPosition.x);
                hitFXPosition.y += Random.Range(-_hitFXRandomPosition.y, _hitFXRandomPosition.y);
                
                Instantiate(_hitFX, hitFXPosition, Quaternion.identity);
                Instantiate(_hitFXFlash, GetComponentInChildren<SpriteRenderer>().transform);

                _hitCooldown = _hitCooldownOnActivate;
            }
        }
        
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
