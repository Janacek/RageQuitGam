using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterZone : MonoBehaviour
{
    public GameObject BossRef;

    // Use this for initialization
    void OnEnable()
    {
        BossRef.GetComponent<BossMovements>().MoveIn = 2f;
    }

    bool prev = true;
    float latency = 0.5f;
    // Update is called once per frame
    void Update()
    {
        BossRef.GetComponent<BossAttacks>().BulletLauncher.GetComponent<Transform>().Rotate(new Vector3(0f, 2f * Time.deltaTime * 30, 0f));

        if (latency < 0f)
        {
            if (prev)
            {
                BossRef.GetComponent<BossAttacks>().FireBullet(0.1f);
                prev = !prev;
            }
            else
            {
                BossRef.GetComponent<BossAttacks>().FireBullet(0.05f);
                prev = !prev;
            }
            latency = 0.5f;
        }
        latency -= Time.deltaTime;
    }
}
