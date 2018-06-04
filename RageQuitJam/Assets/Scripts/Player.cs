using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Speed = 20.0f;
    public float DashPub = 15.5f;
    private float DashValue = 0.0f;
    public GameObject Blood;
    GameObject BloodInstance = null;

    public GameObject SmokeDash;

    public void Start()
    {
        states = GetComponent<States>();
        boss = GameObject.Find("Boss").gameObject;
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        tm = GameObject.Find("GlobalTimer").GetComponent<TimeManager>();
    }

    public void Reset()
    {
        states = GetComponent<States>();
        states.Dead = false;
        Destroy(BloodInstance);
        BloodInstance = null;
        transform.GetChild(0).gameObject.SetActive(true);
    }

    Vector3 offset;
    bool instantiatedash = true;

    public void FixedUpdate()
    {
        if (states.Dead)
        {
            if (BloodInstance)
            {
                return;
            }   
            BloodInstance = GameObject.Instantiate(Blood);
            BloodInstance.transform.position = transform.position;
            BloodInstance.transform.Rotate(0, Random.Range(0, 360), 0);
            transform.GetChild(0).gameObject.SetActive(false);
        }
        if (BloodInstance)
        {
            return;
        }

        offset = new Vector3(0, upVelocity, 0);
        if (states.MovingDown)
        {
            offset.z -= (Speed + DashValue) * Time.fixedDeltaTime;
        }

        if (states.MovingUp)
        {
            offset.z += (Speed + DashValue) * Time.fixedDeltaTime;
        }

        if (states.MovingLeft)
        {
            offset.x -= (Speed + DashValue) * Time.fixedDeltaTime;

            side = 2;
            flip = false;
        }

        if (states.MovingRight)
        {
            offset.x += (Speed + DashValue) * Time.fixedDeltaTime;
            side = 2;
            flip = true;
        }

        #region Idle

        if (states.MovingDown && (!states.MovingLeft && !states.MovingRight))
        {
            side = states.Dashing ? 5 : 10;
            flip = false;
        }
        else if (states.MovingDown && states.MovingLeft)
        {
            side = states.Dashing ? 8 : 13;
            flip = false;
        }
        else if (states.MovingDown && states.MovingRight)
        {
            side = states.Dashing ? 8 : 13;
            flip = true;
        }
        else if (states.MovingUp && (!states.MovingLeft && !states.MovingRight))
        {
            side = states.Dashing ? 6 : 11;
            flip = false;
        }
        else if (states.MovingUp && states.MovingLeft)
        {
            side = states.Dashing ? 9 : 14;
            flip = false;
        }
        else if (states.MovingUp && states.MovingRight)
        {
            side = states.Dashing ? 9 : 14;
            flip = true;
        }
        else if (states.MovingLeft)
        {
            side = states.Dashing ? 7 : 12;
            flip = false;
        }
        else if (states.MovingRight)
        {
            side = states.Dashing ? 7 : 12;
            flip = true;
        }
        else
        {
            if (side == 5 || side == 10)
                side = 0;
            else if (side == 8 || side == 13)
                side = 3;
            else if (side == 6 || side == 11)
                side = 1;
            else if (side == 4 || side == 14)
                side = 4;
            else if (side == 7 || side == 12)
                side = 2;
        }

        #endregion

        #region Dash



        #endregion

        if (states.Jumping)
        {
            //states.Jumping = false;
            //upVelocity = 5;
            upVelocity += 9.81f * 0.015f;
            if (upVelocity >= 1.5f)
                states.Jumping = false;
        }
        else
        {
            upVelocity -= 9.81f * 0.005f;
        }

        if (states.Dashing)
        {
            if (Vector3.Distance(transform.position, boss.transform.position) < 1)
            {
                if (GetComponent<Controller>() && instantiatedash)
                {
                    --boss.GetComponent<BossLife>().Life;
                    Scripter s = new Scripter();
                    s.Action = Actions.e_Actions.Hit;
                    s.ID = -1;
                    s.Time = tm.UpdateTimer;
                    gm.OriginalTimeMachine.Add(s);
                }
            }

            if (instantiatedash)
            {
                GameObject o = GameObject.Instantiate(SmokeDash);
                o.transform.position = transform.position;
                instantiatedash = false;
            }
            DashValue += 10.0f * 0.15f;
            if (DashValue >= DashPub)
            {
                states.Dashing = false;
                instantiatedash = true;
            }
        }
        else
        {
            DashValue -= 10.0f * 0.3f;
            if (DashValue <= 0)
                DashValue = 0;
        }

        if (upVelocity <= 0)
        {
            upVelocity = 0;
            states.Grounded = true;
        }
        else
        {
            states.Grounded = false;
        }

        transform.position += offset;
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        transform.GetChild(0).transform.localPosition = new Vector3(0, 0, upVelocity);
    }

    [HideInInspector]
    public int side = 1;
    [HideInInspector]
    public bool flip = false;

    GameObject boss;

    float upVelocity = 0.0f;
    States states;
    GameManager gm;
    TimeManager tm;
}
