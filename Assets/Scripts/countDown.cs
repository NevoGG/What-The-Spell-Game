using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class countDown : MonoBehaviour
{

    [SerializeField] private GameManager GM;

    public void SetCountDown()
    {
        GameManager.countDownFinish = true;
    }
    

}
