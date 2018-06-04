using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttacks : MonoBehaviour
{
    public GameObject BulletLauncher;
    public GameObject BulletInstance;
    GameObject NewBullet;

    private void Start()
    {
        cacAttack = transform.FindChild("CloseAttack").GetComponent<Animator>();
        loadingAnimator = transform.FindChild("LoadingFX").GetComponent<Animator>();
        p = GameObject.Find("Player").GetComponent<Player>();
        tm = GameObject.Find("GlobalTimer").GetComponent<TimeManager>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void FireBullet(float speed)
    {
        NewBullet = GameObject.Instantiate(BulletInstance);
        NewBullet.GetComponent<Transform>().position = BulletLauncher.GetComponentsInChildren<Transform>()[1].position;
        NewBullet.GetComponent<MoveBullet>().DirectionVector = (BulletLauncher.GetComponentsInChildren<Transform>()[1].position - BulletLauncher.GetComponent<Transform>().position) * speed;
        //   BulletLauncher.GetComponentsInChildren<Transform>()[1].position - BulletLauncher.GetComponent<Transform>().position
    }

    public bool canMove = true;
    public bool loading = false;

    IEnumerator cacAttackAnim(bool record)
    {
        canMove = false;
        cacAttack.gameObject.SetActive(true);
        cacAttack.ResetTrigger("CacAttack");
        cacAttack.SetTrigger("CacAttack");

        yield return new WaitForSeconds(0.267f);

        cacAttack.gameObject.SetActive(false);

        if (record)
        {
            Scripter s = new Scripter();
            s.Action = Actions.e_Actions.TeleportAttack;
            s.ID = -1;
            s.Time = tm.UpdateTimer;

            gm.OriginalTimeMachine.Add(s);
        }
        yield return new WaitForSeconds(0.2f);
        // KILL THE PLAYER CREATE THE EVENT
        if (record)
            p.GetComponent<Controller>().EndRound();

        canMove = true;
        yield return null;
    }

    public void CloseAttack(bool record = true)
    {
        if (canMove)
        {
            StartCoroutine("cacAttackAnim", record);
        }
    }

    IEnumerator loadingAnim()
    {
        loading = true;

        loadingAnimator.gameObject.SetActive(true);
        loadingAnimator.ResetTrigger("LoadingFX");
        loadingAnimator.SetTrigger("LoadingFX");

        yield return new WaitForSeconds(0.4f);

        loadingAnimator.gameObject.SetActive(false);

        loading = false;
        yield return null;
    }

    public void Load()
    {
        if (!loading)
        {
            StartCoroutine("loadingAnim");
        }
    }

    Animator cacAttack;
    Animator loadingAnimator;
    Player p;
    TimeManager tm;
    GameManager gm;
}
