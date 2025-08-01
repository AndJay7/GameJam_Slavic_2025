using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Bow : Weapon<BowAbility>
{
}

[System.Serializable]
public class BowAbility : Ability
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