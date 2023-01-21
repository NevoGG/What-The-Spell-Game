using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    private Vector3 initCloudPos;
    // [SerializeField] private Vector3 cloudSpeed;
    // [SerializeField] private float leftBounderCloud;
    // Start is called before the first frame update
    [SerializeField] private float cloudSpeed;
    [SerializeField] private float rightBounder;
    [SerializeField] private float leftBounder;

    private bool left = true;
    void Start()
    {
        initCloudPos = transform.localPosition;
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
        if (left)
        {
            if (transform.position.x > leftBounder )
            {
                
                transform.position = new Vector3( transform.position.x - cloudSpeed * Time.deltaTime,transform.position.y, 0);
            }
            else
            {
                left = false;
            }
            
            
        }
        else
        {
            if (transform.position.x <rightBounder)
            {
                transform.position = new Vector3( transform.position.x + cloudSpeed * Time.deltaTime,transform.position.y, 0);
                Debug.Log("left!");
            }
            else
            {
                left = true;
            }
        }
    }
}

