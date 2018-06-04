
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public void Start()
    {
        actions = GetComponent<Actions>();
        states = GetComponent<States>();

        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        tm = GameObject.Find("GlobalTimer").GetComponent<TimeManager>();

        InitialPosition = transform.position;
        InitialRotation = transform.rotation;

        gm.Player = gameObject;
    }

    float PrevX = 0.0f;
    float PrevY = 0.0f;
    float DeadZone = 0.1f;

    public void Update()
    {
        if (gm.CanMove == false)
        {
            states.MovingDown = false;
            states.MovingUp = false;
            states.MovingLeft = false;
            states.MovingRight = false;
            return;
        }

        //Appeller les fonctions qui vont bien
        if (Input.GetAxis("Horizontal") > DeadZone && PrevX < DeadZone)
            MoveRight(true);
        else if (Input.GetAxis("Horizontal") < DeadZone && PrevX > DeadZone)
            MoveRight(false);
        if (Input.GetAxis("Horizontal") < (-1 * DeadZone) && PrevX > (-1 * DeadZone))
            MoveLeft(true);
        else if (Input.GetAxis("Horizontal") > (-1 * DeadZone) && PrevX < (-1 * DeadZone))
            MoveLeft(false);

        if (Input.GetAxis("Vertical") > DeadZone && PrevY < DeadZone)
            MoveUp(true);
        else if (Input.GetAxis("Vertical") < DeadZone && PrevY > DeadZone)
            MoveUp(false);
        if (Input.GetAxis("Vertical") < (-1 * DeadZone) && PrevY > (-1 * DeadZone))
            MoveDown(true);
        else if (Input.GetAxis("Vertical") > (-1 * DeadZone) && PrevY < (-1 * DeadZone))
            MoveDown(false);

        if (Input.GetButtonDown("Fire1"))
            Attack(true);
        else if (!Input.GetButtonUp("Fire1"))
            Attack(false);
        if (Input.GetButtonDown("Fire2"))
            Dash(true);
        else if (!Input.GetButtonUp("Fire2"))
            Dash(false);
        if (Input.GetButtonDown("Fire3"))
            Jump(true);
        else if (!Input.GetButtonUp("Fire3"))
            Jump(false);
        PrevX = Input.GetAxis("Horizontal");
        PrevY = Input.GetAxis("Vertical");

        if (Input.GetButtonDown("Cancel"))
        {
            EndRound();
        }
    }

    bool MoveLeft(bool up)
    {
        states.MovingLeft = up;

        if (gm.Recording)
        {
            Scripter s = new Scripter();

            s.Action = Actions.e_Actions.MoveLeft;
            s.Time = tm.UpdateTimer;
            s.Up = up;
            s.ID = ID;


            gm.OriginalTimeMachine.Add(s);
        }
        return true;
    }

    bool MoveRight(bool up)
    {
        states.MovingRight = up;

        if (gm.Recording)
        {
            Scripter s = new Scripter();

            s.Action = Actions.e_Actions.MoveRight;
            s.Time = tm.UpdateTimer;
            s.Up = up;
            s.ID = ID;


            gm.OriginalTimeMachine.Add(s);
        }
        return true;
    }

    bool MoveUp(bool up)
    {
        states.MovingUp = up;

        if (gm.Recording)
        {
            Scripter s = new Scripter();

            s.Action = Actions.e_Actions.MoveUp;
            s.Time = tm.UpdateTimer;
            s.Up = up;
            s.ID = ID;


            gm.OriginalTimeMachine.Add(s);
        }
        return true;
    }

    bool MoveDown(bool up)
    {
        states.MovingDown = up;

        if (gm.Recording)
        {
            Scripter s = new Scripter();

            s.Action = Actions.e_Actions.MoveDown;
            s.Time = tm.UpdateTimer;
            s.Up = up;
            s.ID = ID;


            gm.OriginalTimeMachine.Add(s);
        }
        return true;
    }

    bool Attack(bool up)
    {
        if (gm.Recording)
        {
            Scripter s = new Scripter();

            s.Action = Actions.e_Actions.Attack;
            s.Time = tm.UpdateTimer;
            s.Up = up;
            s.ID = ID;

            gm.OriginalTimeMachine.Add(s);
        }
        return true;
    }

    bool Dash(bool up)
    {
        if (up == false)
            return true;
        states.Dashing = true;

        if (gm.Recording)
        {
            Scripter s = new Scripter();

            s.Action = Actions.e_Actions.Dash;
            s.Time = tm.UpdateTimer;
            s.Up = up;
            s.ID = ID;


            gm.OriginalTimeMachine.Add(s);
        }
        return true;
    }

    bool Jump(bool up)
    {
        if (up == false || states.Grounded == false)
            return true;
        states.Jumping = true;

        if (gm.Recording)
        {
            Scripter s = new Scripter();

            s.Action = Actions.e_Actions.Jump;
            s.Time = tm.UpdateTimer;
            s.Up = up;
            s.ID = ID;


            gm.OriginalTimeMachine.Add(s);
        }
        return true;
    }

    public void EndRound()
    {
        transform.position = InitialPosition;
        transform.rotation = InitialRotation;

        MoveDown(false);
        MoveUp(false);
        MoveLeft(false);
        MoveRight(false);

        Scripter s = new Scripter();

        s.Action = Actions.e_Actions.Death;
        s.Time = tm.UpdateTimer;
        s.Up = true;
        s.ID = ID;


        ++ID;
        gm.OriginalTimeMachine.Add(s);
        gm.End();
    }

    [HideInInspector]
    public int ID = 0;

    [HideInInspector]
    public Vector3 InitialPosition;
    [HideInInspector]
    public Quaternion InitialRotation;

    GameManager gm;
    TimeManager tm;

    Actions actions;
    States states;
}
