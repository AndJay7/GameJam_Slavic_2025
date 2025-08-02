using System.Collections;
using System.Collections.Generic;
using UnityEngine;



    namespace Survivor
{
    public class MirrorAttack : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
        if (PlayerMovement.Instance.LatestMovementDirection.x > 0)
            {
            transform.localScale = new Vector3(-1, 1, 1);
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
