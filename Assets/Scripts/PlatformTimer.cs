using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformTimer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float waitTime = 4f;
    private bool active;

    void Start()
    {
        active = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            waitTime -= Time.deltaTime;
            if (waitTime <= 0)
            {
                active = false;
                transform.parent.gameObject.SetActive(false); 
                
            }
        }
    }
}
