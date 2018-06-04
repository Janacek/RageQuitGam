using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossLife : MonoBehaviour
{
    public int Life = 50;

    public void Start()
    {
        LifeBar = GameObject.Find("LifeBar");
    }

    public void Update()
    {
        GameObject o = LifeBar.transform.GetChild(1).gameObject;

        if (Input.GetKeyDown(KeyCode.O))
        {
            Life -= 5;
        }

        o.GetComponent<Image>().fillAmount = (float)Life / (float)50;
        if (Life <= 0)
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().GameOver();
        }
    }

    GameObject LifeBar;
}
