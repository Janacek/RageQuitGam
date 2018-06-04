using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class States : MonoBehaviour
{
    [HideInInspector]
    public bool MovingRight = false;
    [HideInInspector]
    public bool MovingLeft = false;
    [HideInInspector]
    public bool MovingUp = false;
    [HideInInspector]
    public bool MovingDown = false;

    [HideInInspector]
    public bool Jumping = false;
    [HideInInspector]
    public bool Grounded = true;
    [HideInInspector]
    public bool Dashing = false;

    [HideInInspector]
    public bool Dead = false;

    [HideInInspector]
    public bool TP = true;
    [HideInInspector]
    public bool BossReplaying = false;
}
