using System.Collections;
using System.Collections.Generic;
using UnityEngine;



    namespace Survivor
{
    public class MirrorAttack : MonoBehaviour
    {
        private int rand;
        // Start is called before the first frame update
        void Start()
        {
        if (PlayerMovement.Instance.LatestMovementDirection.x > 0)
            {
                //transform.localScale = new Vector3(-1, 1, 1);
                Vector3 scale = transform.localScale;
                scale.x *= -1;
                transform.localScale = scale;

            }
            else
            {
                if(PlayerMovement.Instance.LatestMovementDirection.x == 0)
                {
                    rand = Random.Range(0, 2);
                    
                    rand = 1 - 2 * rand;

                    //transform.localScale = new Vector3(rand, 1, 1);
                    Vector3 scale = transform.localScale;
                    scale.x *= rand;
                    transform.localScale = scale;

                }
            }
        }
        

        public void DestroyMe() { Destroy(gameObject); }


        // Update is called once per frame
        /*
        void Update()
            {

                {
                    if (PlayerMovement.Instance.LatestMovementDirection.x > 0)
                    {

                        transform.localScale = new Vector3(-1, 1, 1);
                    }
                    if (PlayerMovement.Instance.LatestMovementDirection.x < 0)
                    {
                        transform.localScale = new Vector3(1, 1, 1);

                    }
                }


            }
     */
    }
}
