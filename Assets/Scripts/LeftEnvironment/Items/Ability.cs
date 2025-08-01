using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Ability
{
    private readonly List<Booster> _boosters = new List<Booster>();

    public void AddBooster(Booster booster)
    {
        _boosters.Add(booster);
    }

    public abstract void Activate();

    public abstract void Stop();

    public void Clear()
    {
        Stop();
        _boosters.Clear();
    }
}
