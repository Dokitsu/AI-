using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.ThirdPerson;

public class Enemy : MonoBehaviour {

    public float health;
    public float scoreVal = 420;
    private Player player;
    public Color white = Color.white;
    public Color clear = Color.clear;
    public float duration = 0.1f;
    Renderer hit;
    public GameObject boi;


    public virtual void TakeDamage(float dmg)
    {
        health -= dmg;
        StartCoroutine(hittaken());
        if (health <= 0)
        {
            Die();
        }
    }

    IEnumerator hittaken()
    {
        Material mat = this.hit.material;
        Color32 col = this.hit.material.color;
        this.hit.material = null;
        this.hit.material.color = Color.white;
        yield return new WaitForSeconds(0.05f);
        this.hit.material = mat;
        this.hit.material.color = col;
    }

    public virtual void Die() 
        {
            Destroy(gameObject);
            //player.Addscore(scoreVal);

            AICharacterControl enemy = GetComponentInChildren<AICharacterControl>();
            if(enemy != null)
            {
                enemy.Ragdeath();
            }

        }




    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        hit = boi.GetComponent<Renderer>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
