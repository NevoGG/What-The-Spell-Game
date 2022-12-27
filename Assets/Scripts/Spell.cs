using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    [SerializeField] private float fireTimeCounter;
    [SerializeField] private float spellMoveSpeed = 0.1f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fireTimeCounter -= Time.deltaTime;
            
        transform.localPosition += transform.up * (spellMoveSpeed * Time.deltaTime * 100f);

        if (fireTimeCounter <= 0)
        {
            Destroy(this);
        } 
    }
}
