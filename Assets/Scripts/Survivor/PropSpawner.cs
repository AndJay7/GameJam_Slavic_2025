using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Survivor
{
    public class PropSpawner : MonoBehaviour
    {
        [SerializeField]
        private float2 _spawnArea;
        [SerializeField]
        private int2 _spawnCount;
        [SerializeField]
        private bool _flippable;
        [SerializeField]
        private GameObject[] _props;

        private void Start()
        {
            var spawnCount = Random.Range(_spawnCount.x, _spawnCount.y);

            for (int i = 0; i < spawnCount; i++)
            {
                var prop = Instantiate(_props[Random.Range(0, _props.Length)], transform);
                prop.transform.localPosition += new Vector3(Snap(Random.Range(-_spawnArea.x, _spawnArea.x) * 0.5f), Snap(Random.Range(-_spawnArea.y, _spawnArea.y) * 0.5f), 0);

                if (_flippable)
                    prop.transform.localScale = new Vector3(Random.Range(0f, 1f) >= 0.5f ? 1f : -1f, 1f, 1f);
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.HSVToRGB(0.05f, 1f, 1f);
            Gizmos.DrawWireCube(transform.position, new Vector3(_spawnArea.x, _spawnArea.y, 0f));
        }

        private float Snap(float v)
        {
            return Mathf.Round(v * 16f) / 16f;
        }
    }
}