using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Buttons : MonoBehaviour {


    public void reset()
    {
        Debug.Log("resetgame");
        SceneManager.LoadScene(0);
    }
}
