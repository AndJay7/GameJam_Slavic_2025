using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;


namespace Survivor
{

    public class ProjectilleScript : MonoBehaviour
    {
        private Vector2 ToPlayer;



        [SerializeField] float speed = 5f;
        // Start is called before the first frame update
        void Start()
        {
            ToPlayer = -transform.position;
            ToPlayer += PlayerMovement.Instance.Playerlocation;
            ToPlayer = ToPlayer.normalized;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            transform.Translate(ToPlayer * Time.fixedDeltaTime * speed);
        }

        void OnCollisionEnter2D()
        {
            Destroy(gameObject);


        }

    }
}
