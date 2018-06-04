using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBullet : MonoBehaviour
{
    public Vector3 DirectionVector = new Vector3();
    // Use this for initialization
    void Start()
    {
        p = GameObject.Find("Player");
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        gm.BulletsToDestroy.Add(gameObject);
        //transform.forward = Vector3.up;
        transform.forward = DirectionVector.normalized;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Wall")
            Destroy(this.gameObject);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += transform.forward * Time.fixedDeltaTime * 3;

        Debug.DrawLine(transform.position, transform.position + transform.up * 10, Color.red);
        Debug.DrawLine(transform.position, transform.position + transform.right * 10, Color.blue);
        Debug.DrawLine(transform.position, transform.position + transform.forward * 10, Color.green);

        if (Vector3.Distance(transform.position, p.transform.position) < 0.5f)
        {
            Debug.Log("Dead");
            p.GetComponent<Controller>().EndRound();
        }
    }

    GameObject p;
    GameManager gm;
}
