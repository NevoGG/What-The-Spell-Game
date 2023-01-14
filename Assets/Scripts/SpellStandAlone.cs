using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellStandAlone : MonoBehaviour
{
    // Start is called before the first frame update
    private float randomDir;
    private Vector2 randForce;
    [SerializeField] private float movementForce = 2f;
    void Start()
    {
        int choice = Random.Range(0, 2);
        if (choice < 1) randomDir = -1;
        else randomDir = 1;
        randForce = Vector2.right * randomDir * movementForce;

    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody2D>().AddForce(randForce);
    }
}
