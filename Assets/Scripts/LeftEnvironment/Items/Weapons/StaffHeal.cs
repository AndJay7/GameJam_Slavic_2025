using Cysharp.Threading.Tasks;
using Survivor;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Animations;

[System.Serializable]
public class StaffHeal : Weapon<StaffHealAbility>
{
}

[System.Serializable]
public class StaffHealAbility : Ability
{
    [SerializeField]
    private int _healValue;
    [SerializeField]
    private float _cooldown;
    [SerializeField]
    private PositionConstraint _effectPrefab;

    private CancellationTokenSource tokenSource;
    private ConstraintSource constraint;

    public override void Activate()
    {
        tokenSource = new CancellationTokenSource();
        constraint = new ConstraintSource();
        constraint.sourceTransform = PlayerMovement.Instance.transform;
        constraint.weight = 1;
        HealingAsync(tokenSource.Token).Forget();
    }

    private async UniTask HealingAsync(CancellationToken cancellation)
    {
        while(!cancellation.IsCancellationRequested)
        {
            var cooldown = _cooldown;
            var healValue = _healValue;

            foreach (var booster in _boosters)
            {
                cooldown = booster.GetModifiedSpawnRate(cooldown);
            }

            await UniTask.WaitForSeconds(cooldown);

            foreach (var booster in _boosters)
            {
                healValue = Mathf.FloorToInt(booster.GetModifiedStrength(healValue));
            }

            PlayerMovement.Instance.Health.AddHealth(healValue);
            var effect = GameObject.Instantiate(_effectPrefab);
            effect.AddSource(constraint);
        }
    }

    public override void Stop()
    {
        tokenSource.Cancel();
    }
}
