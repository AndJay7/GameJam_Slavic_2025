using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemSO : ScriptableObject
{
    public abstract Item Item { get; }
}


public class ItemSO<ItemType> : ItemSO where ItemType : Item
{
    [SerializeField]
    private ItemType _item;

    public override Item Item => _item;
}
