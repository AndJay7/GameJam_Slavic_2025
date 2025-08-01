using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

namespace Survivor
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField]
        private Movement _movement;

        private void FixedUpdate()
        {
            var movementVector = Vector2.zero;

            if (Keyboard.current.wKey.isPressed)
                movementVector += Vector2.up;
            if (Keyboard.current.sKey.isPressed)
                movementVector -= Vector2.up;
            if (Keyboard.current.dKey.isPressed)
                movementVector += Vector2.right;
            if (Keyboard.current.aKey.isPressed)
                movementVector -= Vector2.right;

            if(movementVector.sqrMagnitude > 1f)
                movementVector.Normalize();
            
            _movement.Move(movementVector, Time.fixedDeltaTime);
        }
    }
}