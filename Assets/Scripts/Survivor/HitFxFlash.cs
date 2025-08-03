using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Survivor
{
    public class HitFxFlash : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer _spriteRenderer;
        [SerializeField]
        private AnimationCurve _curve;
        [SerializeField]
        private AudioSource _sound;
        
        private float _startTime;
        private SpriteRenderer _parent;
        
        private void Start()
        {
            _startTime = Time.time;
            _parent = GetComponentInParent<SpriteRenderer>();
            
            if(_sound != null)
                _sound.Play();
        }

        private void LateUpdate()
        {
            var delta = Time.time - _startTime;
            
            _spriteRenderer.color = Color.white * _curve.Evaluate(delta);
            _spriteRenderer.sprite = _parent.sprite;
            _spriteRenderer.flipX = _parent.flipX;
            
            if(delta > _curve.length && (_sound == null || !_sound.isPlaying))
                Destroy(gameObject);
        }
    }
}