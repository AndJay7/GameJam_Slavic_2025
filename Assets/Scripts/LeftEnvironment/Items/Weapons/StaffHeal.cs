using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StaffHeal : Weapon<StaffHealAbility>
{
}

[System.Serializable]
public class StaffHealAbility : Ability
{
    public override void Activate()
    {
    }

    public override void Stop()
    {
    }
}
