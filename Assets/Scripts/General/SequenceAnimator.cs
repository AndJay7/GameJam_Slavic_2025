using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Survivor
{
    public class SequenceAnimator : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer _spriteRenderer;
        [SerializeField]
        private float _framerate;
        [SerializeField]
        private Sprite[] _sequence;
        [SerializeField]
        private bool _destroyOnEnd;
        
        private float _startTime;

        private void Start()
        {
            _startTime = Time.time;
        }

        private void Update()
        {
            var spriteIndex = Mathf.FloorToInt((Time.time - _startTime) * _framerate);

            if (spriteIndex >= _sequence.Length && _destroyOnEnd)
            {
                Destroy(gameObject);
                return;
            }
            
            spriteIndex %= _sequence.Length;
            _spriteRenderer.sprite = _sequence[spriteIndex];
        }
    }
}
