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
        private float _radius;
        [SerializeField]
        private float _speed;

        public Vector2 Move(Vector2 position, Vector2 direction, float deltaTime)
        {
            var distance = _speed * deltaTime;
            
            var hit = Physics2D.CircleCast(position, _radius, direction, distance + Bias, LayerMask);

            if (hit.transform == null)
            {
                return position + direction * distance;
            }

            position += direction * (hit.distance - Bias);
            direction += Vector2.Dot(direction, hit.normal) * hit.normal;
            direction.Normalize();
            distance -= hit.distance - Bias;
            
            hit = Physics2D.CircleCast(position, _radius, direction, distance + Bias, LayerMask);

            if (hit.transform == null)
            {
                return position + direction * distance;
            }
            
            position += direction * (hit.distance - Bias);

            return position;
        }
    }
}