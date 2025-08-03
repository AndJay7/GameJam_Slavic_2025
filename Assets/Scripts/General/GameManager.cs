using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private ItemDatabaseSO _itemsDatabase;
    [SerializeField]
    private List<ItemSO> _startEquipment;
    [SerializeField]
    private List<ItemSO> _startQueue;

    public ItemDatabaseSO ItemDatabase => _itemsDatabase;
    public ItemsQueue ItemsQueue { get; } = new ItemsQueue();
    public Equipment Equipment { get; } = new Equipment();
    protected override bool DontDestroy => false;

    protected override void Awake()
    {
        base.Awake();
        Equipment.OnItemAdded += ItemAdded;
        Equipment.OnItemSwapStart += ItemSwapStart;
        Equipment.OnItemSwapEnd += ItemSwapEnd;       
    }

    private void Start()
    {
        foreach (var itemSO in _startQueue)
            ItemsQueue.Enqueue(itemSO.Item);

        foreach (var itemSO in _startEquipment)
        {
            Equipment.AddItem(itemSO.Item);
            Equipment.SetFocusItem(new Vector2Int(1, 0));
        }
    }

    private void OnDestroy()
    {
        ItemsQueue.Dispose();
        Equipment.Dispose();
    }

    private void ItemSwapStart(Vector2Int startIndex, Vector2Int endIndex)
    {
        ItemRemove(startIndex,true);
        ItemRemove(endIndex, true);
    }

    private void ItemSwapEnd(Vector2Int startIndex, Vector2Int endIndex)
    {
        ItemAdded(startIndex);
        ItemAdded(endIndex);
    }

    private void ItemAdded(Vector2Int itemIndex)
    {
        var slot = Equipment.GetSlot(itemIndex);
        var item = slot.item;
        if (item == null)
            return;

        switch (item)
        {
            case Weapon weapon:
                {
                    var neighbours = Equipment.GetNeighbourIndexes(itemIndex);
                    foreach (var neighbour in neighbours)
                    {
                        var sideSlot = Equipment.GetSlot(neighbour);
                        if (sideSlot.item is Booster booster)
                            weapon.Ability.AddBooster(booster);
                    }
                    weapon.Ability.Activate();
                    break;
                }
            case Booster booster:
                {
                    var neighbours = Equipment.GetNeighbourIndexes(itemIndex);
                    foreach(var neighbour in neighbours)
                    {
                        var sideSlot = Equipment.GetSlot(neighbour);
                        if (sideSlot.item is Weapon weapon)
                            weapon.Ability.AddBooster(booster);
                    }
                    break;
                }
        }
    }

    private void ItemRemove(Vector2Int itemIndex, bool fullClear)
    {
        var slot = Equipment.GetSlot(itemIndex);
        var item = slot.item;
        slot.item = null;
        if (item == null)
            return;

        switch (item)
        {
            case Weapon weapon:
                {
                    weapon.Ability.RemoveAllBoosters();
                    if (fullClear)
                        weapon.Ability.Stop();
                    break;
                }
            case Booster booster:
                {
                    var neighbours = Equipment.GetNeighbourIndexes(itemIndex);
                    foreach (var neighbour in neighbours)
                    {
                        var sideSlot = Equipment.GetSlot(neighbour);
                        if (sideSlot.item is Weapon weapon)
                            weapon.Ability.RemoveBooster(booster);
                    }
                    break;
                }
        }
    }
}
