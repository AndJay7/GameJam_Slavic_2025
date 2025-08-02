using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Survivor
{
    public class Spawner : MonoBehaviour
    {
        private int wavesize = 10;
        public float waveinterval = 0f;
        private int[] bigwaves = new int[] { 30, 40, 100 };
        private int[] wavesizes = new int[] { 10, 5, 20 };
        private int[] intervals = new int[] { 4, 1, 8 };

        [SerializeField] GameObject Enemy1;
        [SerializeField] GameObject Enemy2;
        [SerializeField] GameObject Enemy3;


        // Start is called before the first frame update
        private Vector3 location;
        private float angle;


        

        private int iteration = 0;

        private int enemycount;

        private GameObject currentEnemy;


        void Awake()
        {
            currentEnemy = Enemy1;
            enemycount = bigwaves[0];
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (waveinterval > 0)
            {
                waveinterval -= Time.fixedDeltaTime;

            }
            else
            {



                StartCoroutine(Burst(wavesize, currentEnemy));

                waveinterval = intervals[iteration];

                enemycount -= wavesize;

                if (enemycount <= 0)
                {
                    iteration++;
                    ////////////////////////////////////////////////
                    if (iteration == 3)
                    {
                        Destroy(this);
                    }
                    else
                    {
                        enemycount = bigwaves[iteration];
                        wavesize = wavesizes[iteration];

                    }
                    /////////////////////////////////////////////////

                    


                    if(iteration == 1)
                    {
                        currentEnemy = Enemy2;
                    }
                    else
                    {
                        if (iteration == 2)
                        {
                            currentEnemy = Enemy3;
                        }
                        
                           
                        
                    }
                    










                }
                
            }
        }


        private IEnumerator Burst(float wavesize, GameObject enemy)
        {
            angle = Random.Range(0f, 360f);

            location = new Vector2(Mathf.Sin(angle) * 16f, Mathf.Cos(angle) * 16f) + PlayerMovement.Instance.Playerlocation;



            while (wavesize > 0)
            {
                Vector3 offset = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f),0);


                Instantiate(enemy, location+offset, Quaternion.identity);
                wavesize--;
                yield return null;
            }



        }

    }
}
