using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : MonoBehaviour
{
    [SerializeField] float firerate = 10f;

    [SerializeField] GameObject projectile;

    private float cooldown;

    void Awake()
    {
        cooldown = firerate;

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(cooldown > 0)
        {
            cooldown -= Time.fixedDeltaTime;

        }
        else
        {
            Instantiate(projectile, transform.position, Quaternion.identity);

            cooldown = firerate;
        }




    }
}
