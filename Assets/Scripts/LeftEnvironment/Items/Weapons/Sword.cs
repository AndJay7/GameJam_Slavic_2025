using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Survivor;
using Cysharp.Threading.Tasks;
using System.Threading;

[System.Serializable]
public class Sword : Weapon<SwordAbility>
{
    protected override Item CloneInternal()
    {
        var weapon = new Sword();
        weapon.Ability = Ability.Clone();
        return weapon;
    }
}




[System.Serializable]
public class SwordAbility : Ability
{
    [SerializeField] GameObject attack;
    [SerializeField] float cooldown = 2f;
    [SerializeField] float damage = 50f;
    [SerializeField] float size = 1f;
    [SerializeField] int repeats = 0;

    public override Ability Clone()
    {
        var ability = new SwordAbility();
        ability.attack = attack;
        ability.cooldown = cooldown;
        ability.damage = damage;
        ability.size = size;
        ability.repeats = repeats;
        ability.index = SwordAbility.count++;
        Debug.Log("Clone " + ability.index);

        return ability;
    }

    private int rememberdir;

    private int correcteddir;

    private int right = 1;

    public static int count = 0;

    private int index = 0;

    private CancellationTokenSource tokenSource;

    public override void Activate()
    {
        Debug.Log("Activate " + index);
        tokenSource?.Cancel();
        tokenSource = new CancellationTokenSource();
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

                if (PlayerMovement.Instance.LatestMovementDirection.x > 0)
                {
                    correcteddir = 1;
                }
                if (PlayerMovement.Instance.LatestMovementDirection.x < 0)
                {
                    correcteddir = -1;
                }
                if (PlayerMovement.Instance.LatestMovementDirection.x == 0)
                {
                    if (rememberdir != 0)
                    {
                        correcteddir = rememberdir;
                    }
                    else
                    {
                        correcteddir = 1;
                    }
                }

                float realsize = size;
                float realdamage = damage;
                

                foreach(Booster booster in _boosters)
                {
                    realsize = booster.GetModifiedSize(realsize);
                    realdamage = booster.GetModifiedStrength(realdamage);
                    

                }


                GameObject instance = Object.Instantiate(attack, PlayerMovement.Instance.Playerlocation + new Vector2(right * 2 , 1), Quaternion.identity);

                if(right == 1)
                {
                    right = -1;
                }
                else
                {
                    right = 1;
                }


                instance.transform.localScale = new Vector3(realsize, realsize, 1);

                Transform colliderTransform = instance.transform.Find("Collider");
                DealDamage dealDamage = colliderTransform.GetComponent<DealDamage>();
                dealDamage.damage = realdamage;


                rememberdir = correcteddir;

                iteration++;
                if (realrepeat > 0)
                {
                    await UniTask.Delay(100,cancellationToken: cancellationToken);
                }
            }

            float realcooldown = cooldown;
            foreach (Booster booster in _boosters)
            {
                realcooldown = booster.GetModifiedSpawnRate(realcooldown);
            }
                await UniTask.Delay((int)(realcooldown * 1000), cancellationToken: cancellationToken);
        }


    }

}







