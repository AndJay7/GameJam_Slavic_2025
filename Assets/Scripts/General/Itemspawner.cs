using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Itemspawner : MonoBehaviour
{

    public float itemtimer = 10f;

    public int itemstospawn = 0;

    private float realtimer;

    public static Itemspawner Instance { get; private set; }

    private void Awake()
    {
        realtimer = itemtimer;
        Instance = this;

    }





    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (realtimer > 0) 
        {
            realtimer -= Time.fixedDeltaTime;
               
                
        }
        else
        {
            itemstospawn++;
            //Debug.Log(itemstospawn);
            realtimer = itemtimer;

        }
        
    }
}
