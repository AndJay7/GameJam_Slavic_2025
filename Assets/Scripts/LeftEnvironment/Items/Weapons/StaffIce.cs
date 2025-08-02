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

    private CancellationTokenSource tokenSource;
    private ConstraintSource constraint;







    //<<<<<<< Updated upstream
    public override Ability Clone()
    {
        throw new System.NotImplementedException();
    }
//=======
    [SerializeField] GameObject attack;
    [SerializeField] float cooldown = 2f;
    
    [SerializeField] float size = 1f;
    [SerializeField] int repeats = 0;

    private int rememberdir;

    private int correcteddir;

    private bool isActive = false;

    [SerializeField] float duration = 5f;
//>>>>>>> Stashed changes

    public override void Activate()
    {

        
        constraint = new ConstraintSource();
        constraint.sourceTransform = PlayerMovement.Instance.transform;
        constraint.weight = 1;



        isActive = true;
        StartLoop().Forget();

    }

    public override void Stop()
    {

        isActive = false;
    }
    private async UniTaskVoid StartLoop()
    {





        while (isActive)
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
            await UniTask.Delay((int)(realcooldown * 1000));
        }


    }

}