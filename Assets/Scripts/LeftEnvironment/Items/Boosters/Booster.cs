using And.Logic.ValueModifiers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Booster : Item
{
    public override ItemType Type => ItemType.Booster;

    [SerializeField]
    private FloatModifier _spawnRateModifier = new FloatModifier();
    [SerializeField]
    private IntModifier _spawnCountModifier = new IntModifier();
    [SerializeField]
    private FloatModifier _strengthModifier = new FloatModifier();
    [SerializeField]
    private FloatModifier _sizeModifier = new FloatModifier();
    [SerializeField]
    private FloatModifier _durationModifier = new FloatModifier();

    public float GetModifiedSpawnRate(float value) => _spawnRateModifier.Evaluate(value);
    public int GetModifiedSpawnCount(int value) => _spawnCountModifier.Evaluate(value);
    public float GetModifiedStrength(float value) => _strengthModifier.Evaluate(value);
    public float GetModifiedSize(float value) => _sizeModifier.Evaluate(value);
    public float GetModifiedDuration(float value) => _durationModifier.Evaluate(value);

    public override Item Clone()
    {
        var newBooster = new Booster();
        newBooster._spawnRateModifier = _spawnRateModifier;
        newBooster._spawnCountModifier = _spawnCountModifier;
        newBooster._strengthModifier = _strengthModifier;
        newBooster._sizeModifier = _sizeModifier;
        newBooster._durationModifier = _durationModifier;
        return newBooster;
    }
}
