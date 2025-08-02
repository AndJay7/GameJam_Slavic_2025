using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class Weapon : Item
{
    public abstract Ability Ability { get; protected set; }
}

[Serializable]
public abstract class Weapon<AbilityType> : Weapon where AbilityType: Ability
{
    public override ItemType Type => ItemType.Weapon;
    public override Ability Ability
    {
        get => _ability;
        protected set => _ability = value as AbilityType;
    }

    [SerializeField]
    private AbilityType _ability;
}
