using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

namespace Survivor
{
    public class SanchoMovement : MonoBehaviour
    {
        [SerializeField]
        private Movement _movement;
        [SerializeField]
        private float _moveAwayDistance;
        [SerializeField]
        private float _moveTowardDistance;
        [SerializeField]
        private float _smoothDistance = 1e-5f;
        [SerializeField]
        private float _growingSpeedOffset = 2f;

        private void FixedUpdate()
        {
            var movementVector = Vector2.zero;

            var delta = (Vector2)transform.position - (Vector2)PlayerMovement.Instance.transform.position;
            var distance = delta.magnitude;
            movementVector = delta / Mathf.Max(_smoothDistance, distance);
            movementVector *= Mathf.Sqrt(distance + _growingSpeedOffset);

            if (distance >= _moveTowardDistance)
            {
                movementVector *= -1f;
            } else if (distance > _moveAwayDistance)
            {
                movementVector *= 0f;
            }
            
            _movement.Move(movementVector, Time.fixedDeltaTime);

            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y * 0.01f);
        }
    }
}