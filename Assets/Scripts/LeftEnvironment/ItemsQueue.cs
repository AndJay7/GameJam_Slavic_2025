using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsQueue
{
    public event Action<Item> OnItemAdded;
    public event Action<Item> OnItemRemoved;

    public Queue<Item> Items { get; } = new Queue<Item>();

    public void Enqueue(Item item)
    {
        var itemInstance = item.Clone();
        Items.Enqueue(item);
        OnItemAdded?.Invoke(item);
    }

    public Item Dequeue()
    {
        var item = Items.Dequeue();
        OnItemRemoved?.Invoke(item);
        return item;
    }

    public bool CanDequeue()
    {
        return Items.Count > 0;
    }
}
