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
    [SerializeField] private int waitTime;
    private bool inDelay = false;
    public static Vector2 cameraCenter;
    public static float cameraWidth;
    public static float cameraBottomBounder;
    
    private bool acend = false;
    // Start is called before the first frame update
    void Start()
    {
        movementSpeed = initialSpeed;
        cameraWidth = topBounder - bottomBounder;
    }
    

    // Update is called once per frame
    void Update()
    {
        cameraCenter = transform.position;
        if (!GameManager.gameEnded && GameManager.countDownFinish)
        {
            CustomUpdate();
        }
    }
    
    IEnumerator DelayAtUpBounder()
    {
        yield return new WaitForSeconds(waitTime);
        acend = true;
        inDelay = false;
    }

    private void CustomUpdate()
    {
        movementSpeed += (topSpeed - initialSpeed)* Time.deltaTime / (timer.timerLenPerRound);
        if (acend)
        {
            if (transform.position.y > bottomBounder )
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - movementSpeed * Time.deltaTime, transform.position.z);

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
            else if(!inDelay)
            {
                inDelay = true;
                StartCoroutine(DelayAtUpBounder());
            }
        }
    }
}
