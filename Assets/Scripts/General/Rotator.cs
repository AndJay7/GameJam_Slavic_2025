using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-1337)]
public class Rotator : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    private void Update()
    {
        transform.Rotate(0f, 0f, _speed * Time.deltaTime);
    }
}
