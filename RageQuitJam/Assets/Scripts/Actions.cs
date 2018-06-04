using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actions : MonoBehaviour
{
    public enum e_Attacks
    {
        none = 0,
        laser,
        jump,

    }

    public enum e_Actions
    {
        MoveLeft = 0,
        MoveRight,
        MoveUp,
        MoveDown,
        Attack,
        Dash,
        Jump,
        Teleport,
        TeleportAttack,
        BossNoActions,
        Death,
        Hit,
    }
}
