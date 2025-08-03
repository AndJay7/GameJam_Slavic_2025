using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyHealth : MonoBehaviour, IHealth
{
    [SerializeField] float starthealth = 100f;
    // Start is called before the first frame update
    [SerializeField] float damagetoplayer = 1f;

    [SerializeField] GameObject popup;

    [SerializeField] GameObject pickup;

    [SerializeField]
    private bool _invincible;

    [SerializeField]
    private bool _boss;
    
    [SerializeField]
    private GameObject _hitFXFlash;
    [SerializeField]
    private AudioSource _hitSFX;

    private float _maxHealth;

    private void Awake()
    {
        _maxHealth = starthealth;
    }

    /*
    public static List<EnemyHealth> Instances = new List<EnemyHealth>();

    void OnEnable()
    {
        Instances.Add(this);
        Debug.Log(Instances.Count);
    }

    void OnDisable()
    {
        Instances.Remove(this);
    }

    */

    private void Update()
    {
        if (_boss)
        {
            BossHealthBar.Instance.SetScale(Mathf.Max(0f, starthealth / _maxHealth));
        }
    }

    public void TakeDamage(float damage)
    {
        if(_invincible)
            return;
        
        if (damage > 0)
        {
            if (_hitSFX != null)
                _hitSFX.Play();
            
            Instantiate(_hitFXFlash, GetComponentInChildren<SpriteRenderer>().transform);
        }
        
        starthealth -= damage;

        if (starthealth <= 0 && _boss)
        {
            SceneManager.LoadScene("WinScreen");
        }
        
        if (popup != null) 
        { 
         GameObject obj = (GameObject)Instantiate(popup, transform.position, transform.rotation);
        obj.GetComponent<DamagePopup>().Number(damage);
        }
        




        if (starthealth <= 0)
        {
            if (Itemspawner.Instance.itemstospawn > 0)
            {
                //Debug.Log("Spawn");
                Instantiate(pickup, transform.position, transform.rotation);
                Itemspawner.Instance.itemstospawn--;


            }


            Destroy(gameObject);


        }

    }



    public float DealDamage()
    {
        return damagetoplayer;
    }

    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
