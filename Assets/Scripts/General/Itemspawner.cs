using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Itemspawner : MonoBehaviour
{

    public float itemtimer = 4f;

    public int itemstospawn = 0;


    public static Itemspawner Instance { get; private set; }

    private void Awake()
    {

        Instance = this;

    }





    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (itemtimer > 0) 
        {
            itemtimer -= Time.fixedDeltaTime;
               
                
        }
        else
        {
            itemstospawn++;
            //Debug.Log(itemstospawn);
            itemtimer = 4f;

        }
        
    }
}
