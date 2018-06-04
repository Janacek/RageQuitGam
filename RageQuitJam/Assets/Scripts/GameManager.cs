using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public List<Scripter> OriginalTimeMachine = new List<Scripter>();
    [HideInInspector]
    public List<Scripter> TimeMachine = new List<Scripter>();
    [HideInInspector]
    public GameObject Player;
    public GameObject Boss;

    public GameObject Ghost;

    [HideInInspector]
    public bool Recording = false;
    public bool CanMove = false;

    public List<GameObject> BulletsToDestroy = new List<GameObject>();

    public void Start()
    {
        tm = GameObject.Find("GlobalTimer").GetComponent<TimeManager>();

        tm.ResetTimers();
    }

    float timer = 0.0f;

    float timeNeeded = 0.33f;

    float timerNoMove = 1.5f;

    public void Update()
    {
        if (CanMove == false)
        {
            timerNoMove -= Time.deltaTime;
            if (timerNoMove <= 0.0f)
            {
                CanMove = true;
                tm.ResetTimers();
                timerNoMove = 1.5f;
            }

        }

        ghosts.ForEach(g =>
        {
            g.transform.Find("Body").GetComponent<SpriteRenderer>().sortingOrder = (int)getZ(g);
        });
        Player.transform.Find("Body").GetComponent<SpriteRenderer>().sortingOrder = (int)getZ(Player);
        Boss.transform.Find("Body").GetComponent<SpriteRenderer>().sortingOrder = (int)getZ(Boss);
        Boss.transform.Find("Head").GetComponent<SpriteRenderer>().sortingOrder = (int)getZ(Boss) + 2;
        Boss.transform.Find("CloseAttack").GetComponent<SpriteRenderer>().sortingOrder = (int)getZ(Boss) + 1;
    }

    float getZ(GameObject obj)
    {
        if (obj.transform.position.z < -2.0f)
            return 10;
        float Z = (15 - obj.transform.position.z) * 1000;
        Z += 14000;

        return Z;
    }

    public void FixedUpdate()
    {
        timer += Time.deltaTime;

        bool BossHasActions = TimeMachine.Exists(x => x.ID == -1);

        TimeMachine.ForEach(tm =>
        {
            //tm.Time -= Time.deltaTime;

            if (timer >= tm.Time)
            {
                // Execute;
                if (tm.ID >= 0) // PLAYER
                {
                    if (tm.Action == Actions.e_Actions.MoveLeft)
                    {
                        ghosts[tm.ID].GetComponent<States>().MovingLeft = tm.Up;
                    }
                    if (tm.Action == Actions.e_Actions.MoveRight)
                    {
                        ghosts[tm.ID].GetComponent<States>().MovingRight = tm.Up;
                    }
                    if (tm.Action == Actions.e_Actions.MoveDown)
                    {
                        ghosts[tm.ID].GetComponent<States>().MovingDown = tm.Up;
                    }
                    if (tm.Action == Actions.e_Actions.MoveUp)
                    {
                        ghosts[tm.ID].GetComponent<States>().MovingUp = tm.Up;
                    }
                    if (tm.Action == Actions.e_Actions.Jump)
                    {
                        ghosts[tm.ID].GetComponent<States>().Jumping = true;
                    }
                    if (tm.Action == Actions.e_Actions.Dash)
                    {
                        ghosts[tm.ID].GetComponent<States>().Dashing = true;
                    }
                    if (tm.Action == Actions.e_Actions.Death)
                    {
                        ghosts[tm.ID].GetComponent<States>().Dead = true;
                    }
                }
                else // BOSS
                {
                    if (tm.Action == Actions.e_Actions.Hit)
                    {
                        --Boss.GetComponent<BossLife>().Life;
                    }
                    if (tm.Action == Actions.e_Actions.TeleportAttack)
                    {
                        Boss.GetComponent<BossAttacks>().CloseAttack(false);
                    }
                }

                TimeMachine.Remove(tm);
            }
        });

        Boss.GetComponent<States>().BossReplaying = BossHasActions;
    }

    public void End()
    {
        RelaunchDimension();

    }

    public void RelaunchDimension()
    {
        int id = Player.GetComponent<Controller>().ID - 1;
        GameObject newGhost = GameObject.Instantiate(Ghost);
        newGhost.transform.position = Player.GetComponent<Controller>().InitialPosition;
        ghosts.Add(newGhost);

        tm.ResetTimers();

        TimeMachine = new List<Scripter>(OriginalTimeMachine);

        Boss.transform.position = Vector3.zero;
        Boss.GetComponent<BossMovements>().Restart();
        Boss.GetComponent<BossLife>().Life = 50;

        ghosts.ForEach(g =>
        {
            g.transform.position = Player.GetComponent<Controller>().InitialPosition;
            g.GetComponent<Player>().Reset();
        });

        timer = 0;

        BulletsToDestroy.ForEach(b =>
        {
            Destroy(b);
        });
    }

    public void GameOver()
    {
        CanMove = false;
    }

    List<GameObject> ghosts = new List<GameObject>();
    TimeManager tm;
}
