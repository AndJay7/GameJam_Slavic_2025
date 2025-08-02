using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ItemQueueView : MonoBehaviour
{
    [SerializeField]
    private ItemQueueSlotView _slotPrefab;
    [SerializeField]
    private Transform _itemContainer;
    [SerializeField]
    private TextMeshProUGUI _descriptionText;
    [SerializeField]
    private GameObject _selection;

    private GameManager _gameManager;
    private readonly Queue<ItemQueueSlotView> _slots = new Queue<ItemQueueSlotView>();

    private void Start()
    {
        _gameManager = GameManager.Instance;
        Initialize();
        InitializeEvents();
    }

    private void Initialize()
    {
        foreach(var item in _gameManager.ItemsQueue.Items)
        {
            AddItem(item);
        }
        UpdateDescription();
        UpdateSelection();
    }

    private void InitializeEvents()
    {
        _gameManager.ItemsQueue.OnItemAdded += AddItem;
        _gameManager.ItemsQueue.OnItemRemoved += RemoveItem;
    }

    private void RemoveItem(Item obj)
    {
        var slot = _slots.Dequeue();
        Destroy(slot.gameObject);
        UpdateDescription();
        UpdateSelection();
    }

    private void AddItem(Item item)
    {
        var slot = Instantiate(_slotPrefab, _itemContainer);
        slot.SetItem(item);
        _slots.Enqueue(slot);
        UpdateDescription();
        UpdateSelection();
    }

    private void UpdateSelection()
    {
        _selection.gameObject.SetActive(_slots.Count > 0);
    }

    private void UpdateDescription()
    {
        var description = string.Empty;
        if(_slots.Count != 0)
        {
            var item = _slots.Peek().Item;
            var text = $"Name: {item.Name}\n";
            text += $"Description: {item.Description}";
            description = text;
        }
        _descriptionText.text = description;
    }
}
