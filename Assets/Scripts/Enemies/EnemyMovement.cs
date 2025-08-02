using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

namespace Survivor
{
    public class EnemyMovement : MonoBehaviour, ISpeed
    {
        [SerializeField]
        private Movement _movement;

        [SerializeField]
        private bool _scaleWithDistance;

        [SerializeField]
        private float _scaleWithDistanceOffset;

        private float fractionspeed = 1f;

        private float countdown = 0f;

        private void FixedUpdate()
        {
            Vector2 movementVector = -transform.position;
            movementVector += PlayerMovement.Instance.Playerlocation;

            if (_scaleWithDistance)
            {
                var distance = movementVector.magnitude;

                movementVector /= Mathf.Max(1e-7f, distance);

                movementVector *= distance + _scaleWithDistanceOffset;
                
                _movement.Move(movementVector*fractionspeed, Time.fixedDeltaTime);
            }
            else
            {
                if (movementVector.magnitude > 20)
                {
                    movementVector = movementVector.normalized;

                    _movement.Move(movementVector * fractionspeed * 10, Time.fixedDeltaTime);
                }
                else
                {
                    movementVector = movementVector.normalized;

                    _movement.Move(movementVector*fractionspeed, Time.fixedDeltaTime);
                }
            }

            if(countdown > 0) 
            {
                countdown -= Time.fixedDeltaTime;
            }
            else
            {
                if(fractionspeed != 1f)
                {
                    fractionspeed = 1f;
                }

            }

            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y * 0.01f);
        }



        public void Slow(float fraction, float duration)
        {
            fractionspeed = fraction;


            countdown = duration;


        }















    }
}