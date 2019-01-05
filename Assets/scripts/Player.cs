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
    //public Text scoretxtgo;

    public GameObject Raggy;

    public Button reset;

    public float Timer;
    public int second = 0;

    public bool go = false;

    public virtual void Die()
    {
        if (go == false)
        {
            go = true;
            Destroy(transform.GetChild(0).gameObject);
            Destroy(transform.GetChild(1).gameObject);
            Destroy(transform.GetChild(2).gameObject);
            Destroy(transform.GetChild(3).GetChild(1).gameObject);
            GetComponent<ThirdPersonCharacter>().enabled = false;
            GetComponent<CapsuleCollider>().enabled = false;
            Destroy(GetComponent<Rigidbody>());

            GameObject rag = Instantiate(Raggy, transform.root.transform.position, Quaternion.identity) as GameObject;

            reset.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }

    }

    public void Addscore(float scoreVal)
    {
        score = score + (scoreVal);
        Debug.Log("score:"+score+"!");
        scoretxt.text = "Score:" + score.ToString();
    }


    // Use this for initialization
    void Start () {
        hell = this;
        score = 0;
        //scoretxtgo.text = "Score:" + score.ToString();
        scoretxt.text = "Score:" + score.ToString();
        healthtxt.text = "Health:" + health.ToString();
        scoreup();

        Timer = Time.time;
    }

    void scoreup()
    {
        scoretxt.text = "Score:" + score.ToString();
    }
	
	// Update is called once per frame
	void Update () {
        healthtxt.text = "Health:" + float.Parse(health.ToString("F0"));

        if (Time.time > Timer + 1)
        {
            Timer = Time.time;
            second++;
        }

        if (second >= 2)
        {
            if(health < 100)
            {
                health += Time.deltaTime;
            }
        }
        if (second >= 4)
        {
            if (health < 100)
            {
                health += Time.deltaTime * 10;
            }
        }
        if (second >= 6)
        {
            if (health < 100)
            {
                health += Time.deltaTime * 20;
            }
        }
        if (second >= 8)
        {
            if (health < 100)
            {
                health += Time.deltaTime * 20;
            }
        }
    }

    public virtual void TakeDamage(float dmg)
    {
        second = 0;
        health -= dmg;
        if (health <= 0)
        {
            Die();
        }
    }

}
