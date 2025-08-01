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
        throw new System.NotImplementedException();
    }

    public override void Stop()
    {
        throw new System.NotImplementedException();
    }
}