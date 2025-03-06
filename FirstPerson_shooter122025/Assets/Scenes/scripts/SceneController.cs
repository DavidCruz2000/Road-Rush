using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    // What prefab to spawn
    [SerializeField] GameObject enemyPrefab;

    [SerializeField] GameObject h_packPrefab;

    private GameObject h_pack;


    // Private field to track a single instance of the enemy
    private GameObject enemy;
                float[] EnemyLocationX = {-5, 4, -22};
                float[] EnemyLocationZ = {1, -6, 16};


                float[] packLocationX = {-22,-11};
                float[] packLocationZ = {16,-27};

    // Start is called before the first frame update
    void Start()
    {
        for(int i =0; i<10; i++)
        {
            
        }
    }
 
    // Update is called once per frame
    
    void Update()
    {
        // If there isn't an enemy, spawn one
        if (enemy == null) 
        {
            //if you want more Enemies or locations
            for(int i =0; i<3; i++)
            {

                enemy = Instantiate(enemyPrefab) as GameObject;
                enemy.transform.position = new Vector3(EnemyLocationX[i], 1, EnemyLocationZ[i]);
                            //you want to put EnemyLocation here ^
                float angle = Random.Range(0, 360);
                enemy.transform.Rotate(0, angle, 0);
            }
        }

        //if(h_pack == null)
        //{
            //for(int j =0; j<2 && h_pack == null; j++){
        //    h_pack = Instantiate(h_packPrefab) as GameObject;
        //    float track = Random.Range(0,1);
        //    h_pack.transform.position = new Vector3(-6, 1, 1);    
        //}

    }
}
