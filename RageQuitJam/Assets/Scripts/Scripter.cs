using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scripter
{
    public float Time = -1.0f;
    public Actions.e_Actions Action;
    public bool Up;
    public Utils Util = new Utils();
    public int ID;
}

public class Utils
{
    public Vector3 Position;
    Actions.e_Attacks AttackType;
    public float Speed;
    public int Zone;
}