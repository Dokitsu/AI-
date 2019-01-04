using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    public List<GameObject> enemies = new List<GameObject>();

    public int ticket;

    // Use this for initialization
    void Start ()
    {
        ticket = 4;
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
    }

    public void ticketreturn()
    {
        ticket += 1;
    }


    public bool canattack()
    {
        if(ticket > 0)
        {
            ticket -= 1;
            return true;
        }
        else
        {
            return false;
        }
    }
}
 