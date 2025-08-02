using System;
using System.Collections;
using System.Collections.Generic;
using Survivor;
using UnityEngine;
using Random = UnityEngine.Random;

[DefaultExecutionOrder(-2077)]
public class ItemPool : MonoBehaviour
{
    public static ItemPool Instance { get; set; }

    [SerializeField]
    private ItemDatabaseSO _database;

    private float[] _currentWeights;
    
    private void Start()
    {
        var items = GameManager.Instance.ItemDatabase.GetItems();
        
        _currentWeights = new float[items.Count];
        
        for(int i = 0; i < items.Count; i++)
        {
            _currentWeights[i] = items[i].Weight;
        }
        
        Instance = this;
    }

    public Item Pick()
    {
        var totalWeight = 0f;
        var maxWeight = 1e-3f;
        
        var items = GameManager.Instance.ItemDatabase.GetItems();

        for(int i = 0; i < items.Count; i++)
        {
            totalWeight += _currentWeights[i];
            maxWeight = Mathf.Max(maxWeight, _currentWeights[i]);
        }
        
        for(int i = 0; i < items.Count; i++)
        {
            _currentWeights[i] /= maxWeight;
        }

        var pickWeight = Random.Range(0f, totalWeight);
        var pickIndex = 0;

        var cumulativeWeight = 0f;
        for (int i = 0; i < items.Count; i++)
        {
            var item = items[i];
            
            cumulativeWeight += _currentWeights[i];

            if (cumulativeWeight > pickWeight)
            {
                pickIndex = i;
                break;
            }
        }
        
        var pickItem = items[pickIndex];

        _currentWeights[pickIndex] *= pickItem.Weight;
        
        return pickItem;
    }
}
