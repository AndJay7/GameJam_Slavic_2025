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
        private bool _flippable;
        [SerializeField]
        private float _flipCooldown;
        [SerializeField]
        private bool _constant;
        [SerializeField]
        private Rigidbody2D _rigidbody;
        [SerializeField]
        private float _stepSoundSpacing;
        [SerializeField]
        private float _stepSoundOffset;
        [SerializeField]
        private AudioSource _stepSoundSource;

        private Vector2 _previousPosition;
        private float _previousVelocitySqrt;
        
        private float _progress;

        private float _flipTime;

        private void OnEnable()
        {
            _previousPosition = transform.position;
        }

        private void Update()
        {
            var velocityVector = _rigidbody == null ? ((_previousPosition - (Vector2)transform.position) / Time.deltaTime) : _rigidbody.velocity;
            var velocity = velocityVector.magnitude;
            var velocitySqrt = Mathf.Sqrt(velocity);
            velocitySqrt = Mathf.MoveTowards(_previousVelocitySqrt, velocitySqrt, Mathf.Max(_previousVelocitySqrt * _roughness * Time.deltaTime, _roughnessMin));

            if (_constant)
            {
                velocity = 1f;
                velocitySqrt = 1f;
            }

            var progressStepIndex = Mathf.FloorToInt(_progress / (Mathf.PI * _stepSoundSpacing) + _stepSoundOffset);
            
            _progress += velocitySqrt * _speed * Time.deltaTime;

            if (progressStepIndex != Mathf.FloorToInt(_progress / (Mathf.PI * _stepSoundSpacing) + _stepSoundOffset) && _stepSoundSource != null)
            {
                _stepSoundSource.Play();
            }

            _progress %= Mathf.PI * 2f;
            
            var progressSin = Mathf.Sin(_progress);

            transform.localEulerAngles = new Vector3(0f, 0f, progressSin * velocitySqrt * _angle);
            transform.localPosition = new Vector3(0f, progressSin * progressSin * velocitySqrt * _offset, 0f);
            
            _previousPosition = transform.position;
            _previousVelocitySqrt = velocitySqrt;

            if (_flippable && Time.time > _flipTime && transform.localScale.x * velocityVector.x < 0f)
            {
                transform.localScale = new Vector3(transform.localScale.x * -1f, 1f, 1f);
                _flipTime = Time.time + _flipCooldown;
            }
        }
    }
}