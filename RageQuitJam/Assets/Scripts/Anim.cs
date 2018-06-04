using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim : MonoBehaviour
{
    public List<Sprite> Front;  // 0
    public List<Sprite> Back;   // 1
    public List<Sprite> Side;   // 2
    public List<Sprite> SideLow;// 3
    public List<Sprite> SideUp; // 4

    public List<Sprite> DashDown;//5
    public List<Sprite> DashUp;  //6
    public List<Sprite> DashSide;   //7
    public List<Sprite> DashSideLow;//8
    public List<Sprite> DashSideUp;//9

    public List<Sprite> WalkDown;//10
    public List<Sprite> WalkUp;  //11
    public List<Sprite> WalkSide;   //12
    public List<Sprite> WalkSideLow;//13
    public List<Sprite> WalkSideUp;//14

    public float Speed = 1.0f;
    int currentFrame = 0;

    List<List<Sprite>> sides = new List<List<Sprite>>();
    public int SideNr = 1;

    void Start()
    {
        sides.Add(Front);
        sides.Add(Back);
        sides.Add(Side);
        sides.Add(SideLow);
        sides.Add(SideUp);

        sides.Add(DashDown);
        sides.Add(DashUp);
        sides.Add(DashSide);
        sides.Add(DashSideLow);
        sides.Add(DashSideUp);

        sides.Add(WalkDown);
        sides.Add(WalkUp);
        sides.Add(WalkSide);
        sides.Add(WalkSideLow);
        sides.Add(WalkSideUp);

    }

    void Update()
    {
        timer += Time.deltaTime;

        SideNr = transform.parent.GetComponent<Player>().side;

        if (timer >= Speed)
        {
            ++currentFrame;
            if (currentFrame >= sides[SideNr].Count)
            {
                currentFrame = 0;
            }
            timer = 0;
        }
        if (currentFrame >= sides[SideNr].Count)
        {
            currentFrame = 0;
        }

        GetComponent<SpriteRenderer>().sprite = sides[SideNr][currentFrame];
        GetComponent<SpriteRenderer>().flipX = transform.parent.GetComponent<Player>().flip;
    }

    float timer = 0.0f;
}
