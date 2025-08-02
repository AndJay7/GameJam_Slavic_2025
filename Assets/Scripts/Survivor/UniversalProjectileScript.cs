using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniversalProjectileScript : MonoBehaviour
{

    [SerializeField] float speed = 5f;
    [SerializeField] bool istimed = false;
    [SerializeField] float duration = 4f;

    private float failsafe = 30f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(Vector2.right * speed * Time.fixedDeltaTime);
        if(istimed == true)
        {
            duration -= Time.fixedDeltaTime;
            if(duration < 0)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            failsafe -= Time.fixedDeltaTime;
            if (failsafe < 0)
            {
                Destroy(gameObject);
            }

        }

    }
}
