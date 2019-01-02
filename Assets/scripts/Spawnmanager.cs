using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnmanager : MonoBehaviour {

    public GameObject[] spawnpoints;
    public static int enemycount;
    public int count;

    public int point;

    // Use this for initialization
    void Start ()
    {
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        enemycount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        count = enemycount;

        if (count <= 4)
        {
            point = Random.Range(0, 7);
            spawnpoints[point].GetComponent<EnemySpawn>().Spawnenemy();
            spawnpoints[point].GetComponent<EnemySpawn>().Spawnenemy();
            spawnpoints[point].GetComponent<EnemySpawn>().Spawnenemy();
            spawnpoints[point].GetComponent<EnemySpawn>().Spawnenemy();
        }
    }

}
