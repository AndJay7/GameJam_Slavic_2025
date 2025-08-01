using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Survivor
{
    public class MovementAnimator : MonoBehaviour
    {
        [SerializeField]
        private float _angle;
        [SerializeField]
        private float _offset;
        [SerializeField]
        private float _speed;
        [SerializeField]
        private float _roughness;
        [SerializeField]
        private float _roughnessMin;
        [SerializeField]
        private Rigidbody2D _rigidbody;

        private Vector3 _previousPosition;
        private float _previousVelocitySqrt;
        
        private float _progress;

        private void OnEnable()
        {
            _previousPosition = transform.position;
        }

        private void Update()
        {
            var velocity = _rigidbody == null ? ((_previousPosition - transform.position).magnitude / Time.deltaTime) : _rigidbody.velocity.magnitude;
            var velocitySqrt = Mathf.Sqrt(velocity);
            velocitySqrt = Mathf.MoveTowards(_previousVelocitySqrt, velocitySqrt, Mathf.Max(_previousVelocitySqrt * _roughness * Time.deltaTime, _roughnessMin));
            
            _progress += velocitySqrt * _speed * Time.deltaTime;
            _progress %= Mathf.PI * 2f;

            var progressSin = Mathf.Sin(_progress);

            transform.localEulerAngles = new Vector3(0f, 0f, progressSin * velocitySqrt * _angle);
            transform.localPosition = new Vector3(0f, progressSin * progressSin * velocitySqrt * _offset, 0f);
            
            _previousPosition = transform.position;
            _previousVelocitySqrt = velocitySqrt;
        }
    }
}