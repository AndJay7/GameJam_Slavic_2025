using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Survivor
{
    public class WorldGenerator : MonoBehaviour
    {
        [SerializeField]
        private int _generateRadius;
        [SerializeField]
        private int _deleteRadius;
        [SerializeField]
        private int _deleteIterations;
        [SerializeField]
        private int _gridSize;
        [SerializeField]
        private Tile[] _tiles;

        private float _tilesTotalWeight;

        private Dictionary<int2, (Tile tile, RectInt rect)> _tileMap = new();
        private List<(Tile tile, RectInt rect)> _tileInstances = new();

        private int _deleteIndex = 0;
        
        private void OnValidate()
        {
            Array.Sort(_tiles, (a, b) => a.name.CompareTo(b.name));
        }

        private void OnEnable()
        {
            Array.Sort(_tiles, (a, b) => b.Weight.CompareTo(a.Weight));
            
            _tilesTotalWeight = 0f;
            foreach (var tile in _tiles)
            {
                _tilesTotalWeight += tile.Weight;
            }
        }

        private void Update()
        {
            var center = new int2(Mathf.RoundToInt(transform.position.x / _gridSize), Mathf.RoundToInt(transform.position.y / _gridSize));

            TryPlaceTile(center, center - new int2(1, 1));
            
            for (int r = 1; r <= _generateRadius; r++)
            {
                for (int c = -r; c < r; c++)
                {
                    TryPlaceTile(center + new int2(-r, c), center);
                    TryPlaceTile(center + new int2(c, r), center);
                    TryPlaceTile(center + new int2(r, -c), center);
                    TryPlaceTile(center + new int2(-c, -r), center);
                }
            }

            for (int i = 0; i < _deleteIterations && _tileInstances.Count > 0; i++)
            {
                _deleteIndex++;
                _deleteIndex %= _tileInstances.Count;
                
                var entry = _tileInstances[_deleteIndex];

                var deleted = false;
                if (entry.tile == null)
                {
                    deleted = true;
                }
                else if (Vector3.Distance(entry.tile.transform.position, transform.position) >= _deleteRadius * _gridSize)
                {
                    deleted = true;
                    
                    for (int x = entry.rect.xMin; x <= entry.rect.xMax; x++)
                    {
                        for (int y = entry.rect.yMin; y <= entry.rect.yMax; y++)
                        {
                            _tileMap.Remove(new int2(x, y));
                        }
                    }
                    
                    Destroy(entry.tile.gameObject);
                }

                if (deleted)
                {
                    _tileInstances.RemoveAtSwapBack(_deleteIndex);
                }
            }
        }

        private void TryPlaceTile(int2 position, int2 center)
        {
            if (_tileMap.ContainsKey(position))
                return;

            var expandSign = Sign(position - center);

            var pickWeight = Random.Range(0f, _tilesTotalWeight);
            var pickIndex = 0;
            var pickTile = _tiles[pickIndex];

            var cumulativeWeight = 0f;
            for (int i = 0; i < _tiles.Length; i++)
            {
                cumulativeWeight += _tiles[i].Weight;

                if (cumulativeWeight >= pickWeight)
                {
                    pickIndex = i;
                    pickTile = _tiles[i];
                }
            }

            PickAgain:
            
            var rangeX = new int2(position.x, position.x + (pickTile.Size.x - 1) * expandSign.x);
            var rangeY = new int2(position.y, position.y + (pickTile.Size.y - 1) * expandSign.y);

            OrderRange(ref rangeX);
            OrderRange(ref rangeY);

            for (int x = rangeX.x; x <= rangeX.y; x++)
            {
                for (int y = rangeY.x; y <= rangeY.y; y++)
                {
                    if (_tileMap.ContainsKey(new int2(x, y)))
                    {
                        pickIndex--;
                        pickTile = _tiles[pickIndex];
                        goto PickAgain;
                    }
                }
            }

            var tile = Instantiate
                (
                    pickTile.gameObject,
                    new Vector3((rangeX.x + rangeX.y) * 0.5f * _gridSize, (rangeY.x + rangeY.y) * 0.5f * _gridSize),
                    Quaternion.identity
                )
                .GetComponent<Tile>();
            
            var tileRect = (tile, new RectInt(rangeX.x, rangeY.x, rangeX.y - rangeX.x + 1, rangeY.y - rangeY.x + 1));

            for (int x = rangeX.x; x <= rangeX.y; x++)
            {
                for (int y = rangeY.x; y <= rangeY.y; y++)
                {
                    _tileMap[new int2(x, y)] = tileRect;
                }
            }
            
            _tileInstances.Add(tileRect);
        }

        private void OrderRange(ref int2 range)
        {
            if(range.x > range.y)
                (range.x, range.y) = (range.y, range.x);
        }

        private int2 Sign(int2 v)
        {
            return new int2(v.x > 0 ? 1 : -1, v.y > 0 ? 1 : -1);
        }
    }
}