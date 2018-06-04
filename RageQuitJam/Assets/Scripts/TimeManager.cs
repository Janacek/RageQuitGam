using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [HideInInspector]
    public float UpdateTimer = 0.0f;

    [HideInInspector]
    public float FixedUpdateTimer = 0.0f;

    [HideInInspector]
    public float Pause = 1.0f;

    [HideInInspector]
    public float BulletTime = 1.0f;

    void Update()
    {
        UpdateTimer += Time.deltaTime * Pause * BulletTime;

        Random.InitState((int)(UpdateTimer * 10));
    }

    void FixedUpdate()
    {
        FixedUpdateTimer = Time.fixedDeltaTime * Pause * BulletTime;
    }

    public void ResetTimers()
    {
        UpdateTimer = 0.0f;
        FixedUpdateTimer = 0.0f;
    }
}
