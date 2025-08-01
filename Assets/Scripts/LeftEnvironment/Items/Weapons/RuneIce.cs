using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RuneIce : Weapon<RuneIceAbility>
{
}

[System.Serializable]
public class RuneIceAbility : Ability
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