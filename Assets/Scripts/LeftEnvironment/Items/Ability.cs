using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Ability
{
    protected readonly List<Booster> _boosters = new List<Booster>();

    public void AddBooster(Booster booster)
    {
        _boosters.Add(booster);
    }

    public abstract Ability Clone();

    public void RemoveBooster(Booster booster)
    {
        _boosters.Remove(booster);
    }

    public abstract void Activate();

    public abstract void Stop();

    public void RemoveAllBoosters()
    {
        _boosters.Clear();
    }
}
