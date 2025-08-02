using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sword : Weapon<SwordAbility>
{
}

[System.Serializable]
public class SwordAbility : Ability
{
    public override void Activate()
    {
    }

    public override void Stop()
    {
    }
}