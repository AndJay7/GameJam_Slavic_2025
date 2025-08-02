using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Survivor;
using Cysharp.Threading.Tasks;



[System.Serializable]
public class Bow : Weapon<BowAbility>
{
}




[System.Serializable]
public class BowAbility : Ability
{
    [SerializeField] GameObject attack;
    [SerializeField] float cooldown = 2f;
    [SerializeField] float damage = 50f;
    [SerializeField] float size = 1f;
    [SerializeField] int repeats = 0;

    private int layerMask = 1 << 8;

    private bool isActive = false;

    private Quaternion extrarotation;

    private int iter;

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


        while (isActive)
        {
            float shortestDistance = Mathf.Infinity;
            Vector2 shortestDirection = Vector2.zero;
            int angle = 0;
            while (angle < 361)
            {
                Vector2 direction = new Vector2(Mathf.Sin(angle * Mathf.PI / 180), Mathf.Cos(angle * Mathf.PI / 180));
                RaycastHit2D hit = Physics2D.Raycast(PlayerMovement.Instance.Playerlocation, direction, 30f, layerMask);

                if (hit.collider != null)
                {
                    if (hit.distance < shortestDistance)
                    {
                        shortestDistance = hit.distance;
                        shortestDirection = direction;
                    }
                }


                angle += 15;
            }
            //Debug.Log($"Closest hit at distance {shortestDistance} in direction {shortestDirection}");

            Quaternion rotation = Quaternion.FromToRotation(Vector2.right, shortestDirection);
            if (repeats == 0)
            {
                GameObject instance = Object.Instantiate(attack, PlayerMovement.Instance.Playerlocation + new Vector2(0, 1), rotation);

                instance.transform.localScale = new Vector3(size, size, 1);

                Transform colliderTransform = instance.transform.Find("Collider");
                DealDamage dealDamage = colliderTransform.GetComponent<DealDamage>();
                dealDamage.damage = damage;

            }
            else
            {
                iter = 0;
                int maxangle = repeats * 10;



                float extraangle = Vector2.SignedAngle(Vector2.right, shortestDirection);
                extraangle -= maxangle;

                //extrarotation = Quaternion.Euler(0f, 0f, extraangle);





                while (iter <= repeats)
                {
                    Quaternion extrarotation = Quaternion.Euler(0f, 0f, extraangle);

                    GameObject instance = Object.Instantiate(attack, PlayerMovement.Instance.Playerlocation + new Vector2(0, 1), extrarotation);

                    instance.transform.localScale = new Vector3(size, size, 1);

                    Transform colliderTransform = instance.transform.Find("Collider");
                    DealDamage dealDamage = colliderTransform.GetComponent<DealDamage>();
                    dealDamage.damage = damage;
                    extraangle += maxangle / (repeats);

                    iter++;



                }



            }





            await UniTask.Delay((int)(cooldown * 1000));
        }


    }

}
