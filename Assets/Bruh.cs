using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(2077)]
public class Bruh : MonoBehaviour
{
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y * 0.01f);
    }
}
