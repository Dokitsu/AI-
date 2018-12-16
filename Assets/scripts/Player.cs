using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Player : MonoBehaviour {

    public static Player hell;
    public double health;
    static float score;
    public Text scoretxt;
    public Text healthtxt;
    public Text scoretxtgo;
    public Text wavetxtgo;
    static float wave;

    public virtual void Die()
    {
        Destroy(gameObject);

        AICharacterControl enemy = GetComponentInChildren<AICharacterControl>();
        if (enemy != null)
        {
            enemy.Ragdeath();
        }

    }

    public void Addscore(float scoreVal)
    {
        float healthmultiplyer = (float)health;
        score = score + (scoreVal * healthmultiplyer);
        Debug.Log("score:"+score+"!");
        scoretxt.text = "Score:" + score.ToString();
    }


    // Use this for initialization
    void Start () {
        hell = this;
        //wave = WAves.curwave;
        scoretxtgo.text = "Score:" + score.ToString();
        wavetxtgo.text = "you got to wave " + wave.ToString()+"/9";
        healthtxt.text = "Health:" + health.ToString();
        scoreup();
    }

    void scoreup()
    {
        scoretxt.text = "Score:" + score.ToString();
    }
	
	// Update is called once per frame
	void Update () {
        //healthtxt.text = "Health:" + float.Parse(health.ToString());

    }

    public virtual void TakeDamage(float dmg)
    {
        health -= dmg;
        if (health <= 0)
        {
            //Die();
        }
    }

}
