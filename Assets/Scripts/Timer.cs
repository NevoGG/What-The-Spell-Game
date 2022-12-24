using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{

    public static bool timerDone;

    private static int defTimerLenPerRound = 60;
    
    [SerializeField] private float timerLenPerRound = defTimerLenPerRound;
    private float curTimer;
    
    // Start is called before the first frame update
    void Start()
    {
        curTimer = timerLenPerRound;
        timerDone = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!timerDone && curTimer > 0)
        {
            curTimer -= Time.deltaTime;
        }
        else
        {
            timerDone = true;
        }
    }
}
