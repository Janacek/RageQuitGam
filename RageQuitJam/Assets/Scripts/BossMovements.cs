using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovements : MonoBehaviour
{

    public List<GameObject> TPCluster;
    public int ActualIndex = -1;
    public float MoveIn = 0f;

    // Use this for initialization

    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        tm = GameObject.Find("GlobalTimer").GetComponent<TimeManager>();
        //TPAtIndex();
        player = GameObject.Find("Player");
    }

    public void Restart()
    {
        //transform.position = TPCluster[4].GetComponent<Transform>().position;
        TPAtIndex(4);
        transform.rotation = new Quaternion();
        MoveIn = 1f;
        GetComponent<BossAttacks>().BulletLauncher.transform.rotation = Quaternion.identity;
        for (int i = 0; i < 5; ++i)
        {
            TPCluster[i].gameObject.SetActive(false);
        }

        transform.FindChild("Head").GetComponent<Animator>().ResetTrigger("HeadON");
        transform.FindChild("Head").GetComponent<Animator>().ResetTrigger("HeadOFF");
        transform.FindChild("Head").GetComponent<Animator>().SetTrigger("HeadON");
        StopCoroutine("tpanim");
        _continue = true;
        timerClose = 0.0f;

        GetComponent<BossAttacks>().canMove = true;
    }

    bool first = true;

    float timerClose = 0.0f;

    // Update is called once per frame
    void Update()
    {
        if (gm.CanMove == false || gm.Recording == false)
        {
            return;
        }

        if (first)
        {
            first = false;
            transform.FindChild("Head").GetComponent<Animator>().SetTrigger("HeadON");
        }

        if (_continue == false ||
            GetComponent<BossAttacks>().canMove == false)
            return;

        float dist = Vector3.Distance(player.transform.position, transform.position);

        if (dist < 1)
        {
            timerClose += Time.deltaTime;
        }
        else
        {
            timerClose = 0.0f;
        }

        if (timerClose >= 1.0f && 
            dist < 2)
        {
            timerClose = 0.0f;
            GetComponent<BossAttacks>().CloseAttack(true);
        }

        if (MoveIn < 0f)
        {
            TPAtIndex();
        }
        MoveIn -= Time.deltaTime;
    }

    public void TPAtIndex()
    {
        if (ActualIndex != -1)
            TPCluster[ActualIndex].SetActive(false);
        ActualIndex = Random.Range(0, TPCluster.Count);

        //  ActualIndex = 0;
        StartCoroutine("tpanim");
    }

    void EnableTP(bool active)
    {
        transform.FindChild("TPAnim").gameObject.SetActive(active);
        transform.FindChild("Head").gameObject.SetActive(!active);
        transform.FindChild("Body").gameObject.SetActive(!active);

        //if (active)
        //{
            //transform.FindChild("TPAnim").GetComponent<Animator>().SetTrigger("AnimTPON");
        //}
    }

    IEnumerator tpanim()
    {
        _continue = false;

        Animator head = transform.FindChild("Head").GetComponent<Animator>();
        Animator a = transform.FindChild("TPAnim").GetComponent<Animator>();

        head.ResetTrigger("HeadON");
        head.SetTrigger("HeadOFF");

        yield return new WaitForSeconds(0.667f);

        EnableTP(true);
        a.ResetTrigger("AnimTPON");
        a.SetTrigger("AnimTPOFF");

        yield return new WaitForSeconds(0.567f);

        GetComponent<Transform>().position = TPCluster[ActualIndex].GetComponent<Transform>().position;
        a.ResetTrigger("AnimTPOFF");
        a.SetTrigger("AnimTPON");

        yield return new WaitForSeconds(0.567f);

        TPCluster[ActualIndex].SetActive(true);
        EnableTP(false);
        head.ResetTrigger("HeadOFF");
        head.SetTrigger("HeadON");

        _continue = true;

        yield return null;
    }

    bool _continue = true;

    public void TPAtIndex(int id)
    {
        //  ActualIndex = 0;
        GetComponent<Transform>().position = TPCluster[id].GetComponent<Transform>().position;
        TPCluster[id].SetActive(true);
    }

    GameManager gm;
    TimeManager tm;
    GameObject player;
}
