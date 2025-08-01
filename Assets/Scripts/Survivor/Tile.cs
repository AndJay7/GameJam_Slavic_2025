using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Survivor
{
    public class Tile : MonoBehaviour
    {
        [SerializeField]
        private int2 _size;
        [SerializeField]
        private float _weight;
        [SerializeField]
        private bool _flippable;

        public int2 Size => _size;
        public float Weight => _weight;
        public virtual bool Flippable => _flippable;

        protected void Start()
        {
            transform.position += Vector3.forward * transform.position.y * 0.01f;
            
            if(Flippable)
                transform.localScale = new Vector3(Random.Range(0f, 1f) >= 0.5f ? 1f : -1f, 1f, 1f);
        }
    }
}