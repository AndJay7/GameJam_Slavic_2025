using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RuneHeal : Weapon<RuneHealAbility>
{
}

[System.Serializable]
public class RuneHealAbility : Ability
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
