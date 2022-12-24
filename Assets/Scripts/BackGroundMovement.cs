using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float topBounder;
    [SerializeField] private float bottomBounder;

    private bool acend = true;
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        if (acend)
        {
            if (transform.position.y < topBounder)
            {
                transform.position = new Vector3(0, transform.position.y + movementSpeed * Time.deltaTime, 0);
                
            }
            else
            {
                acend = false;
            }
            
        }
        else
        {
            if (transform.position.y > bottomBounder)
            {
                transform.position = new Vector3(0, transform.position.y - movementSpeed * Time.deltaTime, 0);
                
            }
            else
            {
                acend = true;
            }
        }
    }
}
