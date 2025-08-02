using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

namespace Survivor
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField]
        private Movement _movement;

        [SerializeField]
        private bool _scaleWithDistance;

        [SerializeField]
        private float _scaleWithDistanceOffset;

        private void FixedUpdate()
        {
            Vector2 movementVector = -transform.position;
            movementVector += PlayerMovement.Instance.Playerlocation;

            if (_scaleWithDistance)
            {
                var distance = movementVector.magnitude;

                movementVector /= Mathf.Max(1e-7f, distance);

                movementVector *= distance + _scaleWithDistanceOffset;
                
                _movement.Move(movementVector, Time.fixedDeltaTime);
            }
            else
            {
                if (movementVector.magnitude > 20)
                {
                    movementVector = movementVector.normalized;

                    _movement.Move(movementVector * 10, Time.fixedDeltaTime);
                }
                else
                {
                    movementVector = movementVector.normalized;

                    _movement.Move(movementVector, Time.fixedDeltaTime);
                }
            }

            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y * 0.01f);
        }
    }
}