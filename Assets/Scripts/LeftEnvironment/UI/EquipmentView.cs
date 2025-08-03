using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentView : MonoBehaviour
{
    [SerializeField]
    private AudioSource _audioItemSwap;
    [SerializeField]
    private AudioSource _audioItemSelect;
    [SerializeField]
    private Transform _focusPoint;
    [SerializeField]
    private Transform _itemContainer;
    [SerializeField]
    private ItemSlotView _itemSlotPrefab;

    private const float _slotSize = 1.25f;
    private GameManager _gameManager;
    private ItemSlotView[,] _itemSlots;

    private void Start()
    {
        _gameManager = GameManager.Instance;
        InitializeSlots();
        InitializeEvents();
    }

    private void InitializeSlots()
    {
        var gridSize = Equipment.GridSize;
        _itemSlots = new ItemSlotView[gridSize, gridSize];
        for(int x = 0; x < gridSize;x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                var itemSlot = Instantiate(_itemSlotPrefab, _itemContainer);
                itemSlot.transform.localPosition = new Vector3(x * _slotSize, y * _slotSize, 0);
                _itemSlots[x, y] = itemSlot;
            }
        }
        SetFocusPointPosition(_gameManager.Equipment.FocusIndex);
    }

    private void InitializeEvents()
    {
        _gameManager.Equipment.OnFocusChanged += SetFocusPointPosition;
        _gameManager.Equipment.OnItemAdded += AddItem;
        _gameManager.Equipment.OnItemSelected += SelectItem;
        _gameManager.Equipment.OnItemDeselect += DeselectItem;
        _gameManager.Equipment.OnItemSwapEnd += SwapItems;
    }

    private void SwapItems(Vector2Int startIndex, Vector2Int endIndex)
    {
        _audioItemSwap.Play();
        AddItem(startIndex);
        AddItem(endIndex);
    }

    private void DeselectItem(Vector2Int index)
    {
        _itemSlots[index.x, index.y].SetSelection(false);
    }

    private void SelectItem(Vector2Int index)
    {
        _itemSlots[index.x, index.y].SetSelection(true);
        _audioItemSelect.Play();
    }

    private void AddItem(Vector2Int index)
    {
        var item = _gameManager.Equipment.GetSlot(index).item;

        _itemSlots[index.x, index.y].SetItem(item);
    }

    private void SetFocusPointPosition(Vector2Int index)
    {
        var position = _itemSlots[index.x, index.y].transform.localPosition;
        _focusPoint.transform.localPosition = position;
    }

}
