using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{

    public static bool timerDone;

    [SerializeField] private float timerLenPerRound = 60;
    [SerializeField] private float curTimer;
    [SerializeField] private Animator animator;
    [SerializeField] private AnimationClip clip;
    [SerializeField] private float clipDuration = 7.83333f;
    
    
    // Start is called before the first frame update
    void Start()
    {
        curTimer = timerLenPerRound;
        timerDone = false;

        animator.speed = clipDuration / timerLenPerRound;

    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("HadTimeStarted", GameManager.countDownFinish);
        if (!GameManager.gameEnded && GameManager.countDownFinish)
        {
            CustomUpdate();
        }
    }

    private void CustomUpdate()
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
