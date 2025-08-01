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

        private void FixedUpdate()
        {
            Vector2 movementVector = -transform.position;
            movementVector += PlayerMovement.Instance.Playerlocation;
            movementVector = movementVector.normalized;

            _movement.Move(movementVector, Time.fixedDeltaTime);
        }
    }
}