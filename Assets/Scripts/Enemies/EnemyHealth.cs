using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IHealth
{
    [SerializeField] float starthealth = 100f;
    // Start is called before the first frame update
    [SerializeField] float damagetoplayer = 1f;

    [SerializeField] GameObject popup;

    [SerializeField] GameObject pickup;
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

    public void TakeDamage(float damage)
    {
        starthealth -= damage;

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
