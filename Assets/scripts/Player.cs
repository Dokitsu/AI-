using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Player : MonoBehaviour {

    public static Player hell;
    public double health;
    static float score;
    public Text scoretxt;
    public Text healthtxt;
    public Text scoretxtgo;
    public Text wavetxtgo;
    static float wave;

    public virtual void TakeDamage(double dmg)
    {
        health -= dmg;
        healthtxt.text = "Health:" + float.Parse(health.ToString());
        if (health <= 0)
        {
            //wave = WAves.curwave;
            Die();
        }
    }

    public virtual void Die()
    {
        //Destroy(gameObject);
        Debug.Log("player dead");
        SceneManager.LoadScene("MenuGO");
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
        Debug.Log(health);
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
        healthtxt.text = "Health:" + float.Parse(health.ToString());

    }

    void OnCollisionStay(Collision bot)
    {
        if (bot.gameObject.tag == "Enemy")
        {
            TakeDamage(0.1);
            Debug.Log(health);
        }

        if (bot.gameObject.tag == "projectile")
        {
            TakeDamage(10);
            Debug.Log(health);
        }
        if (bot.gameObject.tag == "moprojectile")
        {
            TakeDamage(1);
            Debug.Log(health);
        }

    }
}
