using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovment : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float topBounder;
    [SerializeField] private float bottomBounder;
    [SerializeField] private float topSpeed;
    [SerializeField] private float initialSpeed;
    [SerializeField] private Timer timer;

    private bool acend = true;
    // Start is called before the first frame update
    void Start()
    {
        movementSpeed = initialSpeed;
    }
    

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.gameEnded && GameManager.countDownFinish)
        {
            CustomUpdate();
        }
    }

    private void CustomUpdate()
    {
        movementSpeed += (topSpeed - initialSpeed)* Time.deltaTime / (timer.timerLenPerRound);
        if (acend)
        {
            if (transform.position.y > bottomBounder )
            {
                
                transform.position = new Vector3(transform.position.x, transform.position.y - movementSpeed * Time.deltaTime, transform.position.z);
                Debug.Log("acend!");
            }
            else
            {
                acend = false;
            }
            
            
        }
        else
        {
            if (transform.position.y <topBounder)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + movementSpeed * Time.deltaTime, transform.position.z);
            }
            else
            {
                acend = true;
            }
        }
    }
}
