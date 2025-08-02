using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Survivor;
using Cysharp.Threading.Tasks;



[System.Serializable]
public class Sword : Weapon<SwordAbility>
{
}




[System.Serializable]
public class SwordAbility : Ability
{
    [SerializeField] GameObject attack;
    [SerializeField] float cooldown = 2f;
    [SerializeField] float damage = 50f;
    [SerializeField] float size = 1f;
    [SerializeField] int repeats = 0;

    private int rememberdir;

    private int correcteddir;

    private bool isActive = false;


    public override void Activate()
    {
        //throw new System.NotImplementedException();
        //Debug.Log("A");
        //Debug.Log(attack);
        //Instantiate(attack);

        isActive = true;
        StartLoop().Forget();

    }

    public override void Stop()
    {
        //throw new System.NotImplementedException();
        isActive = false;
    }
    private async UniTaskVoid StartLoop()
    {
        Debug.Log("BowAbility activated!");

        while (isActive)
        {
            int iteration = 0;
            while (iteration <= repeats)
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


                GameObject instance = Object.Instantiate(attack, PlayerMovement.Instance.Playerlocation + new Vector2(correcteddir * 2 + iteration * 2 * correcteddir, 1), Quaternion.identity);

                instance.transform.localScale = new Vector3(size, size, 1);

                Transform colliderTransform = instance.transform.Find("Collider");
                DealDamage dealDamage = colliderTransform.GetComponent<DealDamage>();
                dealDamage.damage = damage;


                rememberdir = correcteddir;

                iteration++;
                if (repeats > 0)
                {
                    await UniTask.Delay(100);
                }
            }



            await UniTask.Delay((int)(cooldown * 1000));
        }


    }

}







