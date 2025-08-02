using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StaffIce : Weapon<StaffIceAbility>
{
    public override Item Clone()
    {
        var weapon = new StaffIce();
        weapon.Ability = Ability.Clone();
        return weapon;
    }
}


[System.Serializable]
public class StaffIceAbility : Ability
{
    public override Ability Clone()
    {
        throw new System.NotImplementedException();
    }

    public override void Activate()
    {
    }

    public override void Stop()
    {
    }
}