using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IHealth
{
    [SerializeField] float starthealth = 100f;
    // Start is called before the first frame update
    [SerializeField] float damagetoplayer = 1f;

    public void TakeDamage(float damage)
    {
        starthealth -= damage;
       if(starthealth <= 0)
        {
            Destroy(gameObject);


        }

    }



    public float DealDamage()
    {
        return damagetoplayer;
    }
}
