using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item
{
    public abstract ItemType Type {get;}
    [SerializeField]
    private Sprite _icon;
    [SerializeField]
    private string _name;
    [SerializeField]
    private string _description;

    public Sprite Icon => _icon;
    public string Name => _name;
    public string Description => _description;
}
