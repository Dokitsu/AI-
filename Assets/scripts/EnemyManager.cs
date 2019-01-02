using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    public List<GameObject> enemies = new List<GameObject>();

    public bool attack;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        updateenemies();
    }

    void updateenemies()
    {
        enemies.Clear();
        foreach (GameObject i in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            enemies.Add(i);
        }

        //adding a ticket system to the game
    }

    public bool canattack()
    {
        attack = true;
        return attack;
    }
}
 