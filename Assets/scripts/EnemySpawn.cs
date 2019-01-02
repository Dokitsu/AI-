using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{

    public GameObject enemy;
    public static int enemycount;
    public int count;

    // Use this for initialization
    void Start()
    {
        enemycount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        Spawnenemy();
    }

    // Update is called once per frame
    void Update()
    {
        count = enemycount;
    }

    void Spawnenemy()
    {
        Instantiate(enemy, transform.position, transform.rotation);
    }
}
