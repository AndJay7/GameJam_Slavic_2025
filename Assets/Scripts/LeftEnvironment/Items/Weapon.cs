using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class Weapon : Item
{
    public abstract Ability Ability { get; }
}

[Serializable]
public abstract class Weapon<AbilityType> : Weapon where AbilityType: Ability
{
    public override ItemType Type => ItemType.Weapon;
    public override Ability Ability => _ability;

    [SerializeField]
    private AbilityType _ability;

}
