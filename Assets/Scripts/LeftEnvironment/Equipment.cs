using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment
{
    public event Action<Vector2Int> OnItemAdded;
    public event Action<Vector2Int, Vector2Int> OnItemSwapStart;
    public event Action<Vector2Int, Vector2Int> OnItemSwapEnd;
    public event Action<Vector2Int> OnItemSelected;
    public event Action<Vector2Int> OnItemDeselect;
    public event Action<Vector2Int> OnFocusChanged;

    public Vector2Int SelectIndex => _selectionIndex;
    public Vector2Int FocusIndex => _focusIndex;
    public Vector2Int PreviousFocusIndex => _previousFocusIndex;
    public const int GridSize = 40;

    private readonly ItemSlot[,] _itemSlots = new ItemSlot[GridSize, GridSize];
    private Vector2Int _previousFocusIndex = new Vector2Int(GridSize / 2, GridSize / 2);
    private Vector2Int _focusIndex = new Vector2Int(GridSize / 2, GridSize / 2);

    private Vector2Int _selectionIndex = new Vector2Int(-1, -1);

    public void SetFocusItem(Vector2Int offset)
    {
        var newFocus = _focusIndex + offset;
        newFocus.x = Mathf.Clamp(newFocus.x, 0, GridSize-1);
        newFocus.y = Mathf.Clamp(newFocus.y, 0, GridSize-1);

        if(newFocus != _focusIndex)
        {
            _previousFocusIndex = _focusIndex;
            _focusIndex = newFocus;
            OnFocusChanged?.Invoke(newFocus);
        }
    }

    internal void Dispose()
    {
        foreach(var slot in _itemSlots)
        {
            if (slot.item is Weapon w)
            {
                w.Ability.Stop();
                w.Ability.RemoveAllBoosters();
            }
        }
    }

    public void SelectItem()
    {
        _selectionIndex = _focusIndex;

        OnItemSelected?.Invoke(_selectionIndex);
    }

    public void Deselect()
    {
        var prevSelect = _selectionIndex;
        _selectionIndex = new Vector2Int(-1, -1);
        OnItemDeselect?.Invoke(prevSelect);
    }

    public bool CanSwapItem()
    {
        return _selectionIndex != _focusIndex;
    }

    public void SwapItem()
    {
        var startSlot = _itemSlots[_selectionIndex.x, _selectionIndex.y];
        var endSlot = _itemSlots[_focusIndex.x, _focusIndex.y];

        OnItemSwapStart?.Invoke(_selectionIndex, _focusIndex);
        (startSlot.item, endSlot.item) = (endSlot.item, startSlot.item);
        _itemSlots[_selectionIndex.x, _selectionIndex.y] = startSlot;
        _itemSlots[_focusIndex.x, _focusIndex.y] = endSlot;
        OnItemSwapEnd?.Invoke(_selectionIndex, _focusIndex);
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

    public List<Vector2Int> GetNeighbourIndexes(Vector2Int index)
    {
        var list = new List<Vector2Int>();

        var newIndex = index + new Vector2Int(0, 1);
        if (IsIndexValid(newIndex))
            list.Add(newIndex);
        newIndex = index + new Vector2Int(0, -1);
        if (IsIndexValid(newIndex))
            list.Add(newIndex);
        newIndex = index + new Vector2Int(1, 0);
        if (IsIndexValid(newIndex))
            list.Add(newIndex);
        newIndex = index + new Vector2Int(-1, 0);
        if (IsIndexValid(newIndex))
            list.Add(newIndex);
        return list;
    }

    public bool IsIndexValid(Vector2Int index)
    {
        var minValid = index.x >= 0 && index.y >= 0;
        var maxValid = index.x < GridSize && index.y < GridSize;
        return minValid && maxValid;
    }

    public ItemSlot GetSlot(Vector2Int index)
    {
        return _itemSlots[index.x, index.y];
    }
}
