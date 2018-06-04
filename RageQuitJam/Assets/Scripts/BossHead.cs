using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHead : MonoBehaviour
{
    public List<Sprite> HeadSprites;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    void Update()
    {
        Vector3 dir = (player.transform.position - transform.parent.position).normalized;
        float angle = Vector3.Angle(transform.parent.forward, dir);
        Debug.Log(360/8 + "  /  " + angle / (360/8));
        GetComponent<SpriteRenderer>().sprite = HeadSprites[(int)(angle / ((360 / 8) - 20))];
    }

    Player player;
}
