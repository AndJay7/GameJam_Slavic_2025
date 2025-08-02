using Cysharp.Threading.Tasks;
using Survivor;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Animations;

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
    [SerializeField]
    private PositionConstraint _effectPrefab;
    [SerializeField] GameObject attack;
    [SerializeField] float cooldown = 2f;
    [SerializeField] float size = 1f;
    [SerializeField] int repeats = 0;
    [SerializeField] float duration = 5f;

    private ConstraintSource constraint;

    public override Ability Clone()
    {
        var ability = new StaffIceAbility();
        ability._effectPrefab = _effectPrefab;
        ability.attack = attack;
        ability.cooldown = cooldown;
        ability.size = size;
        ability.repeats = repeats;
        ability.duration = duration;
        return ability;
    }

    private int rememberdir;

    private int correcteddir;

    private CancellationTokenSource tokenSource;

    public override void Activate()
    {
        tokenSource?.Cancel();
        tokenSource = new CancellationTokenSource();

        constraint = new ConstraintSource();
        constraint.sourceTransform = PlayerMovement.Instance.transform;
        constraint.weight = 1;

        StartLoop(tokenSource.Token).Forget();

    }

    public override void Stop()
    {
        tokenSource?.Cancel();
    }

    private async UniTaskVoid StartLoop(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            int realrepeat = repeats;

            foreach (Booster booster in _boosters)
            {
                realrepeat = booster.GetModifiedSpawnCount(realrepeat);

            }


            int iteration = 0;
            while (iteration <= realrepeat)
            {

                

                float realsize = size;

                float realduration = duration;

                foreach (Booster booster in _boosters)
                {
                    realsize = booster.GetModifiedSize(realsize);
                    realduration = booster.GetModifiedDuration(realduration);


                }


                //GameObject instance = GameObject.Instantiate(attack, PlayerMovement.Instance.Playerlocation + new Vector2(0, 1), Quaternion.identity);



                var effect = GameObject.Instantiate(_effectPrefab);
                effect.AddSource(constraint);

                //instance.transform.localScale = new Vector3(realsize, realsize, 1);

                //Transform colliderTransform = instance.transform.Find("Collider");

                effect.transform.localScale = new Vector3(realsize, realsize, 1);
                Transform colliderTransform = effect.transform.Find("Collider");


                DealSlow dealSlow = colliderTransform.GetComponent<DealSlow>();
                dealSlow.duration = realduration;


                

                iteration++;
                if (realrepeat > 0)
                {
                    await UniTask.Delay(300);
                }
            }

            float realcooldown = cooldown;
            foreach (Booster booster in _boosters)
            {
                realcooldown = booster.GetModifiedSpawnRate(realcooldown);
            }
            await UniTask.Delay((int)(realcooldown * 1000),cancellationToken:cancellationToken);
        }


    }

}