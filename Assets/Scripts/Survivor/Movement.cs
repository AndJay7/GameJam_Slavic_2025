using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Survivor
{
    [Serializable]
    public class Movement
    {
        private const float Bias = 1f / (1 << 10);
        private const int LayerMask = 1 << 10;
        
        [SerializeField]
        private float _acceleration;
        [SerializeField]
        private float _speed;
        [SerializeField]
        private Rigidbody2D _rigidbody;

        public void Move(Vector2 movementVector, float deltaTime)
        {
            _rigidbody.velocity = Vector2.MoveTowards(_rigidbody.velocity, movementVector * _speed, _acceleration * deltaTime);
        }
    }
}