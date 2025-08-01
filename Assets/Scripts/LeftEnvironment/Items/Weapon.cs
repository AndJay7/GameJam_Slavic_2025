using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class Weapon<AbilityType> : Item where AbilityType: Ability
{
    public override ItemType Type => ItemType.Weapon;
    public Ability Ability => _ability;

    [SerializeField]
    private AbilityType _ability;

}
