using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    private Vector3 initCloudPos;
    [SerializeField] private Vector3 cloudSpeed;
    [SerializeField] private float leftBounderCloud;
    // Start is called before the first frame update
    void Start()
    {
        initCloudPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += cloudSpeed * Time.deltaTime;
        if (transform.position.x < leftBounderCloud)
        {
            transform.localPosition = initCloudPos;
        }
    }
}
