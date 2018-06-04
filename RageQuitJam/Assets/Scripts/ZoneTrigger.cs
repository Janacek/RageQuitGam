using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneTrigger : MonoBehaviour
{
    public GameObject Player;
    public GameManager gm;

    void OnTriggerEnter(Collider other)
    {
        Camera.main.GetComponent<Animator>().SetTrigger("EnterTheRing");
        Player.transform.position = new Vector3(0.0f, 0.0f, -3.8f);
        Player.GetComponent<Controller>().InitialPosition = new Vector3(0.0f, 0.0f, -3.8f);
        gm.Recording = true;
        gm.CanMove = false;

        GameObject lifeBar = GameObject.Find("LifeBar");
        lifeBar.GetComponent<Animator>().SetTrigger("EnterTheRing");
    }
}
