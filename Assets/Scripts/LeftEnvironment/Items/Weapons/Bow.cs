using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Survivor;
using Cysharp.Threading.Tasks;
using System.Threading;

[System.Serializable]
public class Bow : Weapon<BowAbility>
{
    protected override Item CloneInternal()
    {
        var weapon = new Bow();
        weapon.Ability = Ability.Clone();
        return weapon;
    }
}

[System.Serializable]
public class BowAbility : Ability
{
    [SerializeField] GameObject attack;
    [SerializeField] float cooldown = 2f;
    [SerializeField] float damage = 50f;
    [SerializeField] float size = 1f;
    [SerializeField] int repeats = 0;

    private int layerMask;

    private Quaternion extrarotation;
    private Vector2 direction = Vector2.left;

    private int iter;

    public override Ability Clone()
    {
        var ability = new BowAbility();
        ability.attack = attack;
        ability.cooldown = cooldown;
        ability.damage = damage;
        ability.size = size;
        ability.repeats = repeats;
        return ability;
    }

    private CancellationTokenSource tokenSource;

    public override void Activate()
    {
        layerMask = LayerMask.GetMask("Dupa", "RightPlayer");
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
            Vector2 direction = Vector2.left;
            int angle = 0;
            while (angle < 361)
            {
                var playerPos = PlayerMovement.Instance.Playerlocation;
                var shortestDist = new Vector2(Mathf.Infinity, Mathf.Infinity);
                Collider2D closestCollider = null;

                Collider2D[] colliders = Physics2D.OverlapCircleAll(playerPos, 30f, layerMask);

                foreach (var collider in colliders)
                {
                    var dist = (playerPos - (Vector2)collider.transform.position);
                    if (shortestDist.sqrMagnitude > dist.sqrMagnitude)
                    {
                        shortestDist = dist;
                        closestCollider = collider;
                    }
                }

                if (closestCollider != null)
                {
                    direction = shortestDist.normalized;
                }

                angle += 15;
            }
            //Debug.Log($"Closest hit at distance {shortestDistance} in direction {shortestDirection}");

            Quaternion rotation = Quaternion.FromToRotation(Vector2.right, direction);


            int realrepeat = repeats;

            foreach (Booster booster in _boosters)
            {

                realrepeat = booster.GetModifiedSpawnCount(realrepeat);

            }



            if (realrepeat == 0)
            {
                float realsize = size;
                float realdamage = damage;


                foreach (Booster booster in _boosters)
                {
                    realsize = booster.GetModifiedSize(realsize);
                    realdamage = booster.GetModifiedStrength(realdamage);


                }

                GameObject instance = Object.Instantiate(attack, PlayerMovement.Instance.Playerlocation + new Vector2(0, 1), rotation);

                instance.transform.localScale = new Vector3(realsize, realsize, 1);

                Transform colliderTransform = instance.transform.Find("Collider");
                DealDamage dealDamage = colliderTransform.GetComponent<DealDamage>();
                dealDamage.damage = realdamage;

            }
            else
            {
                iter = 0;
                int maxangle = realrepeat * 10;



                float extraangle = Vector2.SignedAngle(Vector2.right, direction);
                extraangle -= maxangle;

                //extrarotation = Quaternion.Euler(0f, 0f, extraangle);





                while (iter <= realrepeat)
                {
                    Quaternion extrarotation = Quaternion.Euler(0f, 0f, extraangle);

                    float realsize = size;
                    float realdamage = damage;


                    foreach (Booster booster in _boosters)
                    {
                        realsize = booster.GetModifiedSize(realsize);
                        realdamage = booster.GetModifiedStrength(realdamage);


                    }


                    GameObject instance = Object.Instantiate(attack, PlayerMovement.Instance.Playerlocation + new Vector2(0, 1), extrarotation);

                    instance.transform.localScale = new Vector3(realsize, realsize, 1);

                    Transform colliderTransform = instance.transform.Find("Collider");
                    DealDamage dealDamage = colliderTransform.GetComponent<DealDamage>();
                    dealDamage.damage = realdamage;
                    extraangle += (float)maxangle / (realrepeat);

                    iter++;



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
