using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfScreen : MonoBehaviour
{
    private Camera _camera;
    private Animator _animator;
    private bool isUpside = false;
    private float _arrowGap = 5f;
    private bool hasGameStarted = false;
    private float toWait = 3f;

    private static readonly int IsOutOfScreen = Animator.StringToHash("IsOutOfScreen");

    // Start is called before the first frame update
    void Start()
    {
        toWait -= Time.deltaTime;
        if (toWait < 0) hasGameStarted = true;
        _camera = Camera.main;
        _animator = GetComponent<Animator>();
        
    }
    //add condition- is game over
    private void Update()
    {
        if (!hasGameStarted && _camera.velocity != Vector3.zero) hasGameStarted = true;
        if (hasGameStarted) ConditionUpdate();
    }

    // Update is called once per frame
    void ConditionUpdate()
    {
        Vector3 posToCheck = GetComponent<Player>().curAnimal.transform.position;
        Vector3 screenPoint = _camera.WorldToViewportPoint(posToCheck);
        if (screenPoint.y > 1) //below screen 
        {
            print("out! 0");
             _animator.SetBool("IsOutOfScreen", true);
            // if (screenPoint.y < -0.2) //??
            // {
            //     _animator.SetTrigger("DeadDown");
            //     GetComponent<Player>().HasLost();
            // }
        }

        if (screenPoint.y < 0) //above screen
        {
            print("out! 1");
            _animator.SetBool(IsOutOfScreen, true);
            // if (screenPoint.y > 1.2) //??
            // {
            //     _animator.SetTrigger("DeadUp");
            //     GetComponent<Player>().HasLost();
            // }
        }
        else _animator.SetBool(IsOutOfScreen, false);

            if (isUpside)
        {
            float newY = CameraMovment.cameraCenter.y + CameraMovment.cameraWidth / 2 + _arrowGap;
            Vector3 pos = transform.position;
            transform.position = new Vector3(pos.x, newY, pos.z);
        }

    }

    private void Flip()
    {
        Vector3 angles = transform.localEulerAngles;
        transform.localEulerAngles = new Vector3(angles.x, angles.y, angles.z + 180);
    }
}
