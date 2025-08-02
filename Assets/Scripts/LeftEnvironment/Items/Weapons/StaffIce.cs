using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StaffIce : Weapon<StaffIceAbility>
{
}

[System.Serializable]
public class StaffIceAbility : Ability
{
    public override void Activate()
    {
    }

    public override void Stop()
    {
    }
}