using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Survivor
{
    public class TempSpawn : MonoBehaviour
    {
        private float countdown = 1f;

        [SerializeField] GameObject attack;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (countdown > 0f)
            {
                countdown -= Time.fixedDeltaTime;
            }
            else
            {
                Vector3 offset = new Vector3(PlayerMovement.Instance.LatestMovementDirection.x*2, PlayerMovement.Instance.LatestMovementDirection.y * 2+1, 0);
                Instantiate(attack, transform.position+offset, transform.rotation);
                countdown = 1f;
            }

        }
    }
}
