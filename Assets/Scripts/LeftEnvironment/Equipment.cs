using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment
{
    public event Action<Vector2Int> OnItemAdded;
    public event Action<Vector2Int, Vector2Int> OnItemSwaped;
    public event Action<Vector2Int> OnItemSelected;
    public event Action<Vector2Int> OnFocusChanged;

    public Vector2Int FocusIndex => _focusIndex;
    public Vector2Int PreviousFocusIndex => _previousFocusIndex;
    public ItemSlot[,] ItemSlots => _itemSlots;

    private const int _gridSize = 40;
    private readonly ItemSlot[,] _itemSlots = new ItemSlot[_gridSize, _gridSize];
    private Vector2Int _previousFocusIndex = new Vector2Int(_gridSize / 2, _gridSize / 2);
    private Vector2Int _focusIndex = new Vector2Int(_gridSize / 2, _gridSize / 2);

    private Vector2Int _selectionIndex = new Vector2Int(-1, -1);

    public void SetFocusItem(Vector2Int offset)
    {
        var newFocus = _focusIndex + offset;
        newFocus.x = Mathf.Clamp(newFocus.x, 0, _gridSize);
        newFocus.y = Mathf.Clamp(newFocus.y, 0, _gridSize);

        if(newFocus != _focusIndex)
        {
            _previousFocusIndex = _focusIndex;
            _focusIndex = newFocus;
            OnFocusChanged?.Invoke(newFocus);
        }
    }

    public void SelectItem()
    {
        _selectionIndex = _focusIndex;

        OnItemSelected?.Invoke(_selectionIndex);
    }

    public bool CanSwapItem()
    {
        return _selectionIndex != _focusIndex;
    }

    public void SwapItem()
    {
        var startSlot = _itemSlots[_selectionIndex.x, _selectionIndex.y];
        var endSlot = _itemSlots[_focusIndex.x, _focusIndex.y];

        (startSlot.item, endSlot.item) = (endSlot.item, startSlot.item);
        OnItemSwaped?.Invoke(_selectionIndex, _focusIndex);
    }

    public bool CanAddItem()
    {
        return _itemSlots[_focusIndex.x, _focusIndex.y].item == null;
    }

    public void AddItem(Item item)
    {
        _itemSlots[_focusIndex.x, _focusIndex.y].item = item;
        OnItemAdded?.Invoke(_focusIndex);
    }

}
