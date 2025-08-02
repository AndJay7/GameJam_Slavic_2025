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
    [SerializeField, Multiline]
    private string _description;
    [SerializeField]
    private float _weight;

    public Sprite Icon => _icon;
    public string Name => _name;
    public string Description => _description;
    public float Weight => _weight;
}
